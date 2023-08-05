using UnityEngine;

[AddComponentMenu("SCP/066 - Erik's toy")]
public sealed class scp066 : MonoBehaviour, IPassportData, IAliveForm, ISCPSkillRequest
{
    public  MurderProperties    murdInstance;
    private Rigidbody           rigbody;
    private ushort              delay;
    private bool                walking;
    private Vector3             direction;
    private byte                groundCount;

    [SerializeField] private bool   agressive;
    [SerializeField] private int    agressiveTimer;

    [SerializeField] private AudioClip[]    ericsSounds;
    [SerializeField] private AudioClip[]    randomNotesSounds;
    [SerializeField] private AudioClip      agressiveSound;

    [SerializeField] MeshRenderer   renderer;
    [SerializeField] Material       defaultMaterial;
    [SerializeField] Material       agressiveMaterial;

    public float    heigh           { get => 1; }
    public string   aliveName       { get { return "SCP - 066"; } set { } }
    public string   aliveSurname    { get { return "Erik's toy"; } set { } }
    public short    aliveAges       { get { return -066; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] 
    {
        new SCPSkillNode("Say \"Erik\"", 512),
        new SCPSkillNode("Say \"To-to-to\"", 512),
        new SCPSkillNode("Kill", 2048),
    };

    private void Start()
    {
        SkillsSet();
        murdInstance.IncludedObjects.parentGameObject = this.gameObject;
        rigbody = GetComponent<Rigidbody>();
        murdInstance.OnDamaged += () => 
        {
            if (!agressive)
            {
                agressive               = true;
                agressiveTimer          = 1024;
                SoundSpots.Generate(transform, agressiveSound, out AudioSource aSource);
                aSource.spatialBlend    = 1;
                aSource.minDistance     = 25;
                aSource.maxDistance     = 800;
            }
        };
    }
    private void FixedUpdate()
    {
        murdInstance.CirclingHeart();
        murdInstance.CheckDie(murdInstance.IncludedObjects.parentGameObject);

        delay++;
        if (delay > 256) 
        {
            delay       = 0; 
            walking     = Random.Range(0, 100) > 50 ? true : false;
            direction   = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

            SoundSpots.Generate(
                transform,
                Random.Range(0, 100) > 10 ?
                randomNotesSounds[Random.Range(0, randomNotesSounds.Length)] 
                :
                ericsSounds[Random.Range(0, ericsSounds.Length)], out AudioSource aSource
                );

            aSource.spatialBlend    = 1;
            aSource.rolloffMode = AudioRolloffMode.Linear;
            aSource.minDistance     = 0.2f;
            aSource.maxDistance     = 128;
            rigbody.AddRelativeForce(Random.Range(-14.0f, 14.0f), 0, Random.Range(-14.0f, 14.0f),ForceMode.Impulse);
        }

        groundCount = (byte)Physics.OverlapSphere(transform.position - new Vector3(0, 2.1f, 0), 3f).Length;

        if (walking && groundCount>1)
        {
            rigbody.AddForce(direction, ForceMode.Impulse);
        }

        if (agressive)
        {
            CamMove_v2.instance.SetShake(3);
            renderer.sharedMaterial = agressiveMaterial;
            foreach (Collider collider in murdInstance.allRaycastedObjects)
            {
                if (collider.gameObject == this.gameObject) continue;

                if (collider.TryGetComponent<IAliveForm>(out IAliveForm aliveform))
                {
                    aliveform.GetField().SetDamage(4, true);
                }
                if (collider.TryGetComponent<IObjectParameters>(out IObjectParameters objectparameters))
                {
                    objectparameters.GetProperties().health -= 4;
                }
            }

            if (agressiveTimer > 0)
            {
                agressiveTimer--;
            }
            else
            {
                agressive = false;
                return;
            }
            walking = false;
            if (!GetComponent<DangerChecker>())
            {
                this.gameObject.AddComponent<DangerChecker>();
            }
        }
        else
        {
            renderer.sharedMaterial = defaultMaterial;
            if (GetComponent<DangerChecker>())
            {
                Destroy(GetComponent<DangerChecker>());
            }
        }


    }
    public void SkillsSet()
    {
        skills[0].isUsedSkill += () =>
        {
            SoundSpots.Generate(transform, ericsSounds[Random.Range(0, ericsSounds.Length)], out AudioSource aSource);
            aSource.spatialBlend = 1;
            aSource.rolloffMode = AudioRolloffMode.Linear;
            aSource.minDistance = 0.2f;
            aSource.maxDistance = 128;
            rigbody.AddRelativeForce(Random.Range(-14.0f, 14.0f), 0, Random.Range(-14.0f, 14.0f), ForceMode.Impulse);
        };
        skills[1].isUsedSkill += () =>
        {
            SoundSpots.Generate(transform, randomNotesSounds[Random.Range(0, randomNotesSounds.Length)], out AudioSource aSource);
            aSource.spatialBlend = 1;
            aSource.rolloffMode = AudioRolloffMode.Linear;
            aSource.minDistance = 0.2f;
            aSource.maxDistance = 128;
            rigbody.AddRelativeForce(Random.Range(-14.0f, 14.0f), 0, Random.Range(-14.0f, 14.0f), ForceMode.Impulse);
        };
        skills[2].isUsedSkill += () =>
        {
            if (!agressive)
            {
                agressive = true;
                agressiveTimer = 1024;
                SoundSpots.Generate(transform, agressiveSound, out AudioSource aSource);
                aSource.spatialBlend = 1;
                aSource.minDistance = 25;
                aSource.maxDistance = 800;
            }
        };
    }
    IAliveConfigs IAliveForm.GetField() => murdInstance;
}

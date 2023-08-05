using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/682 - Invulnerable reptile")]
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class scp682 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    public  MurderProperties          murderConfigs   = new MurderProperties();
    private Animator        _animSource     => GetComponent<Animator>();
    private NavMeshAgent    _navmesh        => GetComponent<NavMeshAgent>();
    private Rigidbody       _rigidBody      => GetComponent<Rigidbody>();
    public float            heigh           { get => 5; }
    public string           aliveName       { get { return "SCP - 682"; } set { } }
    public string           aliveSurname    { get { return "Invulnerable reptile"; } set { } }
    public short            aliveAges       { get { return 0; } set { } }
    public IAliveConfigs GetField() => murderConfigs;
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[2] 
    {
        new SCPSkillNode("Dash", 4000),
        new SCPSkillNode("Open doors", 500)
    };
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GameObject     soundSpot;
    [SerializeField] private AudioClip      audioclip;
    
    private void Start          ()
    {
        SkillsSet();
        murderConfigs.IncludedObjects.parentGameObject = gameObject;
    }
    private void FixedUpdate    ()
    {
        MainThreadHandler.AddActions(()=>{murderConfigs.CirclingHeart();});
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        try
        {
            NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, 5, murderConfigs.allRaycastedObjects, out GameObject men);

            if (men)
            {
                IWannaKillMen(men);
            }
        }
        catch
        {

        }


        WalkingAnimationUtility.Animate(_animSource, _navmesh, _rigidBody, 1, 0.02f, 2, 0.1f);
        if (murderConfigs.walking > 50)
        {
            IWannaWalking(true);
        }
        else
        {
            IWannaWalking(false);
        }
    }
    private void IWannaKillMen  (in GameObject men)
    {
        if (Vector3.Distance(transform.position, men.transform.position) < 8 && !_animSource.GetCurrentAnimatorStateInfo(0).IsTag("ATTACK"))
        {
            bool setRandom = Random.Range(0,50) >= 25? true : false;
            _animSource.SetBool("AltVer", setRandom);

            _animSource.Play("CheckBool");
            GameObject soundSpotInst = Instantiate(soundSpot,transform.position,Quaternion.identity);
            soundSpotInst.GetComponent<AudioSource>().clip = audioclip;
            soundSpotInst.GetComponent<AudioSource>().Play();

            if (men.TryGetComponent<IAliveForm>(out IAliveForm aliveForm))
            {
                aliveForm.GetField().SetDamage(90,true);
            }   
        }
        if (!_animSource.GetCurrentAnimatorStateInfo(0).IsTag("ATTACK"))
        {
            murderConfigs.interestPoint = men.transform.position;
            murderConfigs.walking = 100;
            Vector3 targetViewing = new Vector3(men.transform.position.x,transform.position.y,men.transform.position.z);
            transform.LookAt(targetViewing);
        }
        else
        {
            murderConfigs.walking = 0;
            _navmesh.velocity = Vector3.zero;
            GetComponent<rig682>().rig.weight = 0;
            particle.Emit(50);
        }
    }
    private void IWannaWalking  (in bool intertstitionToSelf)
    {
        if (!_navmesh.enabled && !_navmesh.isActiveAndEnabled) return;
        _navmesh.SetDestination(intertstitionToSelf is true ? murderConfigs.interestPoint : transform.position);
    }
    public  void SkillsSet      ()
    {
        skills[0].isUsedSkill += () => {
            GetComponent<NavAgentGroundCheck>().DisableNavmesh();

            Collider[] colliders = Physics.OverlapSphere(transform.position,150);
            NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, 5, colliders, out GameObject men);

            if (men)
            {
                _rigidBody.velocity = (transform.position - men.transform.position).normalized*-12 + (new Vector3(0,18,0)); 
            }
            else
            {
                _rigidBody.velocity = (transform.forward * 15) + new Vector3(0, 18, 0);
            }
        };
        skills[1].isUsedSkill += () => {
            Collider[] colliders = Physics.OverlapSphere(transform.position,60);
            for (short i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("DoorGates"))
                {
                    colliders[i].GetComponent<Animator>().SetInteger("Timer", 150);
                }
            }
        };
    }
}
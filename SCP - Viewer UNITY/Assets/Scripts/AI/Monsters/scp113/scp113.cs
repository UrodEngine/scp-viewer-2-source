using UnityEngine;

[AddComponentMenu("SCP/113 - gender stone")]
public class scp113 : MonoBehaviour, IPassportData,ISCPSkillRequest
{
    #region Alterable values
    /*=========================================================================================================================================================*/

    public  int             ReloadTimer;
    public SimpleDelayer simpleDelayer = new SimpleDelayer(15);
    private ParticleSystem  partSystem  => transform.GetChild(0).GetComponent<ParticleSystem>();
    private AudioSource     aSource     => GetComponent<AudioSource>();
    private Rigidbody       rigbod      => GetComponent<Rigidbody>();

    [SerializeField] private ParticleSystem skillParticles;
    [SerializeField] private AudioSource    skillSound;

    public string   aliveName       { get { return "SCP - 113"; } set { } }
    public string   aliveSurname    { get { return "Gender switcher"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Jump (Low)",3600), 
        new SCPSkillNode("Jump (Default)", 6500),
        new SCPSkillNode("Wave", 10500),
    };
    /*=========================================================================================================================================================*/
    #endregion

    private void Start          ()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            Debug.Log($"{skills[i].name} -> {skills[i].reload}");
        }
        SkillsSet();
    }
    private void FixedUpdate    (){
        if (ReloadTimer <= 0) partSystem.Emit(1);
        else partSystem.Stop();

        GameObject people = new NearObjUtilities().NearestTarget(transform, 0, 10, nameof(Man), nameof(DangerChecker));
        ReloadTimer = ReloadTimer > 0 ? ReloadTimer - 1 : ReloadTimer;
        if (people is null) return;
        AttachToOther(people);
    }
    private void AttachToOther  (in GameObject target){
        if (target is null || ReloadTimer > 0) return;
        Man dclass = target.GetComponent(nameof(Man)) as Man;

        rigbod.velocity = (target.transform.position+new Vector3(0,2,0) - transform.position)*2.4f; //move towards target

        if (Vector3.Distance(target.transform.position, transform.position) < 5f){
            aSource.Play();
            ReloadTimer = 600;
            ChangeGender(target);
            dclass.DClassConfigs.SetStan(128);
            dclass.DClassConfigs.SetDamage(100,true);
        }
    } //Если рядом человек, присоединяется к нему и пытается поменять пол. + перезарядка камня. + станит человека
    private void ChangeGender   (in GameObject target){

        Man man = target.GetComponent<Man>();
        
        if (man.DClassConfigs.components.skinnedMeshRenderer.sharedMesh == man.genderMeshes.male)
        {
            man.DClassConfigs.components.skinnedMeshRenderer.sharedMesh = man.genderMeshes.female;
        }
        else
        {
            man.DClassConfigs.components.skinnedMeshRenderer.sharedMesh = man.genderMeshes.male;
        }

    } //Меняет пол человека
    public  void SkillsSet      ()
    {
        skills[0].isUsedSkill += () => {
            rigbod.velocity = rigbod.velocity + new Vector3(0, 15, 0);
        };
        skills[1].isUsedSkill += () => {
            rigbod.velocity = rigbod.velocity + new Vector3(0, 25, 0);
        };
        skills[2].isUsedSkill += () => {
            skillParticles.Emit(118);
            skillSound.Play();
            Collider[] colliders = Physics.OverlapSphere(transform.position,45);
            for (uint index = 0; index < colliders.Length; index++)
            {
                if (colliders[index].TryGetComponent<Man>(out Man component))
                {
                    aSource.Play();
                    ChangeGender(colliders[index].gameObject);
                    component.DClassConfigs.SetStan(128);
                    component.DClassConfigs.SetDamage(100, true);
                }
            }
        };
    }
}
using UnityEngine;

[AddComponentMenu("SCP/1123 - skull of agressive"),DisallowMultipleComponent]
public class scp1123 : MonoBehaviour, IPassportData,ISCPSkillRequest
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private int            _activeTimer;
    [SerializeField] private bool           _mensWantTakeMe;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ParticleSystem skillParticles;
    [SerializeField] private AudioSource    skillSound;

    public string   aliveName       { get { return "SCP - 1123"; } set { } }
    public string   aliveSurname    { get { return "Cruelty skull"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Jump (Low)",3600),
        new SCPSkillNode("Jump (Default)", 6500),
        new SCPSkillNode("Agressive wave", 12500),
    };
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion
    private void Start()
    {
        SkillsSet();
    }
    private void    Update      (){
        _activeTimer = _activeTimer > 0 ? _activeTimer - 1 : ResetTimer();

        if (_mensWantTakeMe) TakeMe();
    }
    private int     ResetTimer  (){
        _mensWantTakeMe = Random.Range(0, 100) > 50 ? true : false;
        return 256;
    }
    private void    TakeMe      ()
    {
        if (_mensWantTakeMe) particles.Emit(1);
        GameObject nearestMen = new NearObjUtilities().NearestTarget(transform, 5, 90, nameof(Man));
        if (nearestMen is null) return;
        Man dclass = nearestMen.GetComponent(nameof(Man)) as Man;
        dclass.DClassConfigs.interestPoint = transform.position;
        if (Vector3.Distance(transform.position, nearestMen.transform.position) < 6) 
        {
            _mensWantTakeMe = false;
            _activeTimer    = 512;
           
            dclass.DClassConfigs.SetStan(128);

            dclass.  DClassConfigs.agressive = true;
            dclass.  bravery                 = 66;
            dclass.  ManType                 = Man.ManTypeEnum.AbsolutelyAgressive;
            GetComponent<Rigidbody>().          velocity                = GetComponent<Rigidbody>().velocity + new Vector3(0, 15, 0);
            GetComponent<Rigidbody>().          angularVelocity         = GetComponent<Rigidbody>().angularVelocity + new Vector3(15, 15, 15);
        }
    }
    public  void    SkillsSet   ()
    {
        skills[0].isUsedSkill += () => {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3(0, 15, 0);
        };
        skills[1].isUsedSkill += () => {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3(0, 25, 0);
        };
        skills[2].isUsedSkill += () => {
            skillParticles.Emit(118);
            skillSound.Play();
            Collider[] colliders = Physics.OverlapSphere(transform.position,45);
            for (int i = 0; i < colliders.Length; i++)
            {
                Man component = colliders[i].GetComponent<Man>();
                if (!component) continue;
                component.DClassConfigs.SetStan(100);
                component.DClassConfigs.agressive = true;
                component.bravery = 100;
            }
        };
    }
}

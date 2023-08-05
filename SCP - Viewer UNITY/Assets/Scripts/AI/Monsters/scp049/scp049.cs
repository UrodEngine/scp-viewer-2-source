using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/049 - plague Doctor")]
sealed class scp049 : MonoBehaviour, IAliveForm , IPassportData, ISCPSkillRequest
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public  MurderProperties          murdInstances;

    private GameObject      _people;
    private NavMeshAgent    _navMeshAgent;
    private Rigidbody       _rigidBody;
    private Animator        _animator;

    [SerializeField] private UnityEngine.Animations.Rigging.ChainIKConstraint chainRig;
    [SerializeField] private ParticleSystem skillParticle;
    [System.Serializable] public struct  ZombiePrefabsByTypeMan
    {
        public Man.ManTypeEnum typeOfMan;
        public GameObject zombiePrefab;
    }
    public ZombiePrefabsByTypeMan[]     zombiePrefabs;

    public byte killDelay = 0;
    public float heigh { get => 7; }
    public string aliveName     { get { return "SCP - 049"; } set { } }
    public string aliveSurname  { get { return "Plague doctor"; } set { } }
    public short aliveAges      { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Anti-plague wave", 12250),
    };
    /*=========================================================================================================================================================*/
    #endregion

    private void Awake                  (){
        SkillsSet();
        murdInstances.IncludedObjects.parentGameObject = this.gameObject;
        murdInstances.blinkingTimer = 99;
        _animator                   = GetComponent<Animator>();
        _navMeshAgent               = GetComponent<NavMeshAgent>();
        murdInstances.OnDamaged     += OnDamaged;
    }
    private void OnDamaged              ()
    {
        murdInstances.walking = 100;
    }
    public  void FixedUpdate            (){
        MainThreadHandler.AddActions(() => { murdInstances.CirclingHeart(); });
        murdInstances.CheckDie(murdInstances.IncludedObjects.parentGameObject);
        NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, 7, murdInstances.allRaycastedObjects, out _people);
        AnimationWalking();
        MovingMethod();
        if (killDelay > 0)
        {
            killDelay--;
        }
    }
    private void MovingMethod           ()
    {
        if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh) return;
        if (murdInstances.walking >= 25)
            if (_people == null)
            {
                _navMeshAgent.SetDestination(murdInstances.interestPoint);
                chainRig.weight = Mathf.Lerp(chainRig.weight, 0, 0.1f);
            }
            else
            {
                TryToKillPeople();
                if (Vector3.Distance(_people.transform.position, transform.position) < 25)
                {
                    _people.GetComponent<Man>().navMeshAgent.speed = 34;
                }
                chainRig.weight = Mathf.Lerp(chainRig.weight, 1, 0.1f);
                _navMeshAgent.SetDestination(_people.transform.position);
            }
        else
        {
            chainRig.weight = Mathf.Lerp(chainRig.weight, 0, 0.1f);
        }
        if (murdInstances.walking < 25 && _navMeshAgent.enabled){
            _navMeshAgent.SetDestination(transform.position);
        }
    }
    private void TryToKillPeople        (){
        if (_people == null) return;
        if (Vector3.Distance(transform.position, _people.transform.position) < 6.8f && murdInstances.walking >= 25 && killDelay<=0){
            transform.position = _people.transform.position;
            murdInstances.walking = 0;
            killDelay = 90;
            for (byte i = 0; i < zombiePrefabs.Length; i++){
                if (_people.GetComponent<Man>().ManType == zombiePrefabs[i].typeOfMan){
                    GameObject PreZobject = Instantiate(zombiePrefabs[i].zombiePrefab, transform.position, Quaternion.identity);
                    PreZobject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    break;
                }
                continue;
            }
            Destroy(_people);
        }
    }
    private void AnimationWalking       () => WalkingAnimationUtility.Animate(_animator, _navMeshAgent, _rigidBody, 1, 0.45f, 5, 0.4f);
    public  void SkillsSet              ()
    {
        skills[0].isUsedSkill += () => {
            AntiPlagueSkill();
        };
    }
    public  void AntiPlagueSkill        ()
    {
        Collider[] peoples = Physics.OverlapSphere(transform.position,45);
        murdInstances.walking = 0;
        for (short i = 0; i < peoples.Length; i++)
        {
            try
            {
                if (peoples[i].gameObject.GetComponent<Man>() == null)
                {
                    continue;
                }
                Man man = peoples[i].GetComponent<Man>();

                GameObject PreZobject = Instantiate(man.zombieRagdollPrefab, peoples[i].transform.position, Quaternion.identity);
                PreZobject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                
                skillParticle.Emit(5);
                Destroy(peoples[i].gameObject);
            }
            catch
            {
                continue;
            }
        }
    }

    IAliveConfigs IAliveForm.GetField() => murdInstances;

}

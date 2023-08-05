using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/1048 - bear the builder")]
public sealed class scp1048 : MonoBehaviour , IAliveForm , IPassportData, ISCPSkillRequest
{
    #region Alterable values
    //==============================================================================================================
    public IAliveConfigs GetField() => murderConfigs;
    public MurderProperties murderConfigs;

    private Animator        _animator       => GetComponent<Animator>();
    private Rigidbody       _rigidBody      => GetComponent<Rigidbody>();
    private NavMeshAgent    _navMeshAgent   => GetComponent<NavMeshAgent>();

    [SerializeField] private GameObject _audioSpotSource;
    [SerializeField] private GameObject _soundWaveEffect;
    [SerializeField] private AudioClip  _screamClip;
    [SerializeField] private bool       _isB_type;
    [SerializeField] private Material   _bTypeMaterial;

    private bool    _isWannaSearchPeoples { get; set; }
    private int     _screamTimer        = 100;
    private int     _spawnBtype_timer   = 1024;
    public float    heigh       { get => 3; }
    public string   aliveName       { get { return "SCP - 1048"; } set { } }
    public string   aliveSurname    { get { if (!_isB_type) return "Bear - The builder"; else return "Bear - B-TYPE"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Create B-type",10048),
    };
    //==============================================================================================================
    #endregion
    private void Start              (){
        SkillsSet();
        murderConfigs.IncludedObjects.parentGameObject = this.gameObject;
        murderConfigs.allObjectSearchRadius = 128;
        _animator.SetBool("isB_type", _isB_type);
        if (_isB_type == false) Destroy(GetComponent<DangerChecker>());
        murderConfigs.OnThinked += delegate { _isWannaSearchPeoples = Random.Range(0, 2) >= 1 ? true : false; Debug.Log(_isWannaSearchPeoples); };
        if (_isB_type)
        {
            skills = new SCPSkillNode[] {
        new SCPSkillNode("Noise wave",5500),
        };
            skills[0].isUsedSkill += () => {
                SPAWN_LOUD_SOUNDS();
            };
        }    
    }
    private void Update             ()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("SCREAM"))
        {
            murderConfigs.walking = 0;
            murderConfigs.interestPoint = transform.position;
            return;
        }
        if (_isB_type) _isWannaSearchPeoples = true;

        try
        {
            if (_isWannaSearchPeoples is true)
            {
                NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, heigh, murderConfigs.allRaycastedObjects, out GameObject OUTPUT);
                if (OUTPUT != null) PeopleFollow(OUTPUT);
            }
        }
        catch
        {

        }
    }
    private int  SpawnBtype         ()
    {
        GameObject SCP_1048_BType = Instantiate(gameObject, transform.position, Quaternion.identity);
        SCP_1048_BType.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterial = _bTypeMaterial;
        SCP_1048_BType.GetComponent<scp1048>()._isB_type = true;
        SCP_1048_BType.AddComponent(typeof(DangerChecker));
        return 2048;
    }
    private void PeopleFollow       (in GameObject target)
    {
        if (_navMeshAgent.enabled is false || _navMeshAgent.isOnNavMesh is false) return;
        murderConfigs.walking = 50;
        _navMeshAgent.SetDestination(target.transform.position);
        if (_isB_type && _screamTimer <= 0 && Vector3.Distance(target.transform.position, transform.position) < 8) SPAWN_LOUD_SOUNDS();
    }
    private void FixedUpdate        (){
        _screamTimer        = _screamTimer > 0 ? _screamTimer - 1 : _screamTimer;
        if(_isB_type is false) _spawnBtype_timer   = _spawnBtype_timer > 0 ? _spawnBtype_timer - 1 : SpawnBtype();
        MainThreadHandler.AddActions(murderConfigs.CirclingHeart);
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        WalkingAnimation();
        IWannaWalking();
    }
    private void SPAWN_LOUD_SOUNDS  ()
    {
        GameObject loudSound = Instantiate(_audioSpotSource, transform.position, Quaternion.identity);
        loudSound.GetComponent<DestroyTimer>().TimerToDie = 5;
        loudSound.GetComponent<AudioSource>().clip = _screamClip;
        loudSound.GetComponent<AudioSource>().Play();
        DamageOnZone damagerOnZone = loudSound.AddComponent<DamageOnZone>();
        damagerOnZone.radius       = 10;
        damagerOnZone.damage       = 9;
        damagerOnZone.withBlood    = true;
        _animator.Play("SCREAM");
        _screamTimer = 512;
        GameObject soundWaveInstance = Instantiate(_soundWaveEffect,loudSound.transform);
        soundWaveInstance.SetActive(true);
    }
    private void IWannaWalking      ()
    {
        if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh) return;
        if (murderConfigs.walking > 32)
            _navMeshAgent.SetDestination(murderConfigs.interestPoint);
        else _navMeshAgent.SetDestination(transform.position);
    }
    private void WalkingAnimation   () => WalkingAnimationUtility.Animate(_animator, _navMeshAgent, _rigidBody, 1, 0.015f, 5, 0.4f);
    public  void SkillsSet          ()
    {
        skills[0].isUsedSkill += () => {
            SpawnBtype();
        };
    }
}

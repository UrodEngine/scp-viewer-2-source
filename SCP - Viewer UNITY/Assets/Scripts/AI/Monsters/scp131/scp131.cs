using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/131 - 2 KapleGlaziki")]
public class scp131 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public  ManProperties             _aliveInstance = new ManProperties();
    private NavMeshAgent    _nAgent;
    private GameObject      _interestingMan;
    public float            heigh { get => 2; }
    public string aliveName     { get { return "SCP - 131"; } set { } }
    public string aliveSurname  { get { return "\"The Eye Pods\""; } set { } }
    public short aliveAges      { get { return 0; } set { } }

    [SerializeField] private float CircleDistancerMultiplier;
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Run", 3600),
        new SCPSkillNode("Jump", 6500),
    };

    private float sinx, cosz;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void            Start               ()
    {
        SkillsSet();
        _aliveInstance.IncludedObjects.parentGameObject = this.gameObject;
        _nAgent = GetComponent<NavMeshAgent>();
    }
    private void            FixedUpdate              ()
    {
        _nAgent.speed = _nAgent.speed > 17.85f ? _nAgent.speed - 0.25f : _nAgent.speed;
        MainThreadHandler.AddActions(() => { _aliveInstance.CirclingHeart(); });
        _aliveInstance.CheckDie(_aliveInstance.IncludedObjects.parentGameObject);
        Movement();
        
        sinx = Mathf.Sin(Time.fixedTime);
        cosz = Mathf.Cos(Time.fixedTime);
        Vector3 CircleDistancer = new Vector3(sinx* CircleDistancerMultiplier, 0, cosz* CircleDistancerMultiplier);

        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 0, _aliveInstance.allRaycastedObjects, nameof(Man),out _interestingMan);

        GameObject danger;
        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 0, _aliveInstance.allRaycastedObjects, nameof(DangerChecker), out danger);

        if (_interestingMan != null && danger == null)
        {
            _aliveInstance.interestPoint = _interestingMan.transform.position + CircleDistancer;
            Debug.DrawLine(transform.position, _interestingMan.transform.position + CircleDistancer);
        }
        else if(_interestingMan != null && danger != null)
        {
            _aliveInstance.interestPoint = ((danger.transform.position + _interestingMan.transform.position) / 2) + CircleDistancer;
            Debug.DrawLine(transform.position, ((danger.transform.position + _interestingMan.transform.position) / 2) + CircleDistancer);
        }
        if (danger != null) LookAtDangerChecker( danger);
    }
    private void            LookAtDangerChecker (in GameObject target)
    {
        _nAgent.speed = 34;
        Vector3 totarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(totarget);
        
        target.GetComponent<DangerChecker>().discoveredTimer = 130;
    }
    private void            Movement         ()
    {
        if (_nAgent.enabled is false || _nAgent.isOnNavMesh is false) return;
        if (_aliveInstance.walking > 25)
        {
            _nAgent.SetDestination(_aliveInstance.interestPoint);
        }
        else
        {
            _nAgent.SetDestination(transform.position);
        }
    }
    public  IAliveConfigs   GetField            () => _aliveInstance;
    public void             SkillsSet()
    {
        skills[0].isUsedSkill += () => {
            _nAgent.speed = 50;
        };
        skills[1].isUsedSkill += () => {
            GetComponent<NavAgentGroundCheck>().DisableNavmesh();
            GetComponent<NavMeshAgent>().enabled = false;
            Rigidbody rigbody = GetComponent<Rigidbody>();
            rigbody.velocity = rigbody.velocity + new Vector3(0, 15, 0) + transform.forward * 15;
        };
    }
}

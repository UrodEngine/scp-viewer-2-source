using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("SCP/ZOMBIE")]
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class Zombie : MonoBehaviour, IAliveForm, IPassportData
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    //static ConsistentObjContainer  zContainer = new ConsistentObjContainer();
    
    public  MurderProperties          murderConfigs = new MurderProperties();
    public  int             damagePerPunch;

    private VoicesOn        _voiceSource;
    private Animator        _animator;
    private Rigidbody       _rigBody;
    private NavMeshAgent    _navMeshAgent;
    private int             _localStannedTime;
    public float    heigh       { get => 7; }
    public string   aliveName       { get { return "Zombie"; } set { } }
    public string   aliveSurname    { get { return ""; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    // ---------------------------------------------------------------------------------------------------------------
    #endregion
    private void Start          ()  
    {
        _rigBody        = this.GetComponent<Rigidbody>();
        _animator       = this.transform.GetChild(0).GetComponent<Animator>();
        _voiceSource    = this.transform.GetChild(0).GetComponent<VoicesOn>();
        _navMeshAgent   = this.GetComponent<NavMeshAgent>();

        murderConfigs.IncludedObjects.parentGameObject = this.gameObject;
    }
    private void AttackMens     (in GameObject targetMen)   
    {
        if (Vector3.Distance(transform.position,targetMen.transform.position) < 7f && _localStannedTime <= 0)
        {
            LocalStanned(25);
            _animator.Play("DclassBones|ZAttack", 0,0);
            _voiceSource.AttackVoice();
            targetMen.GetComponent<Man>().DClassConfigs.SetDamage(damagePerPunch,true);
        }
    }   // +LocalStanned
    private void FixedUpdate    ()  
    {
        _localStannedTime = _localStannedTime > 0 ? _localStannedTime - 1 : _localStannedTime;
        
        MainThreadHandler.AddActions(() => { murderConfigs.CirclingHeart(); });
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        if (murderConfigs.stanTime > 0 || murderConfigs.properties.health < 0 || _localStannedTime > 0) return;

        IWalkAnimation();
        IwannaWalking();

        PeopleSearch(murderConfigs.allRaycastedObjects);
    }
    private void PeopleSearch   (in Collider[] targets)     
    {
        GameObject men = new NearObiUtilitiesSimple().NearestTargetComponent(transform, 7, targets, nameof(Man));
        if (men is null)
        {
            return;
        }
        if (men.GetComponent<Man>().bravery < 40)
        {
            men.GetComponent<Man>().navMeshAgent.speed = 43;
        }
        if (_navMeshAgent.enabled && _navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.SetDestination(men.transform.position);
        }
        AttackMens(men);
        murderConfigs.walking = 100;
    }   // +AttackMens
    private void LocalStanned   (in int value)
    {
        _localStannedTime = value;
        _navMeshAgent.SetDestination(transform.position);        
        _animator.SetLayerWeight(1, 0);
        _animator.SetLayerWeight(2, 0);
    }
    private void IwannaWalking  ()  
    {
        if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh || murderConfigs.scaredTimer > 0) return;
        if (murderConfigs.walking < 35)
        {
            _navMeshAgent.SetDestination(transform.position);
            return;
        }
        _navMeshAgent.SetDestination(murderConfigs.interestPoint);
    }
    private void IWalkAnimation ()  
    {
        WalkingAnimationUtility.Animate(_animator, _navMeshAgent, _rigBody, 1, 0.015f, 5, 0.1f);
    }
    public IAliveConfigs GetField() => murderConfigs;
}

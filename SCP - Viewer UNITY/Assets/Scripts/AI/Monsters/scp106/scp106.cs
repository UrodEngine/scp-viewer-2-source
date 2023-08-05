using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/106 - old man")]
public sealed class scp106 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{   
    #region Alterable values
    /*=========================================================================================================================================================*/
    public  MurderProperties          murderConfigs;
    public  short           chanceToPortalize   = 0;
    public  bool            walkBlock           = false;
    private GameObject      _people             = null;
    private Animator        _animator;
    private NavMeshAgent    _navMeshAgent;
    private Rigidbody       _rigidBody => GetComponent<Rigidbody>();
    public float    heigh       { get => 7; }
    public string   aliveName       { get { return "SCP - 106"; } set { } }
    public string   aliveSurname    { get { return "Old man"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Go to portal", 3600),
        new SCPSkillNode("Exit the portal", 3600),
    };


    [SerializeField] private UnityEngine.Animations.Rigging.ChainIKConstraint chainRig;
    /*=========================================================================================================================================================*/
    #endregion

    private void Start              (){
        SkillsSet();
        murderConfigs.IncludedObjects.parentGameObject = this.gameObject;
        _navMeshAgent                   = GetComponent<NavMeshAgent>();
        _animator                       = GetComponent<Animator>();
        murderConfigs.OnThinked        += TryPortalizate;
    }
    private void FixedUpdate        (){
        MainThreadHandler.AddActions(()=> {murderConfigs.CirclingHeart();});
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);

        try
        {
            SearchPeople();
            TryToKillPeople();
            if (_people)
            {
                if (Vector3.Distance(transform.position, _people.transform.position) < 10)
                {
                    chainRig.weight = Mathf.Lerp(chainRig.weight, 1, 0.1f);
                }
                else
                {
                    chainRig.weight = Mathf.Lerp(chainRig.weight, 0, 0.1f);
                }
            }
            else
            {
                chainRig.weight = Mathf.Lerp(chainRig.weight, 0, 0.1f);
            }
        }
        catch
        {
            _people = null;
        }

        if (murderConfigs.walking > 25)
        {
            IWannaWalking();
        }
        else
        {
            if (_navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.SetDestination(transform.position);
            }
            IWannaAnimWalking();
            if (_people != null)
            {
                murderConfigs.walking = 100;
            }
            DoorSearching();
        }
    }
    private void SearchPeople       ()
    {
        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 8, murderConfigs.allSpheredObjects, nameof(Man), out _people);
    }
    private void DoorSearching      ()
    {
        Collider[] DoorOverpass = Physics.OverlapSphere(transform.position, 10); //For NearObjUtilitiesV2
        GameObject door = null;
        NearObjUtilitiesStatic.NearestTargetTagNoRay(transform, DoorOverpass, 5, "DoorGates", out door);
        if (door != null)
            door.GetComponent<Animator>().SetInteger("Timer", 45);
    }
    private void IWannaWalking      (){
        if (_navMeshAgent.isOnNavMesh is false)              return;

        if (murderConfigs.components.animator.GetCurrentAnimatorStateInfo(0).IsTag("portal"))
        {
            murderConfigs.walking = 0;
            _navMeshAgent.SetDestination(transform.position);
        }

        if (walkBlock is true){
            murderConfigs.walking = 0;
            _navMeshAgent.SetDestination(transform.position);
            return;
        }
        if (_people is null)
        {
            _navMeshAgent.SetDestination(murderConfigs.interestPoint);
        }
        else
        {
            _navMeshAgent.SetDestination(_people.transform.position);
        }
    }
    private void TryPortalizate     (){
        chanceToPortalize = (short)Random.Range(0, 300);
        if (chanceToPortalize >= 150)                                                   _animator.SetInteger("IsPortal", 1);
        else if (chanceToPortalize < 150 && _animator.GetInteger("IsPortal") == 1)      _animator.SetInteger("IsPortal", 2);
    }
    private void TryToKillPeople    (){
        if (_people is null)
        {
            return;
        }
        if (murderConfigs.components.animator.GetCurrentAnimatorStateInfo(0).IsTag("portal"))
        {
            return;
        }
        InteractiveMethods.FearByObject(transform.position, _people.transform);
        _people.GetComponent<Man>().navMeshAgent.speed = 54;
        if (Vector3.Distance(_people.transform.position, transform.position) < 5.5f && walkBlock is false){
            transform.position  = _people.transform.position;
            Man component    = _people.GetComponent<Man>();
            component?.DClassConfigs.KillForm(true);
        }
    }
    private void IWannaAnimWalking  () => WalkingAnimationUtility.Animate(_animator, _navMeshAgent,_rigidBody, 1, 0.015f, 5, 0.4f);
    public void SkillsSet()
    {
        skills[0].isUsedSkill += () =>
        {
            murderConfigs.thinkingTimer = 1000;
            chanceToPortalize = 155;
            WalkBlocker(1);
            _animator.SetInteger("IsPortal", 1);
        };
        skills[1].isUsedSkill += () =>
        {
            murderConfigs.thinkingTimer = 1000;
            chanceToPortalize = 0;
            _animator.SetInteger("IsPortal", 2);
        };
    }
    public IAliveConfigs GetField() => murderConfigs;

    #region By animator event
    private void WalkBlocker        (int blocker)
    {
        walkBlock = blocker == 1 ? true : false;

        if (walkBlock is true)  GetComponent<Collider>().enabled = false;
        else                    GetComponent<Collider>().enabled = true;
    }       //By animator event.    +Collider switcher.
    private void ResetWalkerBlock   ()
    {
        _animator.SetInteger("IsPortal", 0);
    }                  //By animator event.
    private void TeleportToVictim   ()
    {
        if (_people is null) return;
        _navMeshAgent.enabled    = false;
        Vector3 tpCoordinates   = _people.transform.position + new Vector3(0, -1, 0);
        transform.position      = tpCoordinates;
        _navMeshAgent.enabled    = true;
    }                  //By animator event.

    #endregion
}

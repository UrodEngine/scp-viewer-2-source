using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[AddComponentMenu("SCP/096 - modest")]
public class main096 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public MurderProperties murderConfigs = new MurderProperties();

    [SerializeField] private int            _anxioty;
    [SerializeField] private int            _madnessStage;
    [SerializeField] private Rig[]          _rigs;
    [SerializeField] private Transform      _lookAtMan;
    [SerializeField] private System.Collections.Generic.List<GameObject> _allRememberedHumans = new System.Collections.Generic.List<GameObject>();

    private Animator        _animSource;
    private NavMeshAgent    _navmesh;
    private sounds096       _sounds096Pack;
    public float    heigh       { get => 7; }
    public string   aliveName       { get { return "SCP - 096"; } set { } }
    public string   aliveSurname    { get { return "\"humble\""; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public SCPSkillNode[] skills { get; set; } = new SCPSkillNode[] {
        new SCPSkillNode("Add anxioty", 200),
    };
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion


    private void    Start                   ()
    {
        SkillsSet();
        murderConfigs.IncludedObjects.parentGameObject = this.gameObject;
        murderConfigs.OnDamaged         += SetAnxiotyIfDamaged;
        _sounds096Pack                  = GetComponent<sounds096>();
        _navmesh                        = GetComponent<NavMeshAgent>();
        _animSource                     = transform.GetChild(0).GetComponent<Animator>();
        _animSource.SetLayerWeight(1, 0);
        
    }
    private void    LateUpdate              ()
    {
        IsWannaWalking();
        CalmAnimation();
        if (_anxioty > 100)
        {
            Madness();
            _navmesh.speed = 20;
        }
        else
        {
            _navmesh.speed = 5;
            _animSource.SetLayerWeight(1, Mathf.Lerp(_animSource.GetLayerWeight(1), 0, 0.2f));
            _animSource.SetInteger("StateMadness", 2);
            _animSource.SetBool("IsMadness", false);
            _sounds096Pack.SaySound(0);
        }

        if( _anxioty < 50)                          skills[0].name = "Add anxioty";
        else if (_anxioty > 50 && _anxioty < 100)   skills[0].name = "Open face";
        else                                        skills[0].name = "...";
    }
    private void    FixedUpdate             ()
    {
        MainThreadHandler.AddActions(murderConfigs.CirclingHeart);
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        _anxioty = (_anxioty > 0 && _anxioty < 100) ? _anxioty - 1 : _anxioty;
        _madnessStage = _animSource.GetInteger("StateMadness") is 1 ? _madnessStage + 1 : _madnessStage;
    }
    private void    SetAnxiotyIfDamaged     ()
    {
        _anxioty += 60;
        murderConfigs.walking = 100;
    }
    private void    IsWannaWalking          ()
    {
        if (_navmesh.enabled is false || _navmesh.isOnNavMesh is false) return;
        if (murderConfigs.walking <= 45 || _anxioty <= 0)
        {
            _navmesh.SetDestination(transform.position);
            _animSource.SetBool("IsWalking", false);
            return;
        }
        if (murderConfigs.walking > 45)
        {
            _navmesh.SetDestination(murderConfigs.interestPoint);
            _animSource.SetBool("IsWalking", true);
        }
    }
    private void    CalmAnimation           ()
    {
        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 5, murderConfigs.allRaycastedObjects, nameof(Man), out GameObject nearMan);
        
        if(nearMan != null)
        {
            _lookAtMan.position = (nearMan.transform.position + transform.position)/2;
            _lookAtMan.transform.LookAt(nearMan.transform.position);
        }

        if (_animSource.GetInteger("StateMadness") >= 2)
        {
            _animSource.SetBool("IsCalm", _anxioty > 0 ? false : true);
            _rigs[0].weight = _anxioty > 90 ? Mathf.Lerp(_rigs[0].weight, 1, 0.02f) : Mathf.Lerp(_rigs[0].weight, 0, 0.02f);
            _rigs[1].weight = _anxioty > 90 ? Mathf.Lerp(_rigs[1].weight, 1, 0.015f) : Mathf.Lerp(_rigs[1].weight, 0, 0.015f);
        }
        else
        {            
            _rigs[0].weight =  Mathf.Lerp(_rigs[0].weight, 0, 0.02f);
            _rigs[1].weight =  Mathf.Lerp(_rigs[1].weight, 1, 0.015f);
        }
    }
    private void    Madness                 ()
    {
        _animSource.SetLayerWeight(1, Mathf.Lerp(_animSource.GetLayerWeight(1), 1, 0.2f));
        SearchAndRemember();
        FearAllMens();
        if (_allRememberedHumans.ToArray().Length <= 0)
        {
            _anxioty = 50;
            _madnessStage = 0;
            return;
        }
        if (_animSource.GetInteger("StateMadness") >= 2){
            _animSource.SetInteger("StateMadness", 0);
            _animSource.Play("Armature|BeingMadness", 1, 0);

        }
        else{
            if (_madnessStage < 580){
                murderConfigs.walking = 0;
                if(_navmesh.enabled && _navmesh.isOnNavMesh) _navmesh.SetDestination(transform.position);
                _sounds096Pack.SaySound(2);
            }
            else    RipAndTear();                       
        }

        if (_animSource.GetInteger("StateMadness").Equals(0) &&_animSource.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.8f)
        {
            _animSource.SetInteger("StateMadness", 1);
        }
    }
    private void    DoorSearching           ()
    {
        Collider[] DoorOverpass = Physics.OverlapSphere(transform.position, 10); //For NearObjUtilitiesV2
        GameObject door = null;
        NearObjUtilitiesStatic.NearestTargetTagNoRay(transform,DoorOverpass ,5,"DoorGates",out door);
        if (door != null)
            door.GetComponent<Animator>().SetInteger("Timer", 45);        
    }
    private void    RipAndTear              ()
    {
        _animSource.SetBool("IsMadness", true);
        _sounds096Pack.SaySound(1);
        DoorSearching();


        #region Search and update mans list
        GameObject nearRemembered = null;

        try{
            foreach (var item in _allRememberedHumans){
                if (item == null)
                    _allRememberedHumans.Remove(item);                
            }
            nearRemembered = new NearObjUtilitiesV2().NearestTargetNoRay(transform, _allRememberedHumans.ToArray(), 7, nameof(Man));
        }
        catch (System.InvalidOperationException){
            return;
        }
        #endregion

        if (_animSource.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            murderConfigs.walking = 0;
            if (_navmesh.enabled && _navmesh.isOnNavMesh) _navmesh.SetDestination(transform.position);
            return;
        }
        if (nearRemembered != null) TryKillThis(nearRemembered);
    }
    private void    SearchAndRemember       ()
    {
        #region Dev. commentary
        /*  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         *  Я ПИСАЛ ЭТО ГОВНО 3 ЕБАНЫХ ДНЯ ПОДРЯД
         *  Этот метод прост до безобразия и в нем фактически 8 строк, но я тупой дебил.
         *  Я реально как затупок сидел и думал, как написать запоминание людей в массив.
         *  Пиздец, мне стыдно.
         *  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         */
        #endregion
        GameObject[] AllObjects = new NearObjUtilities().RaycastedArrayByComponent(transform, 100, nameof(Man), 6);

        foreach (var current in AllObjects)
        {
            bool isDubl = false;
                foreach (var listedActive in _allRememberedHumans.ToArray())
                {
                    if (listedActive == current) isDubl = true; ;
                    continue;
                }
                if(!isDubl) _allRememberedHumans.Add(current);
            continue;
        }
    }
    private void    TryKillThis             (in GameObject target) 
    {
        murderConfigs.walking = 100;
        _navmesh.SetDestination(target.transform.position);
        if (_animSource.GetCurrentAnimatorStateInfo(1).IsTag("Attack")) return;
        if (Vector3.Distance(transform.position, target.transform.position) < 8)
        {
            bool attackAnimType = Random.Range(0, 100) > 50 ? true : false;
            _animSource.Play(attackAnimType is true ? "Attack" : "Attack_2",1);
            target.GetComponent<Man>().DClassConfigs.SetDamage(100,true);
        }
    }
    private void    FearAllMens             ()
    {
        GameObject[] allmensNear = new NearObjUtilities().RaycastedArrayByComponent(transform, 90, nameof(Man), 7);
        for (int i = 0; i < allmensNear.Length; i++)
        {
            InteractiveMethods.FearByObject(transform.position, allmensNear[i].transform);
        }
    }

    public void SkillsSet()
    {
        skills[0].isUsedSkill += () => {
            _anxioty += 70;
        };
    }
    public IAliveConfigs GetField() => murderConfigs;
}

using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("SCP/173 - sculpture")]
sealed class scp173 : MonoBehaviour, IAliveForm , IPassportData
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public      MurderProperties    murderconfigs;

    private     NavMeshAgent        _navMeshAgent;
    private     AudioSource         _moveSound;
    private     Rigidbody           _rigBody;

    [SerializeField] private     GameObject          _people;
    public      float               heigh { get => 7; }
    public      string              aliveName { get { return "SCP - 173"; } set { } }
    public      string              aliveSurname { get { return "Sculpture"; } set { } }
    public      short               aliveAges { get { return 0; } set { } }
    /*=========================================================================================================================================================*/
    #endregion

    private void    Awake                   (){
        murderconfigs.IncludedObjects.parentGameObject = this.gameObject;
        _navMeshAgent                   = this.GetComponent<NavMeshAgent>();
        _rigBody                        = this.GetComponent<Rigidbody>();
        _moveSound                      = GetComponent<AudioSource>();
        murderconfigs.allObjectSearchRadius = 400;
        murderconfigs.allObjectSearhHeigth  = 6;
    }

    private void    FixedUpdate             (){
        MainThreadHandler.AddActions(() => {murderconfigs.CirclingHeart();});
        murderconfigs.CheckDie(murderconfigs.IncludedObjects.parentGameObject);
        _people = new NearObiUtilitiesSimple().NearestTargetComponent(transform, 8, murderconfigs.allRaycastedObjects, nameof(Man));

        if (_navMeshAgent.enabled is false || _navMeshAgent.isOnNavMesh is false) return;

        Movements();
        try
        {
            ifManBravery_lookAtMe(murderconfigs.allRaycastedObjects);
            ifManBravery_FearAndRun(murderconfigs.allRaycastedObjects);
        }
        catch
        {

        }
    }
    private void    Movements               (){
        if (GetComponent<DangerChecker>().discoveredTimer < 5 && _navMeshAgent.enabled){
            _navMeshAgent.SetDestination(murderconfigs.interestPoint);
            murderconfigs.walking = 85;
            _moveSound.volume = 1;
        }   //If not discovered - Can move;

        else if (GetComponent<DangerChecker>().discoveredTimer >= 5 && _navMeshAgent.enabled){
            _navMeshAgent.SetDestination(transform.position);
            murderconfigs.walking = 0;
            _moveSound.volume = 0;
        }   //If discovered - stay and don't moving

        if (_people != null && GetComponent<DangerChecker>().discoveredTimer < 5){
            _navMeshAgent.SetDestination(_people.transform.position);
            _navMeshAgent.speed = 128;
            SearchMens();
        }   //If man has been discovered and you can move - move toward man and try kill it

        else if (_people == null){
            _navMeshAgent.speed = 32;
        }   //If man's hasn't been discovered - Can move;
    }
    private void    SearchMens              (){
        if (Vector3.Distance(_people.transform.position, transform.position) >= 5) return;
        transform.position = _people.transform.position;
        _people.GetComponent<Man>().DClassConfigs.KillForm(true);
    } //Kill the D-class instance    
    private void    ifManBravery_lookAtMe   (in Collider[] _colliders){
        for (ushort index = 0; index < _colliders.Length; index++)
        {
            if (_colliders[index].GetComponent<Man>())
            {
                Vector3 lookAtMe = new Vector3(transform.position.x, _colliders[index].transform.position.y, transform.position.z);
                _colliders[index].transform.LookAt(lookAtMe);
            }
        }
    }
    private void    ifManBravery_FearAndRun (in Collider[] _colliders){
        for (ushort index = 0; index < _colliders.Length; index++)
        {
            if (_colliders[index].GetComponent<Man>().bravery > 77 || Vector3.Distance(_colliders[index].transform.position,transform.position)>17) return;
            Vector3 lookAtMe = new Vector3(transform.position.x, _colliders[index].transform.position.y, transform.position.z);

            InteractiveMethods.FearByObject(transform.position, _colliders[index].transform);
        }
    }

    public IAliveConfigs GetField     () => murderconfigs;
}

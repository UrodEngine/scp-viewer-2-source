using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[AddComponentMenu("SCP/457 - fire Man")]
public class scp457 : MonoBehaviour,IAliveForm, IPassportData
{
    [SerializeField] private MurderProperties     murder;
    [SerializeField] private GameObject scp457_miniPrefab;
    private Rigidbody       rigidBody       => GetComponent<Rigidbody>();
    private short           burnLevel;
    public  float           heigh           { get => 8; }
    public  string          aliveName { get { return "SCP - 457"; } set { } }
    public  string          aliveSurname { get { return "The Burning Man"; } set { } }
    public  short           aliveAges { get { return 0; } set { } }

    private void Start()
    {
        murder.IncludedObjects.parentGameObject = this.gameObject;
        burnLevel = 500;
    }
    private void FixedUpdate()
    {
        burnLevel = burnLevel > (short)0 ? (short)(burnLevel - 1) : burnLevel;
        MainThreadHandler.AddActions(murder.CirclingHeart);
        murder.CheckDie(murder.IncludedObjects.parentGameObject);

        NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform,heigh,murder.allRaycastedObjects,out GameObject victim);

        WalkingAnimationUtility.Animate(murder.components.animator, murder.components.navMeshAgent, rigidBody, 2, 0.015f, 2, 0.1f);
        if (murder.walking > 60)
            WannaWalking();
        if (victim != null){
            murder.interestPoint = victim.transform.position;
            IsVictimNear(victim);
            murder.walking = 100;
        }

        if (burnLevel <= 0)
        {
            Instantiate(scp457_miniPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void IsVictimNear(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 15) return;
        target.TryGetComponent<IAliveForm>(out IAliveForm form);
        AliveForm field;
        new AliveFormFieldGetter(form, out field);
        field.properties.heatLevel = 200;
        field.properties.health = (short)(field.properties.health / 1.05f);
        DebaffsListContainer.instance.AddObject(target,DebaffsListContainer.burningObjects);
        burnLevel = 500;
    }
    private void WannaWalking()
    {
        if (murder.components.navMeshAgent.enabled && murder.components.navMeshAgent.isOnNavMesh)
            murder.components.navMeshAgent.SetDestination(murder.interestPoint);
    }

    public IAliveConfigs GetField()
    {
        return murder;
    }
}

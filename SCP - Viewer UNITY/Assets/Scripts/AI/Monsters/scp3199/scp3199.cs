using UnityEngine;

[AddComponentMenu("SCP/3199 - two leg's without feathers"), DisallowMultipleComponent]
public sealed class scp3199 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    #region alterable values
    //-----------------------------------------------------------------------------------
    public MurderProperties murderConfigs = new MurderProperties();

    public float    heigh           { get => 4; }
    public string   aliveName       { get { return "SCP - 1048"; } set { } }
    public string   aliveSurname    { get { return "Two leg's without feathers"; } set { } }
    public short    aliveAges       { get { return 2; } set { } }
    public SCPSkillNode[] skills    { get; set; } = new SCPSkillNode[] 
    {
        new SCPSkillNode("Spawn egg",10000),
        new SCPSkillNode("Spawn more eggs", 50000)
    };

    private Rigidbody @rigidBody;

    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private GameObject burnedPrefab;
    //-----------------------------------------------------------------------------------
    #endregion

    private void            Start       ()
    {
        murderConfigs.IncludedObjects.parentGameObject = this.gameObject;
        @rigidBody = GetComponent<Rigidbody>();
        SkillsSet();
        murderConfigs.OnDamaged += () => { murderConfigs.walking = 100; };
        murderConfigs.OnDying += () => { skills[0].UseSkill(); };
    }
    private void            FixedUpdate ()
    {
        Man nearestMen = null;
        MainThreadHandler.AddActions(()=> { murderConfigs.CirclingHeart(); });
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        WalkingAnimationUtility.Animate(murderConfigs.components.animator, murderConfigs.components.navMeshAgent, @rigidBody, 1, 0.1f, 1, 0.1f);
        if (murderConfigs.walking > 25)
        {
            NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, heigh, murderConfigs.allRaycastedObjects, out GameObject result);
            if (result != null)
            {
                nearestMen = result.GetComponent<Man>();
            }

            if (murderConfigs.components.navMeshAgent.isActiveAndEnabled && murderConfigs.components.navMeshAgent.isOnNavMesh)
            {
                if (nearestMen != null)
                {
                    murderConfigs.components.navMeshAgent.SetDestination(nearestMen.transform.position);
                }
                else
                {
                    murderConfigs.components.navMeshAgent.SetDestination(murderConfigs.interestPoint);
                }
            }
            else
            {
                murderConfigs.interestPoint = transform.position;
            }            
        }
        if (nearestMen != null && Vector3.Distance(transform.position, nearestMen.transform.position)<4.5f && !murderConfigs.components.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            murderConfigs.components.animator.Play("scp3199armature|Attack");
            murderConfigs.walking = 0;
            murderConfigs.components.navMeshAgent.velocity = Vector3.zero;
            rigidBody.velocity = Vector3.zero;
            if (nearestMen.DClassConfigs.properties.health < 40)
            {
                GameObject burned = Instantiate(burnedPrefab, nearestMen.gameObject.transform.position, nearestMen.gameObject.transform.rotation);
                Destroy(nearestMen.gameObject);
            }
            else
            {
                nearestMen.DClassConfigs.SetDamage(35, true);
            }
        }


    }
    public  IAliveConfigs   GetField    ()
    {
        return murderConfigs;
    }
    public  void            SkillsSet   ()
    {
        skills[0].isUsedSkill += () => 
        {
            GameObject egg = Instantiate(eggPrefab, transform.position+new Vector3(0,heigh,0), transform.rotation);
        };
        skills[1].isUsedSkill += () => 
        {
            StartCoroutine(Eggs());
        };
    }

    private System.Collections.IEnumerator Eggs()
    {
        for (byte i = 0; i < Random.Range(5, 17); i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject egg = Instantiate(eggPrefab, transform.position + new Vector3(0, heigh, 0), transform.rotation);
            egg.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(0f, 6f), Random.Range(0f, 6f), Random.Range(0f, 6f));
        }
    }
}

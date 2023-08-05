using UnityEngine;

[AddComponentMenu("SCP/035 - mask of madness")]
public class scp035 : MonoBehaviour, IPassportData
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private SkinContainer[]    DclassSkinChanger;
    [SerializeField] private GameObject         prefabAsMe;
    public string   aliveName       { get { return "SCP - 035"; } set { } }
    public string   aliveSurname    { get { return "Obsession mask"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    public GameObject men;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void Awake()
    {
        prefabAsMe = PrefabManager.GetManagerByKey("monsters").GetPrefab("SCP-035");
    }
    private void FixedUpdate()
    {
        MainThreadHandler.AddActions(() => 
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 60);
            Collider[] raycasted = new NearObiUtilitiesSimple().SimpleRaycastAll(transform.position, colliders, 60, 1);
            NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 1, raycasted, nameof(Man), out men);
        });


        try
        {
            if (men != null)
            {
                Man dclass = men.GetComponent(nameof(Man)) as Man;
                dclass.DClassConfigs.interestPoint = transform.position;
                dclass.DClassConfigs.walking = 100;
                if (Vector3.Distance(transform.position, men.transform.position) < 8)
                {
                    TakeByDclass(dclass);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.StackTrace);
        }
    }
    private void TakeByDclass(in Man target)
    {   
        for (short i = 0; i < DclassSkinChanger.Length; i++)
        {
            if(DclassSkinChanger[i].manType == target.ManType)
            {
                target.DClassConfigs.components.skinnedMeshRenderer.material.mainTexture = DclassSkinChanger[i].texture;
                target.HatSlot.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                target.HatSlot.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;

                GameObject particle = Instantiate(transform.GetChild(0).gameObject, target.HatSlot.transform.GetChild(0));

                if (target.DClassConfigs.IncludedObjects.drop == null || target.DClassConfigs.IncludedObjects.drop.Length <= 0)
                {
                    target.DClassConfigs.IncludedObjects.drop       = new AliveForm.GameObjectPrefabs.Drop[1];
                    for (int x = 0; x < target.DClassConfigs.IncludedObjects.drop.Length; x++)
                    {
                        target.DClassConfigs.IncludedObjects.drop[x] = new AliveForm.GameObjectPrefabs.Drop();
                    }
                }
                target.DClassConfigs.IncludedObjects.drop[0].item           = PrefabManager.GetManagerByKey("monsters").GetPrefab("SCP-035");
                target.DClassConfigs.IncludedObjects.drop[0].dropChance     = 100;

                target.DClassConfigs.SetStan(150);
                target.transform.gameObject.AddComponent<scp035Dclass>();
                target.EnemiesType = new Man.ManTypeEnum[] { Man.ManTypeEnum.Security };

                Destroy(this.gameObject);
                return;
            }
        }
    }
    [System.Serializable] private class SkinContainer
    {
        public Man.ManTypeEnum manType;
        public Texture texture;
    }
}

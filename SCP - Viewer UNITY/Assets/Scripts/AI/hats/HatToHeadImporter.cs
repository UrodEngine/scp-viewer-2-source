using UnityEngine;
using System.IO;
public sealed class HatToHeadImporter : MonoBehaviour
{
    public  static GameObject[]  hats;
    public  static bool          updated = false;
    private static UnityEngine.SceneManagement.Scene rememberedScene;
    
    public void Start       ()
    {
        if (rememberedScene != gameObject.scene)
        {
            rememberedScene = gameObject.scene;
            updated = false;
        }
        
        MeshFilter child = transform.GetChild(0).GetComponent<MeshFilter>();
        
        if (!updated) GetAllMeshesAsStaticArray();

        if (hats.Length <= 0)
        {
            child.mesh = null;
        }
        else
        {
            child.mesh = hats[Random.Range(0, hats.Length)].GetComponent<MeshFilter>().sharedMesh;
        }
    }
    private void GetAllMeshesAsStaticArray  ()
    {
        updated = true;

        hats = PrefabManager.GetManagerByKey("hats").prefabs;

        System.Collections.Generic.List<GameObject> filteredHats = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < hats.Length; i++)
        {
            if (File.Exists(ModsDirectories.hats + "/" + hats[i].name))
            {
                if (File.ReadAllText(ModsDirectories.hats + "/" + hats[i].name).Length > 0)
                {
                    filteredHats.Add(hats[i]);
                    Debug.Log($"hat exist and activated: {hats[i].name}");
                }
            }
        }
        hats = filteredHats.ToArray();
    }
}

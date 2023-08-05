using UnityEngine.SceneManagement;
using UnityEngine;

public sealed class MapModsUnloaderOnStart : MonoBehaviour
{
    void Start()
    {
        try
        {
            MapModsWindowList.assetBundleMapFile.Unload(false);
        }
        catch
        {
            MapModsWindowList.assetBundleMapFile = null;
        }
        finally
        {
            MapModsWindowList.assetBundleMapFile = null;
            MapModsWindowList.selectedFile = null;
        }
    }
}

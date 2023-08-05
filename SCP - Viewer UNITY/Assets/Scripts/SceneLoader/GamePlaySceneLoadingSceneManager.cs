using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;

[DisallowMultipleComponent]
public sealed class GamePlaySceneLoadingSceneManager : MonoBehaviour
{
    #region alterable values
    public UnityEngine.UI.Image     progress_bar;
    public GameObject               maincamera;
    public GameObject[]             otherOnLoad;

    public static float savedLoadProcent;
    #endregion

    
    private void Start()
    {
        LoadROOM(LoadLevel.LoadLevelname);
    }
    private void LoadROOM(string sceneName)
    {       
        StartCoroutine(LoadScene(sceneName));       
    }
    private IEnumerator LoadScene(string InputName)
    {
        AsyncOperation LevelLoadAsyncOP;
        if (MapModsWindowList.assetBundleMapFile != null)
        {
            LevelLoadAsyncOP = SceneManager.LoadSceneAsync(MapModsWindowList.assetBundleMapFile.GetAllScenePaths()[0], LoadSceneMode.Additive);          
        }
        else
        {
            LevelLoadAsyncOP = SceneManager.LoadSceneAsync(InputName, LoadSceneMode.Additive);
        }
        while (LevelLoadAsyncOP.progress < 1)
        {
            progress_bar.fillAmount = LevelLoadAsyncOP.progress;
            savedLoadProcent        = LevelLoadAsyncOP.progress;
            yield return null;
        }

        Task.Run(async () => 
        { 
            await Task.Delay(2700);
            MainThreadHandler.AddActions(() =>
            {
                foreach (var item in otherOnLoad)
                {
                    item.SetActive(true);
                }
                Destroy(gameObject);
                GameObject newcam = Instantiate(maincamera, new Vector3(0, 0, 0), Quaternion.identity);

            },priority:0);
        });      
    }
}
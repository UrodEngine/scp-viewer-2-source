using UnityEngine;

public sealed class DeleteSceneOnStart : MonoBehaviour
{
    public string nameScene;
    private void Start()
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(nameScene);
        }
        catch
        {
            return;
        }
    }


}

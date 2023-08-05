using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public static string LoadLevelname;

    public void LevelLoading(string setname){
        LoadLevel.LoadLevelname = setname;
        Map_select.CurrentMapSelect = setname;
        SceneManager.LoadScene("Gameplay");
    }

    public void LevelLoadingAUTO(){
        LoadLevel.LoadLevelname = Map_select.CurrentMapSelect;
        SceneManager.LoadScene("Gameplay");
    }
}

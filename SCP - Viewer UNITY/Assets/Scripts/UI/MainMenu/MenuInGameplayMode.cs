using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuInGameplayMode : MonoBehaviour{
    public void GoInMenu()
    {
        PlayerPrefs.SetInt("InterADS_turn", PlayerPrefs.GetInt("InterADS_turn") + 1);
            
        SceneManager.LoadScene("MainMenu");
    }
    public void KillAllInteractables()
    {
        GameObject[] interactableArray = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject interactable in interactableArray)
        {
            Destroy(interactable);
        }

        GameObject[] worldInteractiveArray = GameObject.FindGameObjectsWithTag("W_Interactive");
        foreach (GameObject interactive in worldInteractiveArray)
        {
            Destroy(interactive);
        }
    }
    public void KillOnlyEntities()
    {
        Man[]            DclassesEntities    =   GameObject.FindObjectsOfType<Man>();
        DangerChecker[]     DangerCheckers      =   GameObject.FindObjectsOfType<DangerChecker>();

        foreach (Man         dclass  in DclassesEntities)    {Destroy(dclass.gameObject);}
        foreach (DangerChecker  dangers in DangerCheckers)      {Destroy(dangers.gameObject);}
    }
}
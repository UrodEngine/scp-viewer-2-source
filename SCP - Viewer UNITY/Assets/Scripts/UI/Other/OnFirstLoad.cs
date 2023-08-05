using UnityEngine;

public class OnFirstLoad : MonoBehaviour
{
    public GameObject gamobj;
    void Start(){
        PlayerPrefs.SetInt("Tutor", PlayerPrefs.GetInt("Tutor") + 1);

        if (PlayerPrefs.GetInt("Tutor") > 3) gamobj.SetActive(false);
    }
}

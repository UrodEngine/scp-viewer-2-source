using UnityEngine;

public class Text_version : MonoBehaviour
{
    private UnityEngine.UI.Text text => GetComponent<UnityEngine.UI.Text>();
    void Update(){
        text.text = "Version: " + Application.version;
    }
}

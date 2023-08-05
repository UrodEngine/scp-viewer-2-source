using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class CheckPermissions : MonoBehaviour
{
    private Text textComponent;

    private void OnEnable()
    {
        textComponent = transform.GetComponent<Text>();

        textComponent.text = "Permissions:\n<color=grey><size=120>--------------------------------------------------</size></color>\n";

        textComponent.text += Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)    ? "<size=180><color=#636363>ExternalStorageRead</color></size>\n" : "<size=180><color=red>ExternalStorageRead</color></size>\n";
        textComponent.text += Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)   ? "<size=180><color=#636363>ExternalStorageWrite</color></size>\n" : "<size=180><color=red>ExternalStorageWrite</color></size>\n";
        //textComponent.text += Permission.HasUserAuthorizedPermission(Permission.Camera)                 ? "<size=180><color=grey>Camera</color></size>\n"               : "<size=180><color=red>Camera</color></size>\n";

        textComponent.text += "<color=grey><size=120>--------------------------------------------------</size></color>\n";
    }
}

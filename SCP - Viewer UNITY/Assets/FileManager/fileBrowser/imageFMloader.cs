using UnityEngine;

public class imageFMloader : MonoBehaviour
{
    public string playerPrefsRead;
    private string tempPath;
    private void FixedUpdate()
    {
        
        if (tempPath != PlayerPrefs.GetString(playerPrefsRead))
        {
            tempPath = PlayerPrefs.GetString(playerPrefsRead);
            StartCoroutine(setRaw());
        }
        
    }
    System.Collections.IEnumerator setRaw()
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequestTexture.GetTexture("file:///" + PlayerPrefs.GetString(playerPrefsRead));
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            GetComponent<UnityEngine.UI.RawImage>().texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(uwr);
        
    }
}

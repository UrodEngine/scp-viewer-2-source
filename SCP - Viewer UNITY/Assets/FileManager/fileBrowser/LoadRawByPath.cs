using UnityEngine;

public class LoadRawByPath : MonoBehaviour
{
    public string   path;
    UnityEngine.UI.RawImage raw;
    public void     OnClick()
    {
        AvatarLoad.window.SavePlayerPrefsPath(path);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_FILE_MANAGER_PREVIEWS) is 1) StartCoroutine(SetPreview(path));
        raw = GetComponent<UnityEngine.UI.RawImage>();
    }

    private System.Collections.IEnumerator SetPreview(string previewPath)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequestTexture.GetTexture("file:///" + previewPath);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            raw.texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(uwr);
        }
    }
}

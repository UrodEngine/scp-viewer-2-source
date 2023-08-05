using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

[DisallowMultipleComponent]
public class DownloadStarterKitMods : MonoBehaviour
{
    #region alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /// <summary>
    /// Самой первой ссылкой с <see langword="contentinfo.html"/> для стартового набора модов считается:<br/>
    /// <see href="https://urodengine.gamejolt.io/SCPV2/contentinfo.html"/>
    /// </summary>
    [SerializeField] private string                 downloadUrl;
    /// <summary>
    /// Самой первой ссылкой с <see langword="/.."/> для стартового набора модов считается:<br/>
    /// <see href="https://urodengine.gamejolt.io/SCPV2/"/><br/>
    /// Эта ссылка описывает корневую папку всего контента, с которой начинается поиск файлов, взятых с <see langword="contentinfo.html"/>
    /// </summary>
    [SerializeField] private string                 rootUrl;

    [SerializeField,TextArea]   private string      contentList;
    [SerializeField]            private string[]    contentElements;
    [SerializeField]            private string      currentElement;
    [SerializeField]            private float       downloadProgress;
    [SerializeField]            private ulong       downloadBytesLength;

    public  string  currentElementField         => currentElement;
    public  ulong   downloadBytesLengthField    => downloadBytesLength;

    private bool    contentinfoDownloaded;
    private bool    contentInfoDownloadedField { get => contentinfoDownloaded; set
        {
            contentinfoDownloaded = value;
            contentElements = contentList.Split('\n');
            contentList = null;
            StartCoroutine(DownloadContent());
        } 
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion



    public void         Run                 ()
    {
        StartCoroutine(ReadContentInfoHtml());
    }
    public IEnumerator  ReadContentInfoHtml ()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(downloadUrl);

        yield return webRequest.SendWebRequest();

        downloadProgress    = webRequest.downloadProgress;
        contentList         = webRequest.downloadHandler.text;

        if (webRequest.result is UnityWebRequest.Result.Success)
        {
            contentInfoDownloadedField = true;
        }
        webRequest.Dispose();
    }
    public IEnumerator  DownloadContent     ()
    {
        for (ushort i = 0; i < contentElements.Length; i++)
        {
            currentElement  = contentElements[i];
            if (CheckDirectory(contentElements[i]))
            {
                Debug.Log("HTML Get break, same file already exist");
                continue;
            }
            UnityWebRequest webRequest = UnityWebRequest.Get(rootUrl+contentElements[i]);

            yield return webRequest.SendWebRequest();

            downloadProgress    = webRequest.downloadProgress;
            downloadBytesLength = webRequest.downloadedBytes;

            Debug.Log(rootUrl + contentElements[i]);
            Debug.Log(webRequest.downloadedBytes + " | " + webRequest.downloadHandler.text);

            ModsDirectories.CreateFile(contentElements[i], webRequest.downloadHandler.data);
        }
    }

    public  static bool CheckDirectory      (string contentElement)
    {
        contentElement      = contentElement.Trim();
        string[] splitted   = contentElement.Split('/');

        if      (contentElement.Contains("/maps/"))     return CheckFile(ModsDirectories.maps       + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/props/"))    return CheckFile(ModsDirectories.props      + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/entities/")) return CheckFile(ModsDirectories.entities   + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/monsters/")) return CheckFile(ModsDirectories.monsters   + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/weapons/"))  return CheckFile(ModsDirectories.weapons    + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/avatars/"))  return CheckFile(ModsDirectories.avatars    + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/other/"))    return CheckFile(ModsDirectories.other      + "/" + splitted[splitted.Length - 1]);
        else if (contentElement.Contains("/tools/"))    return CheckFile(ModsDirectories.tools      + "/" + splitted[splitted.Length - 1]);
        else
        {
            Debug.Log($"(!) - CheckDirectory failed to find which directory must be as main: " + contentElement);
            return false;
        };
    }
    private static bool CheckFile           (in string path)
    {
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }
}

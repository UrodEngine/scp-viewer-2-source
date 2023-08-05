using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public sealed class AvatarLoad : MonoBehaviour
{
    #region Alterable values
    //======================================================================================
    public              static int          filesCount;
    public              GameObject          filesAvatarList, filesContent, filePrefab;
    public              string              playerPrefsKeyName;
#if UNITY_EDITOR
    private             DirectoryInfo       dirInfo = new DirectoryInfo("C:tmp");
#else
    private             DirectoryInfo       dirInfo = new DirectoryInfo(Application.persistentDataPath+"/Mods/Avatars/");
#endif
    private             FileInfo[]          fileInfo;

    [SerializeField] private    string          path;       /*    /Mods/Avatars/  */
    [SerializeField] private    Text            pathText;
    [SerializeField] private    Text            windowTitle;
    [SerializeField] private    Text            informationsText;
    public  static              AvatarLoad      window;

    //======================================================================================
    #endregion
    private void        OnEnable            (){
        dirInfo                 = new DirectoryInfo(Application.persistentDataPath + path);
        window                  = this;
        pathText.text           = $"<{dirInfo.ToString()}> is empty. 0 files was finded";
        windowTitle.text        = $"<color=red>{playerPrefsKeyName}</color>\nFolder";
        informationsText.text   = $"Current folder is:<color=#0010AB>{dirInfo.FullName}</color>";
        LoadAvatarList();
    }
    private void        OnDisable           ()
    {
        StopCoroutine(LoadPrefabsFromIoFiles());
        for (int i = 0; i < filesContent.transform.childCount; i++)
        {
            Destroy(filesContent.transform.GetChild(i).gameObject);
        }
    }
    private void  LoadAvatarList      ()
    {
        try
        {
            fileInfo = new string[] { "*.jpeg", "*.jpg", "*.png" }.SelectMany(OUTPUT => dirInfo.GetFiles(OUTPUT, SearchOption.AllDirectories)).ToArray();

            if (fileInfo.Length <= 0)
            {
                pathText.gameObject.SetActive(true);
            }
            else
            {
                pathText.gameObject.SetActive(false);
            }

            StartCoroutine(LoadPrefabsFromIoFiles());
        }
        catch
        {
            pathText.gameObject.SetActive(true);
            pathText.text = "This directory is wrong";
        }

    }    
    public System.Collections.IEnumerator LoadPrefabsFromIoFiles()
    {  
        foreach (FileInfo fileInfo in fileInfo)
        {
            yield return new WaitForSeconds(FPSCounter.fps > 30 ? 0.03f : 0.16f);
            filesCount++;
            GameObject fileBro = Instantiate(filePrefab, filesContent.transform);

            fileBro.name = fileInfo.Name;
            fileBro.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name;
            fileBro.GetComponent<LoadRawByPath>().path = fileInfo.FullName;           
        }
    }
    public  void        SavePlayerPrefsPath (in string fullName)    => PlayerPrefs.SetString(playerPrefsKeyName, fullName);
    public  void        ChangePath          (in string newPath)     => path = newPath;
}

using UnityEngine;
using System.IO;
[AddComponentMenu("Start Tools/Toggle object active if directory exist")]
public sealed class ToggleObjectActiveIfDirectoryExist : MonoBehaviour
{
    [Header("Example: /Mods/Maps")]
    [SerializeField] private string path = "/Mods/Maps";
    void OnEnable()
    {
        if (Directory.Exists(Application.persistentDataPath + path))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath + path);
            if (dirInfo.GetFiles().Length > 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
    }
}

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public sealed class MapModsWindowList : MonoBehaviour
{
    public static string selectedFile;
    public static AssetBundle assetBundleMapFile;

    [Header("Example: /Mods/Maps")]
    [SerializeField] private string path = "/Mods/NULL";

    [SerializeField] private Transform  content;
    [SerializeField] private GameObject template;

    [SerializeField] private Text selectedTitle;
    [SerializeField] private GameObject runButton;

    
    private void Start()
    {
        template.gameObject.SetActive(false);
        runButton.gameObject.SetActive(false);

        runButton.GetComponent<Button>().onClick.AddListener(() => 
        {
            if (selectedFile!=null)
            {
                assetBundleMapFile = AssetBundle.LoadFromFile(selectedFile);
                string[] scenes = assetBundleMapFile.GetAllScenePaths();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
            }
        });
    }
    private void OnEnable()
    {
        #region clearOldList
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        UpdateRunButton();
        #endregion

        if (Directory.Exists(Application.persistentDataPath + path))
        {
            FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + path).GetFiles();

            for (UInt16 i = 0; i < files.Length; i++)
            {
                Button selectMapFromModButton = Instantiate(template, content).GetComponent<Button>();
                selectMapFromModButton.gameObject.SetActive(true);
                selectMapFromModButton.transform.GetChild(0).GetComponent<Text>().text = $"{files[i].Name} <color=#5555ff77><{(float)(files[i].Length / 1024f / 1024f)} mb></color>";

                GameObject ID = new GameObject($"{i}");
                ID.transform.parent = selectMapFromModButton.transform;

                selectMapFromModButton.onClick.AddListener(() =>
                {
                    int id = int.Parse(selectMapFromModButton.transform.GetChild(1).name);
                    selectedTitle.text = $"Selected: <color=#550000ff>{files[id].Name} <{(float)(files[id].Length / 1024f / 1024f)} mb></color>";
                    selectedFile = files[id].FullName;
                    UpdateRunButton();
                });
            }
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);           
            return;
        }
    }
    private void UpdateRunButton()
    {
        if (selectedFile != null)
        {
            runButton.gameObject.SetActive(true);
        }
        else
        {
            runButton.gameObject.SetActive(false);
        }
    }
}

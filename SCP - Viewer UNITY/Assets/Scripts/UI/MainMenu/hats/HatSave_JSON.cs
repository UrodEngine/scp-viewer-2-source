using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
public class HatSave_JSON : MonoBehaviour
{
    #region Alterable values
    /*==========================================================================================*/
    public string           DataPath;
    public int              selectedHat;
    public AdsHatsCounter    adshatcount;
    [System.Serializable]
    public  class           HatsJsoner {public List<int> SavedSlots = new List<int>();}
    public  HatsJsoner      hatsjsoner = new HatsJsoner();
    public  HatsList       hatslist;
    /*==========================================================================================*/
    #endregion


    void Awake              ()
    {
        DataPath = Application.persistentDataPath + "/Hats.dll";
        
    }
    void CheckArray         ()
    {
        if (hatsjsoner.SavedSlots.ToArray().Length == 0)
        {
            hatsjsoner.SavedSlots.Add(selectedHat);
            Debug.Log("Added hat");
        }
        else
        {
            for (int i = 0; i < hatsjsoner.SavedSlots.ToArray().Length; i++){
                if (hatsjsoner.SavedSlots[i] == selectedHat){
                    Debug.Log("You already have this hat");
                    adshatcount.countADS_HATS++;
                    return;
                }
                continue;
            }
            Debug.Log("Saved hat");
            hatsjsoner.SavedSlots.Add(selectedHat);
        }
    }
    public void Start       ()
    {
        
        GetComponent<Button>().onClick.AddListener(delegate { SaveHat(); });

        if(File.Exists(DataPath)) {
            hatsjsoner = JsonUtility.FromJson<HatsJsoner>(File.ReadAllText(DataPath));
           
            GlobalHats.takedhat = hatsjsoner.SavedSlots.ToArray();


        }//Application.dataPath + "/Resources/Hats_save/Hats.dll"
    }
    public void SaveHat     ()
    {
        if (selectedHat < 0) return;
        CheckArray();            
        File.WriteAllText(DataPath, JsonUtility.ToJson(hatsjsoner));
        GlobalHats.takedhat = hatsjsoner.SavedSlots.ToArray();       
    }
    public void SelectHat   (int slot)
    {
        selectedHat = slot;
    }
}


public static class GlobalHats
{
    public static int[] takedhat;
    public static bool[] enabledhat;
    public static Mesh[] meshes;
}

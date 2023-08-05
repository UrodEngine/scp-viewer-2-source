using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using Task = System.Threading.Tasks.Task;


[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class HatsList : MonoBehaviour
{
    #region Alterable values
    /*==========================================================================================*/
    private Mesh[]                  finded;
    public  HatsShopButton[]        hats;
    public  string                  DataPath;
    public  Vector2                 DefaultSizeOfSlot;
    public  GameObject              ButtonInstance;
    public  List<RectTransform>     buttons = new List<RectTransform>();
    public  RectTransform           RectPanel;

    public  Color                   enabledhatColor;                //HatButton_Enabled
    public  static Color            enabledhatcolorStatic;

    public  Color                   enabledhatColorCheckBox;        //CheckBox_Enabled
    public  static Color            enabledhatcolorStaticCheckBox;

    [System.Serializable]
    public  class                   EnabledHats { public bool[] enabledHatsArray; }
    public  EnabledHats             enabledHats;

    /*==========================================================================================*/
    #endregion

    private void Start                  ()
    {
        finded = new Mesh[hats.Length];
        for (int i = 0; i < finded.Length; i++)
        {
            finded[i] = hats[i].hatMesh;
        }

        enabledhatcolorStatic           = enabledhatColor;
        enabledhatcolorStaticCheckBox   = enabledhatColorCheckBox;
        enabledHats.enabledHatsArray    = new bool[finded.Length];
        GlobalHats.enabledhat           = new bool[finded.Length];

        GlobalHats.meshes = finded;

        DataPath                = Application.persistentDataPath + "/HatsActivated.dll";
        RectPanel.sizeDelta     = DefaultSizeOfSlot * finded.Length; //Button panel height
        CreateButton(finded.Length);

        if (File.Exists(DataPath)){
            
            enabledHats = JsonUtility.FromJson<EnabledHats>(File.ReadAllText(DataPath));

            GlobalHats.enabledhat   = enabledHats.enabledHatsArray;
            
            if (GlobalHats.enabledhat is null || GlobalHats.enabledhat.Length <= 0)
            {
                return;
            }
            else
            {
                bool[] filteredEnabledHatsArray = new bool[finded.Length];
                for (int i = 0; i < GlobalHats.enabledhat.Length; i++)
                {
                    filteredEnabledHatsArray[i] = GlobalHats.enabledhat[i];
                }
                enabledHats.enabledHatsArray = filteredEnabledHatsArray;
                GlobalHats.enabledhat = filteredEnabledHatsArray;
            }
        }
    }
    private void Update                 ()
    {
        InteractiveButtons();
    }
    private void CreateButton           (in int slots)
    {
        for (int i = 0; i < slots; i++)
        {
            GameObject      button      = Instantiate(ButtonInstance, transform);
            RectTransform   btnRect     = button.GetComponent<RectTransform>();
            btnRect.anchoredPosition    = RectPanel.anchoredPosition - new Vector2(20, (btnRect.rect.height / 2) + DefaultSizeOfSlot.y * i); //Set button position
            button.name = $"BTN_{hats[i].name}";
            button.GetComponent<Hat_select>().HatSlot = i;
            SetButtonText(i, button);
            SetButtonMeshReference(i, button);
            buttons.Add(button.GetComponent<RectTransform>());
        }
    }
    private void SetButtonText          (in int slots, in GameObject button)
    {
        Text texter = button.transform.GetChild(0).transform.GetComponent<Text>();
        texter.text = hats[slots].name;
    }
    private void SetButtonMeshReference (in int slots, in GameObject button)
    {
        button.GetComponent<Hat_select>().RefMesh = hats[slots].hatMesh;
    }
    public  void SaveEnabledHatArray    ()
    {
        Task.Run(async () => 
        {
            await Task.Delay(55);
            File.WriteAllText(DataPath, JsonUtility.ToJson(enabledHats));
            Debug.Log($"Saved {nameof(enabledHats)} class. DataPath: {DataPath}");
        });        
    }
    private void InteractiveButtons     ()
    {
        if (buttons is null)
        {
            return;
        }
        foreach (RectTransform rectT in buttons)
        {
            if (GlobalHats.takedhat != null)
            {
                for (ushort i = 0; i < GlobalHats.takedhat.Length; i++)
                {
                    if (rectT.GetComponent<Hat_select>().HatSlot >= 0 && rectT.GetComponent<Hat_select>().HatSlot == GlobalHats.takedhat[i])
                    {
                        rectT.GetComponent<Image>().color = enabledhatColor;
                    }  
                }
            }                
        }
    }  

    [System.Serializable]
    public class HatsShopButton
    {
        public string   name;
        public Mesh     hatMesh;
    }
}

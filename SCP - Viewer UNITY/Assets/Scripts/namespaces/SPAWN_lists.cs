// Список объектов. доступных для спавна.
// Имеет три массива.
// Props    - Декор, обычно бесполезен, но есть объекты, с которыми можно взаимодействовать.
// NPC      - Люди или альтернативные версии людей (Обычно зомби)
// SCP      - Непосредственно то, из-за чего существует игра

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SPAWN_lists : MonoBehaviour
{
    public SPAWN_lists()
    {
        instance = this;
    }

    #region alterable values
    //=========================================================================================================
    public  static  SPAWN_lists         instance;

    public          List<GameObject>    PropsArray;     //99% useless
    public          List<GameObject>    NPCArray;       //NPC 
    public          List<GameObject>    SCPArray;       //SCP
    public          List<GameObject>    WeaponsArray;   //Weapons                   +in update
    public          List<GameObject>    ToolsArray;     //Work with world           +in update
    public          List<GameObject>    MapsArray;      //SCP-maps and other maps   +in update

    public          List<GameObject>    lastSelectedList = new List<GameObject>(6); //+in update

    public          Mods                mods = new Mods();

    public          GameObject          SelectedInstance;
    public          GameObject          ErrorModel;

    public          int                 ArrayOffset;
    public          int                 SelecteObject;
    public          enum                ArraySPAWNLIST {Prop, NPC, SCP, Weapons, Tools, Maps } //+in update
    public          ArraySPAWNLIST      ArrayMode;
    //=========================================================================================================
    #endregion

    private void                    Awake                   ()
    {
        LoadMods(ModsDirectories.weapons,   WeaponsArray);
        LoadMods(ModsDirectories.props,     PropsArray);
        LoadMods(ModsDirectories.entities,  NPCArray);
        LoadMods(ModsDirectories.tools,     ToolsArray);
        LoadMods(ModsDirectories.monsters,  SCPArray);
    }
    private void                    LoadMods                (in string modsDirectory, in List<GameObject> list)
    {
        string[] getModsFiles = Directory.GetFiles(modsDirectory);
        for (ushort index = 0; index < getModsFiles.Length; index++)
        {
            AssetBundle loadedBundle = AssetBundle.LoadFromFile(getModsFiles[index]);
            GameObject prefab = loadedBundle.LoadAsset("prefab") as GameObject;
            prefab.name = new FileInfo(getModsFiles[index]).Name;

            Texture2D textureIcon = loadedBundle.LoadAsset("icon") as Texture2D;
            Sprite icon = Sprite.Create(textureIcon, new Rect(0f, 0f, textureIcon.width, textureIcon.height), new Vector2(0.5f, 0.5f));
            TextAsset textAsset = loadedBundle.LoadAsset("description") as TextAsset;
            mods.references.Add(new UltimateModReferences(getModsFiles[index], prefab.name, icon, textAsset.text));

            loadedBundle.Unload(false);

            list.Add(prefab);
        }
    }
    public  async   void            SelectObject            (int numba)
    {
        SelecteObject = numba + ArrayOffset;
        await System.Threading.Tasks.Task.Delay(5);
        UpdateLastSelectedList(SelectedInstance);
    }
    public  void                    SelectByLastObjectList  (int btnID)
    {
        if(btnID > lastSelectedList.ToArray().Length-1)
        {
            SelectedInstance = null;
            return;
        }
        SelectedInstance = lastSelectedList[btnID] != null ? lastSelectedList[btnID] : null;
    }
    public  void                    EraseSelectedObj        ()
    {
        instance.SelectedInstance = null;
    }
    private void                    UpdateLastSelectedList  (in GameObject gameObject)
    {
        lastSelectedList.Add(gameObject);
        if (lastSelectedList.ToArray().Length>6)
        {
            lastSelectedList.RemoveAt(0);
        }
    }
    public  UltimateModReferences   TryGetModResources      (in string refName)
    {
        if (mods.references.Count <= 0)
        {
            return null;
        }
        foreach (UltimateModReferences modRef in mods.references)
        {
            if (modRef.name == refName)
            {
                return modRef;
            }
        }
        return null;
    }

    [System.Serializable]
    public class Mods
    {
        public List<UltimateModReferences> references = new List<UltimateModReferences>(16);
    }
    [System.Serializable]
    public class UltimateModReferences
    {
        public UltimateModReferences(string path, string name, Sprite icon, string description)
        {
            this.path           = path;
            this.name           = name;
            this.icon           = icon;
            this.description    = description;
        }
        public string path;
        public string name;
        public Sprite icon;
        [TextArea] public string description;
    }
}

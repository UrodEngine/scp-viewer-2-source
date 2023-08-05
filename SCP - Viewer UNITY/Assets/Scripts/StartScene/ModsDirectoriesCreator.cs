using UnityEngine;
using System.IO;

public class ModsDirectoriesCreator : MonoBehaviour
{
    private void Start              () => DirectoryInfoCheck();
    private void DirectoryInfoCheck ()
    {
        if (!Directory.Exists(ModsDirectories.hats))        Directory.CreateDirectory(ModsDirectories.hats);
        if (!Directory.Exists(ModsDirectories.mainFolder))  Directory.CreateDirectory(ModsDirectories.mainFolder);
        if (!Directory.Exists(ModsDirectories.avatars))     Directory.CreateDirectory(ModsDirectories.avatars);
        if (!Directory.Exists(ModsDirectories.maps))        Directory.CreateDirectory(ModsDirectories.maps);
        if (!Directory.Exists(ModsDirectories.props))       Directory.CreateDirectory(ModsDirectories.props);
        if (!Directory.Exists(ModsDirectories.entities))    Directory.CreateDirectory(ModsDirectories.entities);
        if (!Directory.Exists(ModsDirectories.weapons))     Directory.CreateDirectory(ModsDirectories.weapons);
        if (!Directory.Exists(ModsDirectories.other))       Directory.CreateDirectory(ModsDirectories.other);
        if (!Directory.Exists(ModsDirectories.tools))       Directory.CreateDirectory(ModsDirectories.tools);
        if (!Directory.Exists(ModsDirectories.monsters))    Directory.CreateDirectory(ModsDirectories.monsters);
    }
}
public static class ModsDirectories
{
    public static readonly string hats          = Application.persistentDataPath + "/Hats";
    public static readonly string mainFolder    = Application.persistentDataPath + "/Mods";
    public static readonly string avatars       = Application.persistentDataPath + "/Mods/Avatars";
    public static readonly string maps          = Application.persistentDataPath + "/Mods/Maps";
    public static readonly string props         = Application.persistentDataPath + "/Mods/Props";
    public static readonly string entities      = Application.persistentDataPath + "/Mods/Entities";
    public static readonly string monsters      = Application.persistentDataPath + "/Mods/Monsters";
    public static readonly string tools         = Application.persistentDataPath + "/Mods/Tools";
    public static readonly string weapons       = Application.persistentDataPath + "/Mods/Weapons";
    public static readonly string other         = Application.persistentDataPath + "/Mods/Other";

    public static readonly string thumbnailsFolder    = Application.persistentDataPath + "/Cache/Thumbnails";



    public  static void CreateFile  (string contentElement, in byte[] data)
    {
        contentElement      = contentElement.Trim();
        string[] splitted   = contentElement.Split('/');

        if      (contentElement.Contains("/maps/"))         WriteFile(maps     + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/props/"))        WriteFile(props    + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/entities/"))     WriteFile(entities + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/weapons/"))      WriteFile(weapons  + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/avatars/"))      WriteFile(avatars  + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/other/"))        WriteFile(other    + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/tools/"))        WriteFile(other    + "/" + splitted[splitted.Length - 1], data);
        else if (contentElement.Contains("/monsters/"))     WriteFile(other    + "/" + splitted[splitted.Length - 1], data);
        else 
        {
            Debug.Log($"(!) - CreateFile failed to find which directory must be as main: " + contentElement);
            return;
        };
    }
    private static void WriteFile   (in string path , in byte[] data)
    {
        Debug.Log("try write to: " + path);
        if (File.Exists(path))
        {
            Debug.Log("File already exist!!!: " + path);
            return;
        }
        Debug.Log("(v) - Saved file: " + path);
        File.WriteAllBytes(path, data);
    }
}
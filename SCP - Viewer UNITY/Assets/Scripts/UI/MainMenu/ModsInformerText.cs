using UnityEngine.UI;
using UnityEngine;
using System.IO;

[DisallowMultipleComponent]
public sealed class ModsInformerText : MonoBehaviour
{
    [SerializeField] private DownloadStarterKitMods downloadStarterKitModsReference;
    string check;


    private void OnEnable   ()
    {
        Reload();
    }
    public  void Update     ()
    {
        if (downloadStarterKitModsReference.currentElementField != check)
        {
            Reload();
        }
    }
    private void Reload     ()
    {
        GetComponent<Text>().text =
            $"Mods:\n" +
            $"<color=#FDFFB6>Maps</color>: {new DirectoryInfo(ModsDirectories.maps).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Weapons</color>: {new DirectoryInfo(ModsDirectories.weapons).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Entities</color>: {new DirectoryInfo(ModsDirectories.entities).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Monsters</color>: {new DirectoryInfo(ModsDirectories.monsters).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Tools</color>: {new DirectoryInfo(ModsDirectories.tools).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Props</color>: {new DirectoryInfo(ModsDirectories.props).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Other</color>: {new DirectoryInfo(ModsDirectories.other).GetFiles().Length}\n" +
            $"<color=#FDFFB6>Avatars</color>: {new DirectoryInfo(ModsDirectories.avatars).GetFiles().Length}\n" +
            $"---------\n" +
            $"last downloaded mod: {downloadStarterKitModsReference.currentElementField}\n" +
            $"last downloaded bytes: {downloadStarterKitModsReference.downloadBytesLengthField}\n";
    }
}

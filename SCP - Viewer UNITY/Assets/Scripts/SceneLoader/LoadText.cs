using UnityEngine;
using Text = UnityEngine.UI.Text;
public sealed class LoadText : MonoBehaviour
{
    private string  localizedText   => GetComponent<Localizator_SCP>().gettedText;
    private Text    textComponent        => GetComponent<Text>();


    private void FixedUpdate(){
        unsafe
        {
            fixed(char* lvl = LoadLevel.LoadLevelname)
            {
                textComponent.text = $"<color=#454545>{localizedText }</color>{(LoadLevel.LoadLevelname ?? "null")} <color=#454545><{((int)lvl).ToString("X2")}></color>. %:{(int)(GamePlaySceneLoadingSceneManager.savedLoadProcent*100)}";
            }
        }       
    }
}

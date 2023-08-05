using UnityEngine;
using Text = UnityEngine.UI.Text;
public sealed class Map_select : MonoBehaviour
{
    public  static  string         CurrentMapSelect;
    public  Text    textObj;

    public void SelectMap(string name)
    {
        CurrentMapSelect = name;

        unsafe
        {
            fixed (char* strP = name)
            {
                textObj.text = $"<color=#555555>Map :</color>{name}. <color=#363636><{((int)strP).ToString("X2")}></color>";
            }
        }
    }
}

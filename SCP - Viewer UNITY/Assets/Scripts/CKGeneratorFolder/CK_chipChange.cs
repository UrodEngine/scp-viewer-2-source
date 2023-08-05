using UnityEngine;

public class CK_chipChange : MonoBehaviour
{
    public Sprite[] imageSource;
    private int     imageCurrent;
    private bool    isHide = true;
    public void ChipBack()
    {
        imageCurrent = imageCurrent <= 0 ? 0 : imageCurrent - 1;
        GetComponent<UnityEngine.UI.Image>().sprite = imageSource[imageCurrent];
    }
    public void ChipNext()
    {
        imageCurrent = imageCurrent < imageSource.Length-1 ?  imageCurrent + 1 : imageSource.Length - 1;
        GetComponent<UnityEngine.UI.Image>().sprite = imageSource[imageCurrent];
    }
    public void ToggleVisible()
    {
        isHide = !isHide; 
        GetComponent<UnityEngine.UI.Image>().enabled = isHide is true ? false : true;
    }
}

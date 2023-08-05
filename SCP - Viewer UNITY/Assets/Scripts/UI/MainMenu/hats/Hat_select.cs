using UnityEngine.UI;
using UnityEngine;

public class Hat_select : MonoBehaviour
{
    #region Alterable values
    /*==========================================================================================*/
    public int          HatSlot;
    public Mesh         RefMesh;
    public Button       BuyButton;
    public Button       EnableHatButton;
    public GameObject   ManHat;
    /*==========================================================================================*/

    #endregion
    public void Start           (){
        
        EnableHatButton.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(delegate { SetHat(); SetCurrentHat(); });
        EnableHatButton.onClick.AddListener(delegate { EnabledHat(); });
        ManHat.GetComponent<MeshFilter>().mesh = null;
        CheckIsEnabled();
    }
    private void Update(){
        if (GetComponent<Image>().color == HatsList.enabledhatcolorStatic) EnableHatButton.gameObject.SetActive(true);
        else EnableHatButton.gameObject.SetActive(false);
    }
    public void SetHat          (){
        ManHat.GetComponent<MeshFilter>().mesh = RefMesh;
    }
    public void SetCurrentHat   (){
        BuyButton.GetComponent<HatSave_JSON>().SelectHat(HatSlot);
    }
    public void EnabledHat      (){
        GlobalHats.enabledhat[HatSlot]              = !GlobalHats.enabledhat[HatSlot];
        CheckIsEnabled();
        GlobalHatsObject.instance.enabledHatsArray[HatSlot]    = GlobalHats.enabledhat[HatSlot];
        
    }
    void CheckIsEnabled(){
        if (GlobalHats.enabledhat == null) return;
        if (HatSlot < 0) return;
        if (GlobalHats.enabledhat.Length < HatSlot) return;

        if (GlobalHats.enabledhat[HatSlot] is false)    transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (GlobalHats.enabledhat[HatSlot] is true)     transform.GetChild(1).GetComponent<Image>().color = HatsList.enabledhatcolorStaticCheckBox;
    }
}

using UnityEngine.UI;
using UnityEngine;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class AdsHatsCounter : MonoBehaviour
{
    #region Alterable values
    /*==========================================================================================*/
    public static AdsHatsCounter    instance;
    private Localizator_SCP         localSCP;
    private Text                    texter;
    public  int                     countADS_HATS;
    public  Button                  Ad_button;
    public  Button                  BUy_button;
    public  Button                  Debug_button;
    /*==========================================================================================*/

    #endregion
    private void Start                  (){
        instance    = this;
        texter      = GetComponent<Text>();
        localSCP    = GetComponent<Localizator_SCP>();

        Ad_button.onClick.AddListener       (() => { AdHatsInitializer.instance.LoadAd();   });
        BUy_button.onClick.AddListener      (() => { countADS_HATS--;                       });
        Debug_button.onClick.AddListener    (() => { countADS_HATS=3;                       });
    }
    private void Update                 (){
        texter.text = localSCP.gettedText + countADS_HATS;
        ButtonInteractables();
    }
    private void ButtonInteractables    (){
        if (countADS_HATS == 0){
            BUy_button.interactable = false;
            Ad_button.interactable  = true;
        }
        else{
            BUy_button.interactable = true;
            Ad_button.interactable  = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Instance_BTN : MonoBehaviour
{
    public      int         SlotID;
    public      string      SlotNamespace;
    protected   Text        _text;
    protected   GameObject  gettedItem;
    protected   Button      _button => GetComponent<Button>();


    public      void    Awake(){
        _text = transform.GetChild(0).GetComponent<Text>();
    }
    public      void    Update(){
        int     OriginalSlotID = SlotID + SPAWN_lists.instance.ArrayOffset;

        switch (SPAWN_lists.instance.ArrayMode){

            case SPAWN_lists.ArraySPAWNLIST.Prop:
                try{
                    _text.text = $"{SPAWN_lists.instance.PropsArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color = new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.PropsArray[OriginalSlotID].transform.gameObject;
                }
                catch{
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;

            case SPAWN_lists.ArraySPAWNLIST.NPC:
                try{
                    _text.text = $"{SPAWN_lists.instance.NPCArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color =new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.NPCArray[OriginalSlotID].transform.gameObject;
                }
                catch {
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;

            case SPAWN_lists.ArraySPAWNLIST.SCP:
                try{
                    _text.text = $"{SPAWN_lists.instance.SCPArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color = new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.SCPArray[OriginalSlotID].transform.gameObject;
                }
                catch{
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~Per update~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            case SPAWN_lists.ArraySPAWNLIST.Weapons:
                try
                {
                    _text.text = $"{SPAWN_lists.instance.WeaponsArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color = new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.WeaponsArray[OriginalSlotID].transform.gameObject;
                }
                catch
                {
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;
            case SPAWN_lists.ArraySPAWNLIST.Tools:
                try
                {
                    _text.text = $"{SPAWN_lists.instance.ToolsArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color = new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.ToolsArray[OriginalSlotID].transform.gameObject;
                }
                catch
                {
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;
            case SPAWN_lists.ArraySPAWNLIST.Maps:
                try
                {
                    _text.text = $"{SPAWN_lists.instance.MapsArray[OriginalSlotID]?.transform?.gameObject?.name}" ?? "NULL";
                    _text.color = new Color(132, 132, 132, 255);
                    gettedItem = SPAWN_lists.instance.MapsArray[OriginalSlotID].transform.gameObject;
                }
                catch
                {
                    _text.text = "Empty ID";
                    _text.color = Color.red;
                }
                break;

            default:
                return;              
        }
        _button.onClick.AddListener(OnClick);
    }
    protected   void    OnClick(){
        SPAWN_lists.instance.SelectedInstance = gettedItem;
    }
}

using UnityEngine;

public class SetArrayMode_HUD : MonoBehaviour
{
    public void SetProp_MODE(){
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.Prop;
    }
    public void SetNPC_MODE(){
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.NPC;
    }
    public void SetSCP_MODE(){
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.SCP;
    }
    //~~~~~~~~~~~~~~~~Per update~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void SetWEAPONS_MODE()
    {
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.Weapons;
    }
    public void SetTOOLS_MODE()
    {
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.Tools;
    }
    public void SetMAPS_MODE()
    {
        SPAWN_lists.instance.ArrayMode = SPAWN_lists.ArraySPAWNLIST.Maps;
    }
}

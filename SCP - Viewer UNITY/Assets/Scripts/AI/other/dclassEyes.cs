using UnityEngine;

sealed class dclassEyes : MonoBehaviour
{
    private Man dclass;
    void Start  (){
        byte parentCounts = 6;
        GameObject parent = this.gameObject;
        for (byte i = 0; i < parentCounts; i++)  {parent = parent.transform.parent.gameObject;}
        dclass = parent.GetComponent<Man>();
    }
    void Update (){
        short blinker         = dclass.DClassConfigs.blinkingTimer;
        if (blinker <= 25)  transform.GetChild(0).gameObject.SetActive(false);
        else                transform.GetChild(0).gameObject.SetActive(true);
    }
}

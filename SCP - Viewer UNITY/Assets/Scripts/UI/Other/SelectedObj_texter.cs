using UnityEngine;
using UnityEngine.UI;

public class SelectedObj_texter : MonoBehaviour
{
    private Text texter;
    public  Text textTutor;

    private void Start  (){
        texter = GetComponent<Text>();
    }
    private void Update (){
        string gettedtext   = textTutor.transform.gameObject.GetComponent<Localizator_SCP>().gettedText;
        textTutor.text      = SPAWN_lists.instance.SelectedInstance != null  ?   gettedtext : "...";
        texter.text         = SPAWN_lists.instance.SelectedInstance != null  ?   $"//[ {SPAWN_lists.instance.SelectedInstance.name} ]\\\\" : $"//[ Object ]\\\\";
    }
}

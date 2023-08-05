using UnityEngine;
using UnityEngine.UI;

public class ListNumb_UI : MonoBehaviour
{
    protected Text _text => GetComponent<Text>();
    void Update(){
        _text.text = $"Offset : {SPAWN_lists.instance.ArrayOffset}";
    }
}

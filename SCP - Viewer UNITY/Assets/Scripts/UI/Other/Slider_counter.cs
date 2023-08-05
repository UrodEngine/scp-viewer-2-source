using UnityEngine;
using UnityEngine.UI;

public class Slider_counter : MonoBehaviour
{
    public      Slider  sliderParent;
    private     Text    _text => GetComponent<Text>();
    public      int     Multipluer;


    void Update(){
        _text.text = $"{(int)(sliderParent.value * Multipluer)}";
    }
}

using UnityEngine.UI;
using UnityEngine;

public class RectRotate : MonoBehaviour
{
    /*========================================================================================================================*/
    public RectTransform    rect;
    public Vector3          rotate3;
    /*========================================================================================================================*/

    private void Start  (){
        rect = GetComponent<RectTransform>();
    }
    private void Update (){
        rect.Rotate(rotate3);
    }
}

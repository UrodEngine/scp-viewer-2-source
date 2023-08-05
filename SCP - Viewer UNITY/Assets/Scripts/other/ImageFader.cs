using UnityEngine;
using UnityEngine.UI;

sealed class ImageFader : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    [SerializeField] private float speedFade;
    [SerializeField] private float valueFade;
    [SerializeField] private Image faderImage;
    /*=========================================================================================================================================================*/
    #endregion
    private void Start(){
        faderImage = GetComponent<Image>();
        float r     = faderImage.color.r;
        float g     = faderImage.color.g;
        float b     = faderImage.color.b;

        faderImage.color = new Color(r, g, b, 1);
        valueFade = 0;
    }
    private void FixedUpdate(){
        float r = faderImage.color.r;
        float g = faderImage.color.g;
        float b = faderImage.color.b;

        faderImage.color = new Color(r, g, b, faderImage.color.a + (valueFade - faderImage.color.a) * speedFade);
    }
}

using UnityEngine;
using UnityEngine.UI;

public sealed class Cursor2 : MonoBehaviour
{
    #region Alterable values
    /*========================================================================================================================*/
    public int idOfTouch;
    public Camera ThisCamera;
    public RectTransform CanvasRect;
    public Text tSource;

    private Image iSource;
    private RectTransform rect;
    private float aChannel;
    /*========================================================================================================================*/
    #endregion

    private void Start(){
        iSource = GetComponent<Image>();
        tSource = transform.GetChild(0).GetComponent<Text>();
        rect    = GetComponent<RectTransform>();
    }
    private void Update(){
        // Set alpha transparent 
        aChannel = aChannel > 0 ? aChannel -= 0.05f : aChannel;
        iSource.color = new Color(iSource.color.r,iSource.color.g,iSource.color.b,aChannel);

        ReadCamMoveV2_parameters();

        //Change position by touch ID. +Set aChannel to 1;
        if (Input.touchCount is 0)
        {
            return;
        }

        for (ushort index = 0; index < Input.touchCount; index++){
            if (idOfTouch == index){
                Vector2 anchoredPos;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    CanvasRect,
                    Input.touches[idOfTouch].position,
                    ThisCamera,
                    out anchoredPos);

                rect.anchoredPosition = anchoredPos;
                aChannel = 1;
            }
        }
    }
    private void ReadCamMoveV2_parameters()
    {
        if (tSource is null) return;
        tSource.text    = CamMove_v2.instance.CameraMode != CamMove_v2.SettedMode.None? $"{CamMove_v2.instance.CameraMode.ToString()}" : "";
        tSource.color   = iSource.color;
    }
}

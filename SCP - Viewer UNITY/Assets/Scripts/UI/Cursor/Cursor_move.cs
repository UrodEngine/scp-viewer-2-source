using UnityEngine;
using UnityEngine.UI;
public class Cursor_move : MonoBehaviour
{
    public static Cursor_move   CM_stat;
    public RectTransform        m_ParentRect;
    public Camera               m_Camera;
    public RectTransform        m_ImageRect;
    public Sprite[]             m_Sprite = new Sprite[2];
    public Image                CursorBro;
    protected int               CursorModes;
    public int                  CursorFunctionModes { //»зменение его значени€ помен€ет текстуру курсора {основан на (int)CursorModes} 
        get 
        { 
            return CursorModes; 
        }  
        set 
        {
            CursorModes = value;
            this.GetComponent<Image>().sprite = m_Sprite[(int)value]; 
        } 
    } 


    protected   void    Start()
    {
        CM_stat = this;
    }
    /// <summary> CursorBro.fillAmount = CameraFOV.FingerTimer.time/90f; </summary>
    protected   void    LateUpdate()
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ParentRect, Input.mousePosition, m_Camera, out anchoredPos);
        m_ImageRect.anchoredPosition = anchoredPos;
        
    }
    /// <summary> »змен€ет значение свойства CursorFunctionModes  </summary>
    public      void    SetCursorMode(int mode) 
    {       
        CM_stat.CursorFunctionModes = mode;
    }
}

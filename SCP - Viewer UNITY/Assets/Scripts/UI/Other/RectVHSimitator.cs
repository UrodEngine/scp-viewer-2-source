using UnityEngine;

public sealed class RectVHSimitator : MonoBehaviour
{
    private RectTransform rectTransform => GetComponent(nameof(RectTransform)) as RectTransform;
 [System.Serializable]   public struct HorizontalLocker{
        public int MinX;
        public int MaxX;
    }
 [System.Serializable]   public struct VerticalLocker{
        public int MinY;
        public int MaxY;
    }

    [SerializeField] private HorizontalLocker   lockerX;
    [SerializeField] private VerticalLocker     lockerY;
    [SerializeField] private Vector2            speed;
   
    void Update()
    {
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x + speed.x, 
            rectTransform.anchoredPosition.y + speed.y);

        rectTransform.anchoredPosition = PositionCorrection(rectTransform.anchoredPosition, lockerY.MinY, lockerY.MaxY, 0);
        rectTransform.anchoredPosition = PositionCorrection(rectTransform.anchoredPosition, lockerX.MinX, lockerX.MaxX, 1);

    }

    private Vector2 PositionCorrection(Vector2 anchoredPosition, in float MinValue, in float MaxValue, in int axisID) 
    {
        //axisID == 0? this is Y
        //axisID != 0? this is X
        float axisPosition = axisID == 0? anchoredPosition.y : anchoredPosition.x;

        if (axisPosition < MinValue)            
            return axisID == 0 ? new Vector2(anchoredPosition.x, MaxValue - 2) : new Vector2(MaxValue - 2, anchoredPosition.y);
        else if (axisPosition > MaxValue)            
            return axisID == 0 ? new Vector2(anchoredPosition.x, MinValue + 2) : new Vector2(MinValue + 2, anchoredPosition.y);

        return anchoredPosition;
    }
}

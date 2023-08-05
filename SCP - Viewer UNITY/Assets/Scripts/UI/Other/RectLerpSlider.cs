using UnityEngine;

public sealed class RectLerpSlider : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform => GetComponent(nameof(RectTransform)) as RectTransform;
    [SerializeField] private bool isOpened = true;
    [SerializeField] private bool autoStartPosSet;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 point;
    [SerializeField] private float speed = 0.1f;

    void Start()
    {
        startPos = autoStartPosSet ? rectTransform.anchoredPosition3D : startPos;
        isOpened = false;
        rectTransform.anchoredPosition3D = point;
    }
    private void Update()
    {
        rectTransform.anchoredPosition3D = isOpened ? 
            Vector3.Lerp(rectTransform.anchoredPosition3D, startPos, speed) 
            : 
            Vector3.Lerp(rectTransform.anchoredPosition3D, point, speed);
    }
    public void OpenRect(bool boolean )
    {
        isOpened = boolean;
    }
}

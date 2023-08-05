using UnityEngine;

public class PlayerPrefsIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform  rect;
    [SerializeField] private Vector2[]      posByPlayerPrefsValue;
    [SerializeField] private string         PlayerPrefsName;
    [SerializeField] private float          speed = 0.1f;
    void FixedUpdate()
    {
        rect.anchoredPosition= Vector2.Lerp(rect.anchoredPosition, posByPlayerPrefsValue[PlayerPrefs.GetInt(PlayerPrefsName)], speed);
    }
}

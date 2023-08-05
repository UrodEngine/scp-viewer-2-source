using UnityEngine;
using UnityEngine.UI;

public class StartUp_skip : MonoBehaviour
{
    [SerializeField] private Image LoadBar;
    [SerializeField] private Text  SkipText;
    [SerializeField] private StartUpTitleEvents events;

    void FixedUpdate()
    {
        LoadBar.fillAmount = Mathf.Lerp(LoadBar.fillAmount, Input.touches.Length > 0 ? 1 : 0, 0.1f);
        if (LoadBar.fillAmount > 0.93f && events.time<13) events.time = 13;

        SkipText.color = new Color(SkipText.color.r, SkipText.color.g, SkipText.color.b, LoadBar.fillAmount*1.5f);
    }
}

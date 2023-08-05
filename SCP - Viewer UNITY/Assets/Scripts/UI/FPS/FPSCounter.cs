using UnityEngine;
using UnityEngine.UI;

public sealed class FPSCounter : MonoBehaviour
{
    private Text _text;
    private float deltaTime;
    public static short fps;

    private     void    Start()
    {
        _text = GetComponent<Text>();
    }
    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float getted = 1.0f / deltaTime;
        fps = (short)Mathf.Ceil(getted);
        _text.text = $"FPS: {fps}";
    }
}

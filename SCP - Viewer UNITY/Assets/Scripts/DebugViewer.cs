using UnityEngine;
using UnityEngine.UI;


public sealed class DebugViewer : MonoBehaviour
{
    [SerializeField] private Text templateText;
    [SerializeField] private Transform contentTransform;
    private void Start()
    {
        Application.logMessageReceived += LogSet;
    }
    private void OnDestroy()
    {
        Application.logMessageReceived -= LogSet;
    }
    private void LogSet(string condition, string stacktrace, LogType type)
    {
        try
        {
            Text message = Instantiate(templateText.gameObject, contentTransform).GetComponent<Text>();
            message.text = $"{condition}:{stacktrace}";
            switch (type)
            {
                case LogType.Log:
                    message.color = Color.white;
                    break;
                case LogType.Error:
                    message.color = Color.red;
                    break;
                case LogType.Warning:
                    message.color = Color.yellow;
                    break;
                default:
                    message.color = Color.white;
                    break;
            }
        }
        catch
        {
            Application.logMessageReceived -= LogSet;
        }
    }
}

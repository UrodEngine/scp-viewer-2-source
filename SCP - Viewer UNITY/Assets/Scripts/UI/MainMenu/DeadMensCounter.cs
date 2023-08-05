using UnityEngine;
using Text = UnityEngine.UI.Text;

public class DeadMensCounter : MonoBehaviour
{
    private Localizator_SCP localizator;
    private Text textComponent;
    void Start()
    {
        localizator = transform.GetComponent<Localizator_SCP>();
        textComponent = transform.GetComponent<Text>();       
    }
    private void FixedUpdate()
    {
        textComponent.text = $"{localizator.gettedText}:{PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_DEADMENS)}";
    }
}

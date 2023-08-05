using UnityEngine.UI;
using UnityEngine;

public sealed class NetIconStatus : MonoBehaviour
{
    #region alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private Image  icon;
    [SerializeField] private Text   text;
    [SerializeField] private byte   alpha = 0xFF;

    private float  normalized;

    [SerializeField, Header("Texts:")]
                        private string connected    = "NET (V)";
    [SerializeField]    private string notConnected = "NET (X)";

    private SimpleDelayer delayer = new SimpleDelayer(byte.MaxValue);
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion
    private void FixedUpdate()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            normalized = (float)(((float)alpha - 0f) / (255f - 0f));

            InternetStatusRequestor.instance.OnNetAvailable += () =>
            {
                icon.color = new Color(0.1485f, 0.5566f, 0.1446f, normalized);
                text.text = $"<color=#268E25{alpha.ToString("X2")}>{connected}</color>";
            };

            InternetStatusRequestor.instance.OnNetFailure += () =>
            {
                icon.color = new Color(0.5686f, 0.1176f, 0.1176f, normalized);
                text.text = $"<color=#911E1E{alpha.ToString("X2")}>{notConnected}</color>";
            };
        }
    }
}

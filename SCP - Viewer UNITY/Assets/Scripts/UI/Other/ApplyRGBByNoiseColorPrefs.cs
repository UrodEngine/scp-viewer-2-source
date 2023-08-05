using UnityEngine;
using UnityEngine.UI;

public sealed class ApplyRGBByNoiseColorPrefs : MonoBehaviour
{
    [SerializeField] private string     res;
    [SerializeField] private string[]   rgb;
    [SerializeField] private float      r, g, b, a;

    private void OnEnable   ()
    {
        Set();
    }
    public  void Set        ()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.OPTIONS_NOISE_ENABLED))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_NOISE_ENABLED, 0);
            transform.gameObject.SetActive(false);
            return;
        }
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_NOISE_ENABLED) is 0)
        {
            transform.gameObject.SetActive(false);
            return;
        }

        Image img = GetComponent<Image>();
        res = PlayerPrefs.GetString(PlayerPrefsKeys.OPTIONS_NOISE_COLOR);
        rgb = res.Split(',');
        for (int i = 0; i < rgb.Length; i++)
        {
            rgb[i] = rgb[i].Replace('.', ',').Replace('f','\0');
        }
        r = float.Parse(rgb[0]);
        g = float.Parse(rgb[1]);
        b = float.Parse(rgb[2]);
        a = float.Parse(rgb[3]);
        img.color = new Color(r, g, b, a);
    }
}

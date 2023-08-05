using UnityEngine;
public static class SavedGraphicsData
{
    /// <summary> 
    /// ------------------------------------------------------------------------------- <br/>
    /// Получить графические <see href="сохраненные"/> настройки <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// </summary>
    public static void GetGraphics()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_FIRST_LOAD) == 0)
        {
            FirstProgrammLoading();
            return;
        }

        QualitySettings.resolutionScalingFixedDPIFactor = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_DPI);

        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.OPTIONS_FPS) || PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_FPS) < 25)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_FPS, 32);
        }

        Application.targetFrameRate         = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_FPS);

        QualitySettings.masterTextureLimit  = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_TEXTURE_QUALITY);
    }
    /// <summary> 
    /// ------------------------------------------------------------------------------- <br/>
    /// Сброс настроек, либо, если игра запущена <see href="впервые"/>, запускается этот метод. <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// </summary>
    public static void FirstProgrammLoading()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_FIRST_LOAD, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_DPI, 2);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_FPS, 45);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_TEXTURE_QUALITY, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_MOTION_BLUR, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_DECAL_DETAILS, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_POST_PROCESSING, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_BLOOM, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_CHROMATIC, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.OPTIONS_NOISE_COLOR, "1,1,1,0.07f");
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_NOISE_COLOR_INDEX, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_NOISE_ENABLED, 0);

        GetGraphics();
    }
}
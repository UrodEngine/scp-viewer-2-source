using UnityEngine;
using UnityEngine.UI;

public class OptionsContainet : MonoBehaviour
{
    public  Slider[]     sliders;
    public  Dropdown[]   dropdowns;
    public  float        DPI;
    public  int          textureint;

    int tempID;

    public void Awake()
    {
        sliders[0].value = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_DPI);
        sliders[1].value = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_TEXTURE_QUALITY);
    }
    public void DPIChange                   (int sliderID)
    {
        int sliderCounter = sliders[sliderID].transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Slider_counter>().Multipluer;
        DPI = (int)(sliders[sliderID].value * sliderCounter);
    }
    public void QTextureChange              (int sliderID)
    {
        int sliderCounter = sliders[sliderID].transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Slider_counter>().Multipluer;
        textureint = (int)(sliders[sliderID].value * sliderCounter);
    }
    /// <summary> Принять настройки </summary>
    public void ApplyOptionsContainet       ()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_DPI, (int)DPI);
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_TEXTURE_QUALITY, textureint);

        SavedGraphicsData.GetGraphics();
    } 
    public async void ResetQualityOptions   ()
    {
        SavedGraphicsData.FirstProgrammLoading();
        SetLanguage(0);
        IfEnabledCameraShaking.ifEnabledStatic.CheckBlurOptions();

        await System.Threading.Tasks.Task.Delay(16);

        sliders[0].value = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_DPI);
        sliders[1].value = PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_TEXTURE_QUALITY);

        await System.Threading.Tasks.Task.Yield();
    } //Сброс настроек
    public async void SetLanguage           (int langEnumer)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_LANGUAGE, langEnumer);
        LocalizatorClass.isUpdating = true;

            await System.Threading.Tasks.Task.Delay(16);

        LocalizatorClass.isUpdating = false;

            await System.Threading.Tasks.Task.Yield(); 
    }
    public void EnableBlur                  ()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_MOTION_BLUR, 1);
        IfEnabledCameraShaking.ifEnabledStatic.CheckBlurOptions();
    }
    public void DisableBlur                 ()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_MOTION_BLUR, 0);
        IfEnabledCameraShaking.ifEnabledStatic.CheckBlurOptions();
    }
    public void ChangeDynamicDetails        (bool rechange)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_DECAL_DETAILS, rechange is true? 1 : 0);
    }
    public void SetPreviewInFileExplorer    (bool booler) => PlayerPrefs.SetInt(PlayerPrefsKeys.OPTIONS_FILE_MANAGER_PREVIEWS, booler is true ? 1 : 0);

}

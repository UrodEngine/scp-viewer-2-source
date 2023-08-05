using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine;

public sealed class AdsTurnInMenu : MonoBehaviour
{
    #region alterable values
    //```````````````````````````````````````````````````````````````````````````````````````
    public  static AdsTurnInMenu    instance;
    private Interstitial            interstitial;
    public  bool                    demo            = true;
    public  string                  blockID;
    [SerializeField] private GameObject loadBar;
    //```````````````````````````````````````````````````````````````````````````````````````
    #endregion

    private void Start()
    {
        Debug.Log($"AFTER_SCENE_MENU_ADS = {PlayerPrefs.GetInt(PlayerPrefsKeys.AFTER_SCENE_MENU_ADS)}");
        instance        = this;
        interstitial    = new Interstitial(demo is true ? AdDemos.DEMO_INTERSTITIAL_ID : blockID);

        interstitial.OnInterstitialLoaded       += (obj, evArg) =>  { Debug.Log($"({blockID}) Try show ad"); interstitial.Show(); };
        interstitial.OnInterstitialFailedToLoad += (obj, evArg) =>  { Debug.Log($"({blockID}) Failed to load: {evArg.Message}"); loadBar.SetActive(false); };
        interstitial.OnInterstitialFailedToShow += (obj, evArg) =>  { Debug.Log($"({blockID}) Failed to shown: {evArg.Message}"); loadBar.SetActive(false); };
        interstitial.OnReturnedToApplication    += (obj, evArg) =>  { loadBar.SetActive(false); };

        if (PlayerPrefs.GetInt(PlayerPrefsKeys.AFTER_SCENE_MENU_ADS) >= 2)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.AFTER_SCENE_MENU_ADS, 0);
            LoadAd();
            loadBar.SetActive(true);
        }
    }

    public void LoadAd()
    {
        if (InternetStatusRequestor.isConnected)
        {
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
        }
        else
        {
            Debug.Log($"({blockID}) NET-not-connected. Ads will not show. Ads is not builded");
        }
    }
}

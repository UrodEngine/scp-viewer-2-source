using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine;

public sealed class IsObjSelectorUsefulInt : MonoBehaviour
{
    #region Alterable values
    /*==========================================================================================*/
    public  int             turns;
    public  string          blockID;
    public  bool            demo        = true;
    public  GameObject      loadBar;
    private Interstitial    interstitial;
    /*==========================================================================================*/
    #endregion

    private void Start          ()
    {       
        InitializeAd();
    }
    private void InitializeAd   ()
    {       
        interstitial = new Interstitial(demo is true ? AdDemos.DEMO_INTERSTITIAL_ID : blockID);

        interstitial.OnInterstitialFailedToLoad += (obj, evArg) => 
        { Debug.Log($"({blockID}) Failed to load: {evArg.Message}"); loadBar.SetActive(false); };

        interstitial.OnInterstitialFailedToShow += (obj, evArg) => 
        { Debug.Log($"({blockID}) Failed to shown: {evArg.Message}"); loadBar.SetActive(false); };

        interstitial.OnInterstitialLoaded += (obj, evArg) =>
        {
            interstitial.Show();
        };
        interstitial.OnReturnedToApplication += (obj, evArg) =>
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.OBJECTS_LIST_ADS, 0);
            loadBar.SetActive(false);
        };

        Debug.Log($"({blockID}) NET-connected. Ads will show. Ads builded");
    }
    public  void InterTurnAdd   ()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.OBJECTS_LIST_ADS, PlayerPrefs.GetInt(PlayerPrefsKeys.OBJECTS_LIST_ADS) + 1);
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.OBJECTS_LIST_ADS) >= 6)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.OBJECTS_LIST_ADS, 0);

            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            loadBar.SetActive(true);
        }
    }
}

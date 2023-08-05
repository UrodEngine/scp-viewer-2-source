using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine;
public sealed class AdHatsInitializer : MonoBehaviour
{
    #region alterable values
    //```````````````````````````````````````````````````````````````````````````````````````
    private RewardedAd                  rewarded;
    public  static AdHatsInitializer    instance;
    public  bool                        demo                = true;
    public  string                      blockID;

    public  bool                        debug               = false;
    public  byte                        availableBuyTickets = 0;

    [SerializeField] private GameObject loadBar;
    //```````````````````````````````````````````````````````````````````````````````````````
    #endregion

    private void Start()
    {
        instance = this;

        rewarded = new RewardedAd(demo is true ? AdDemos.DEMO_REWARDED_ID : blockID);

        rewarded.OnRewardedAdLoaded         += (obj, evArg) => { Debug.Log("Loaded block"); rewarded.Show(); };
        rewarded.OnRewarded                 += (obj, evArg) => { availableBuyTickets = 3; loadBar.SetActive(false); };
        rewarded.OnRewardedAdFailedToLoad   += (obj, evArg) => { Debug.Log($"({blockID}) Failed to load: {evArg.Message}"); loadBar.SetActive(false); };
        rewarded.OnRewardedAdFailedToShow   += (obj, evArg) => { Debug.Log($"({blockID}) Failed to shown: {evArg.Message}"); loadBar.SetActive(false); };
    }
    public  void LoadAd()
    {
        if (!debug)
        {
            AdRequest request = new AdRequest.Builder().Build();
            rewarded.LoadAd(request);
            loadBar.SetActive(true);
        }
        else
        {
            availableBuyTickets = 3;
        }
    }
}

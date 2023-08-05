using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using YandexMobileAds;
using YandexMobileAds.Base;

#if UNITY_ANDROID || UNITY_IOS
using NativeGalleryNamespace;
#endif

public sealed class CK_screenShoot : MonoBehaviour
{
    //==================================================================================================
    private DirectoryInfo   dirInfo;
    private FileInfo[]      files;

    [SerializeField] private Camera screenCamera;

    private Interstitial    interstitial;
    public  bool            demo            = true;
    public  string          blockID;
    public  GameObject      screenShotButton;
    public  GameObject      loadBarImage;
    //==================================================================================================

    private void        Start()
    {
        #region directory
        if (!Directory.Exists(Application.persistentDataPath + "/Screenshots"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Screenshots");
        }
        dirInfo = new DirectoryInfo(Application.persistentDataPath + "/Screenshots");
        #endregion

        interstitial = new Interstitial(demo is true ? AdDemos.DEMO_INTERSTITIAL_ID : blockID);

        InternetStatusRequestor.instance.OnNetAvailable += () => { screenShotButton.SetActive(true); };
        
        interstitial.OnInterstitialFailedToLoad += (obj, evArg) => { AdRequest request = new AdRequest.Builder().Build(); interstitial.LoadAd(request); Debug.Log($"({blockID}) Failed to load: {evArg.Message}"); loadBarImage.SetActive(false); };
        interstitial.OnInterstitialLoaded       += (obj, evArg) => { Debug.Log("Loaded block"); interstitial.Show(); };
        interstitial.OnReturnedToApplication    += (obj, evArg) => { SetReadyToScreenshot(); screenShotButton.GetComponent<Button>().interactable = true; loadBarImage.SetActive(false); };
        interstitial.OnInterstitialFailedToShow += (obj, evArg) => { AdRequest request = new AdRequest.Builder().Build(); interstitial.LoadAd(request); Debug.Log($"({blockID}) Failed to show: {evArg.Message}"); loadBarImage.SetActive(false); };
    }
    public void StartAds()
    {
        loadBarImage.SetActive(true);
        screenShotButton.GetComponent<Button>().interactable = false;
        AdRequest request = new AdRequest.Builder().Build(); 
        interstitial.LoadAd(request);
    }
    public  void        SetReadyToScreenshot()
    {
        screenCamera.depth = 2048;
        StartCoroutine(SaveScreenshot());
        #if UNITY_ANDROID || UNITY_IOS
        StartCoroutine(nameof(NativeGalleryScreenSave));
        #endif
    }
    private IEnumerator SaveScreenshot()
    {
        string  currentName = "KeyCard";
        int     lastNameNumber = 0;
        try
        {
            files = dirInfo.GetFiles("*.png", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++){if (($"{currentName}_{i}.png") == files[i].Name){lastNameNumber++;}continue;}
        }
        finally { }
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screenshots/" + currentName + "_" + lastNameNumber + ".png");
        yield return new WaitForSeconds(1.5f);
        screenCamera.depth = -2048;
    }

    #if UNITY_ANDROID || UNITY_IOS
    private IEnumerator NativeGalleryScreenSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshotPreData = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, 0, false);
        screenshotPreData.ReadPixels(new Rect(0, 0, Screen.width, Screen.height),0, 0);
        screenshotPreData.Apply();

        string currentName = "KeyCard";
        int lastNameNumber = 0;
        try
        {
            files = dirInfo.GetFiles("*.png", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (($"{currentName}_{i}.png") == files[i].Name)
                {
                    lastNameNumber++;
                }
                continue;
            }
        }
        finally { }
        NativeGallery.SaveImageToGallery(screenshotPreData, "SCP - Viewer 2 KeyCards", $"{currentName}_{lastNameNumber}.png");
    }
    #endif

}


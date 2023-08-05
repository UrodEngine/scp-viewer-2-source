using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

sealed class StartUpTitleEvents : MonoBehaviour
{
    #region Alterable values
    /*=======================================================================================================*/
    public  byte   time;
    [SerializeField] private Text   title;
    [SerializeField] private Image  fadeImage;
    [SerializeField] private Image  imageLoad;
    [SerializeField] private float  fadeValue;
    [SerializeField] private float  speedFade;
    [SerializeField] private Font[] fonts;
    [SerializeField] private AudioSource audioSource1;

    private Timer timer = new Timer(1000);
    /*=======================================================================================================*/
    #endregion
    private void Start              (){
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeValue       = fadeImage.color.a;

        timer.AutoReset = true;
        timer.Enabled   = true;
        timer.Elapsed   += delegate { time++;};
        timer.Start();
    }
    private void Fader              (){
        float r         = fadeImage.color.r;
        float g         = fadeImage.color.g;
        float b         = fadeImage.color.b;
        fadeImage.color = new Color(r, g, b, fadeImage.color.a + (fadeValue - fadeImage.color.a) * speedFade);        
    }
    private void FixedUpdate        (){
        imageLoad.fillAmount = time / 15f;
        Fader();
        if (time == 15){
            timer.Stop();
            time = 245;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        if (time == 14){
            fadeValue = 1;
            speedFade = 0.4f;
        }
        if(time > 6)
        {
            audioSource1.pitch = Mathf.Lerp(audioSource1.pitch, 0.1f, 0.005f);
            audioSource1.volume = Mathf.Lerp(audioSource1.pitch, 0, 0.005f);
        }
        TimerChecker();

    }
    private void TimerChecker       (){

        if (time ==  1) fadeValue   = 0;

        if (time == 3 ) title.text  = "";

        if (time == 4)
        {
            title.font = fonts[1];
            title.fontSize = 150;
            title.text = "UROD ENGINE \npresents:";
        }

        if (time == 9 ) title.text  = "";
        if (time == 10)
        {
            title.font = fonts[0];
            title.fontSize = 300;
            title.text = "SCP - Viewer 2";
        }

        if (time == 14) title.text  = "";
    } //Per timer.elapsed

    #region timer stop
    private void OnApplicationQuit  () => timer.Stop();
    private void OnDestroy          () => timer.Stop();
    #endregion
}

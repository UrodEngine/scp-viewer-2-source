using UnityEngine;

public class VoicesOn : MonoBehaviour
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    [SerializeField]    private AudioClip[]     Voices;
    [SerializeField]    private AudioClip       AttackClip;
                        private AudioSource     ASource;
    [SerializeField]    private int             currentTimeToVoice;
                        public  int             voiceInterval;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion


    private     int     Elapsed     ()  {
        PlayVoice();
        return voiceInterval;
    }
    private     void    Start       ()  {
        currentTimeToVoice  =   voiceInterval;
        ASource             =   GetComponent<AudioSource>();
    }
    private     void    PlayVoice   ()  {
        bool iWantToVoice = Random.Range(0, 70) > 40 ? true : false;

        if (iWantToVoice is false) return;
        iWantToVoice = false;
        ASource.clip = Voices[Random.Range(0, Voices.Length)];
        ASource.Play();
    }
    public      void    AttackVoice ()  {
        ASource.clip = AttackClip;
        ASource.Play();
    }
    private     void    Update      () => currentTimeToVoice = currentTimeToVoice > 0 ? currentTimeToVoice - 1 : Elapsed();
}

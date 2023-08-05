using UnityEngine;
using System.Timers;
sealed class AMBIENTS_spot : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public  AudioClip[] AmbientClips;
    private AudioSource aSource;
    private Timer       timer = new Timer(1000);

    [SerializeField] private ushort toAmbTimer;    
    /*=========================================================================================================================================================*/
    #endregion

    private void Start              (){
        aSource         = GetComponent<AudioSource>();
        timer.Elapsed   += delegate { toAmbTimer--; };
        timer.AutoReset = true;
        timer.Enabled   = true;
        toAmbTimer      = 25;
        timer.Start();
        aSource.clip = AmbientClips[Random.Range(0, AmbientClips.Length)];
    }
    private void OnDestroy          (){
        timer.Stop();
    }
    private void LateUpdate         ()
    {
        if (toAmbTimer <= 0) StartAmbient();
    }
    private void StartAmbient       (){
        aSource.clip        = AmbientClips[Random.Range(0, AmbientClips.Length)];
        toAmbTimer          = (ushort)(aSource.clip.length + 25);
        aSource.Play();
    }
    private void OnApplicationQuit  (){
        timer.Stop();
    }
}

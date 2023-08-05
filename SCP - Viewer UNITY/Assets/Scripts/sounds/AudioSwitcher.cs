using UnityEngine;

[AddComponentMenu("Audio/Audio switcher")]
public class AudioSwitcher : MonoBehaviour,ISoundSwitcher
{
    [SerializeField] private AudioClip      enableSound, disableSound;
    [SerializeField] private AudioSource    audioSource ;
    private sbyte   activeCharge = -1;
    private bool    isFixed;

    private void Update()
    {
        activeCharge = activeCharge > (sbyte)0 ? (sbyte)(activeCharge - 1)  : activeCharge;
        if(activeCharge == 0)
        {
            isFixed = false;
            activeCharge = -1;
            if (disableSound != null)
            {
                audioSource.clip = disableSound;
                audioSource.Play();
            }
        }
    }
    public  void FixedPlay()
    {
        activeCharge = 15;
        if (isFixed) return;
        isFixed = true;
        audioSource.clip = enableSound;
        audioSource.Play();

    }
}

using UnityEngine;

[AddComponentMenu("Sound Scripts/WalkSounds")]
public sealed class WalkSounds : MonoBehaviour
{
    /*======================================================*/
    private Animator                        _animator;
    private AudioSource                     _ASource ;
    [SerializeField] private AudioClip[]    SoundOnStep;
    [SerializeField] private byte           layerID = 1;
    /*======================================================*/

    void Start  ()
    {
        _animator   = GetComponent<Animator>();
        _ASource    = GetComponent<AudioSource>();
    }
    void Step   ()
    {
        try
        {
            if (_animator.GetLayerWeight(layerID) <= 0.5)
            {
                return;
            }
            _ASource.clip = SoundOnStep[Random.Range(0, SoundOnStep.Length)];
            _ASource.Play();
        }
        catch
        {
            return;
        }
    } //By animator
}

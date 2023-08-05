using UnityEngine;

sealed class SoundOnClick : MonoBehaviour
{
    public  AudioClip[]  samples;

    public void PlaySound(int sampleID){
        AudioSource     ASource = GetComponent<AudioSource>();

        ASource.clip = samples[sampleID];
        ASource.volume = 1;
        ASource.pitch = 1;
        ASource.PlayOneShot(ASource.clip);
    }
    public void PlaySound(AudioClip clip)
    {
        AudioSource ASource = GetComponent<AudioSource>();

        ASource.clip = clip;
        ASource.volume = 1;
        ASource.pitch = 1;
        ASource.PlayOneShot(ASource.clip);
    }
    public void PlaySound(in AudioClip clip, in bool randomPitch = false, in float volume = 1)
    {
        AudioSource ASource = GetComponent<AudioSource>();

        ASource.clip    = clip;
        ASource.volume  = volume;
        ASource.pitch   = randomPitch is false ? 1 : Random.Range(0.3f, 1.6f);
        ASource.PlayOneShot(ASource.clip);
    }
}

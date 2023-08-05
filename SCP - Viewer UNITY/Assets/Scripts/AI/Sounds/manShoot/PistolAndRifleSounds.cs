using UnityEngine;

public sealed class PistolAndRifleSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundSource;
    private AudioSource asource => GetComponent<AudioSource>();


    void ShootPlay(int step)
    {
        asource.clip = soundSource[step];
        asource.Play();
    }
}

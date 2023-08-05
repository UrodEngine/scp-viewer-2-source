using UnityEngine;

public class DoorSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] SoundClips;
    private AudioSource asource => GetComponent<AudioSource>();
    void DoorSound(int doorSound) {
        asource.clip = SoundClips[doorSound];
        asource.Play();
    }

}

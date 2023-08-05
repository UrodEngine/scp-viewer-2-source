using UnityEngine;

public sealed class ShotgunSounds : MonoBehaviour
{
    [SerializeField]    private AudioClip[] shotgClips;
                        private AudioSource asource => GetComponent<AudioSource>();
    void ShootSound(int step){
        asource.clip = shotgClips[step];
        asource.Play();
    }
}

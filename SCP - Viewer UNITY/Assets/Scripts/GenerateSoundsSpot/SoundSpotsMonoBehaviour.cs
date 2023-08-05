using UnityEngine;

[AddComponentMenu("UROD Engine/Sounds/SoundsMonoBehaviour")]
public class SoundSpotsMonoBehaviour : MonoBehaviour
{
    [SerializeField] private SoundElement[] sounds;
    [SerializeField] private Parameters     parameters;


    public void Play(string key)
    {
        foreach (SoundElement sound in sounds)
        {
            if (sound.key == key)
            {
                SoundSpots.Generate(transform, sound.clip, out AudioSource aSource);
                SoundSpots.AppendParameters(aSource, parameters);
                return;
            }
        }
    }
    public AudioClip GetClip(string key)
    {
        foreach (SoundElement sound in sounds)
        {
            if (sound.key == key)
            {
                return sound.clip;
            }
        }
        return null;
    }


    [System.Serializable]
    public class SoundElement
    {
        public AudioClip clip;
        public string key;
    }
    [System.Serializable]
    public struct Parameters
    {
        public byte priority;
        [Range(0f, 1f)] public float volumetric;
        [Range(0f, 1f)] public float volume;
        [Range(-3f, 3f)] public float pitch;
        public float minDistance;
        public float maxDistance;
    }
}

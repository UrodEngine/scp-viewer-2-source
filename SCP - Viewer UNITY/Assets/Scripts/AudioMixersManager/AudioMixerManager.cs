using UnityEngine;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.Audio.AudioMixerGroup[] audioMixers;
    public static AudioMixerManager instance;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    public static UnityEngine.Audio.AudioMixerGroup GetMixerByName(in string _name)
    {
        foreach (var item in instance.audioMixers)
        {
            if (item.audioMixer.name == _name)
            { 
                return item;
            }
        }
        return null;
    }
}

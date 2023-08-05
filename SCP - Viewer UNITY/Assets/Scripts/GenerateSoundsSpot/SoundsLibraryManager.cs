using UnityEngine;
using UnityEditor;

[DisallowMultipleComponent]
public class SoundsLibraryManager : MonoBehaviour
{
    public static SoundsLibraryManager instance;
    public Sound[] sounds;
    void Start()
    {
        instance = this;
    }
    public void Launch(AudioClip _clip)
    {
        GameObject audioObj = new GameObject("SoundLibraryListenerInstance", typeof(AudioSource));
        AudioSource source = audioObj.GetComponent<AudioSource>();
        source.clip = _clip;
        source.Play();
        Destroy(audioObj, _clip.length);
    }
    public void Launch(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                Launch(sounds[i].clip);
                return;
            }
        }
    }
    public void Launch(short _id)
    {
        Launch(sounds[_id].clip);
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}

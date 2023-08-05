using UnityEngine;

public class sounds096 : MonoBehaviour
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public  AudioClip[]     sounds;

    private AudioSource     _aSource;   
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void    Start          ()
    {
        _aSource = GetComponent<AudioSource>();
    }
    public  void    SaySound        (in int setClipID)
    {
        if (_aSource.clip               == sounds[setClipID])              return;
        if (_aSource.clip.name          == sounds[setClipID].name)          return;
        if (_aSource.clip.GetHashCode() == sounds[setClipID].GetHashCode()) return;

        _aSource.clip = sounds[setClipID];
        _aSource.Play();
    }
    public  string  GetSound        ()
    {
        return _aSource.clip.name;
    }
}

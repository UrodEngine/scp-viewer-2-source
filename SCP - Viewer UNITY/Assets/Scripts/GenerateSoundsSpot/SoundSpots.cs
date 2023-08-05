using UnityEngine;

public static class SoundSpots
{
    public static void Generate(in Transform transform, in AudioClip clip, out AudioSource aSource, in string audioMixerGroup = null)
    {
        aSource = null;
        GameObject soundSpot = new GameObject($"GeneratedSoundSpot_{System.Guid.NewGuid().ToString().Split('-')[0]}");
        soundSpot.transform.position = transform.position;
        soundSpot.transform.rotation = transform.rotation;
        soundSpot.transform.localScale = transform.localScale;
        aSource = soundSpot.AddComponent<AudioSource>();
        //aSource.outputAudioMixerGroup = AudioMixerManager.GetMixerByName(audioMixerGroup ?? "CommonMixer");
        aSource.clip = clip;
        aSource.Play();
        GameObject.Destroy(soundSpot.gameObject, clip.length);
    }
    public static void AppendParameters(in AudioSource aSource, in SoundSpotsMonoBehaviour.Parameters parameters)
    {
        aSource.volume          = parameters.volume;
        aSource.pitch           = parameters.pitch;
        aSource.spatialBlend    = parameters.volumetric;
        aSource.minDistance     = parameters.minDistance;
        aSource.maxDistance     = parameters.maxDistance;
        aSource.priority        = parameters.priority;
    }
}

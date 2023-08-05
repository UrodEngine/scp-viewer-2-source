using UnityEngine;
[AddComponentMenu("UROD Engine/Sounds/Spot on destroy or spawn")]
public sealed class SoundSpotOnDestroyOrSpawn : MonoBehaviour
{
    #region alterable values
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public enum Type { DESTROY, SPAWN, BOTH }
    public Type type = Type.DESTROY;

    [SerializeField]                    private AudioClip sound;
    [SerializeField]                    private AudioClip spawnSound;
    [Range(0, 1)]   [SerializeField]    private float volume = 1;
    [Range(-1, 1)]  [SerializeField]    private float pitch = 1;
    [Range(0, 1)]   [SerializeField]    private float to3D = 0;
    [SerializeField]                    private float minRollof = 0;
    [SerializeField]                    private float maxRollof = 15.97f;
    [SerializeField]                    private AudioRolloffMode rollofMode = AudioRolloffMode.Linear;

    [Header("Delete component on execute:")]
    public bool deleter = false;
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #endregion

    private void Awake      ()
    {
        if (type is Type.DESTROY || type is Type.BOTH)
        {
            deleter = false;
        }
        if (spawnSound is null)
        {
            spawnSound = sound;
        }
        if (sound is null)
        {
            Destroy(this);
            return;
        }
    }
    private void Start      ()
    {
        if (!spawnSound)
        {
            return;
        }
        if (type is Type.SPAWN || type is Type.BOTH)
        {

            if (!gameObject.scene.isLoaded)
            {
                return;
            }
            SoundSpots.Generate(transform, spawnSound, out AudioSource aSource);
            aSource.volume = volume;
            aSource.pitch = pitch;
            aSource.spatialBlend = to3D;
            aSource.rolloffMode = rollofMode;
            aSource.minDistance = minRollof;
            aSource.maxDistance = maxRollof;
        }

        if (deleter)
        {
            Destroy(this);
            return;
        }
    }
    private void OnDestroy  ()
    {
        if (type is Type.DESTROY || type is Type.BOTH)
        {
            if (!gameObject.scene.isLoaded)
            {
                return;
            }
            SoundSpots.Generate(transform, sound, out AudioSource aSource);
            aSource.volume = volume;
            aSource.pitch = pitch;
            aSource.spatialBlend = to3D;
            aSource.rolloffMode = rollofMode;
            aSource.minDistance = minRollof;
            aSource.maxDistance = maxRollof;

            if (deleter)
            {
                Destroy(this);
                return;
            }
        }
    }
}

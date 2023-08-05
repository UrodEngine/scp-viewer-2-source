using UnityEngine;
[AddComponentMenu("UROD Engine/Sounds/Spot by falling")]
public sealed class SoundSpotByFalling : MonoBehaviour
{
    public enum Type { CHECK_BOTH_AXIS, CHECK_ONLY_Y};
    public Type type = Type.CHECK_ONLY_Y;

    private Vector3 previousPosition;
    private Vector3 deltaPosition;

    [Header("Звуковые поля")]
    [SerializeField] private AudioClip enterOnGroundSound;

    [Header("Настройки")]
    [SerializeField, Range(0f, 1f)]     private float volume        = 0.3f;
    [SerializeField, Range(0f, 1f)]     private float spatialBlend  = 0;
    [SerializeField, Range(0f, 256)]    private float minDistance  = 1;
    [SerializeField, Range(0f, 256)]    private float maxDistance  = 128;
    [SerializeField, Range(0f, 1f)]     private float volumeFalling = 0.2f;
    [SerializeField] private AudioSource loopedFallSource;


    private void Start()
    {
        previousPosition = transform.position;
    }
    private void FixedUpdate()
    {
        deltaPosition = previousPosition - transform.position;
        previousPosition = transform.position;

        switch (type)
        {
            case Type.CHECK_BOTH_AXIS:
                if (loopedFallSource)
                {
                    if (deltaPosition.y >= 0.1f || deltaPosition.x>=0.1f || deltaPosition.z >= 0.1f)
                    {
                        loopedFallSource.volume += (volumeFalling - loopedFallSource.volume) * 0.1f;
                    }
                    else
                    {
                        loopedFallSource.volume += (0 - loopedFallSource.volume) * 0.1f;
                    }
                }
                break;

            case Type.CHECK_ONLY_Y:
                if (loopedFallSource)
                {
                    if (deltaPosition.y >= 0.1f)
                    {
                        loopedFallSource.volume += (volumeFalling - loopedFallSource.volume) * 0.1f;
                    }
                    else
                    {
                        loopedFallSource.volume += (0 - loopedFallSource.volume) * 0.1f;
                    }
                }
                break;
        }        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (enterOnGroundSound)
        {
            if (deltaPosition.y >= 0.1f || deltaPosition.x >= 0.1f || deltaPosition.z >= 0.1f)
            {
                SoundSpots.Generate(transform, enterOnGroundSound, out AudioSource aSource);
                aSource.volume = volume;
                aSource.spatialBlend = spatialBlend;
                aSource.minDistance = minDistance;
                aSource.maxDistance = maxDistance;
            }
        }
    }
}

using UnityEngine;

public sealed class PhysicsPropProperties : MonoBehaviour, IObjectParameters
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public bool checkGravity = true;
    private Rigidbody rigidBody => GetComponent<Rigidbody>();

    [SerializeField] private AliveForm.GameObjectPrefabs gameObjectPrefabs = new AliveForm.GameObjectPrefabs();

    [Header("Hp and gravity damage for this object")]

    [SerializeField] private ObjectFormProperties properties = new ObjectFormProperties();

    [SerializeField] private float  damageMultiptier = 1f;

    [Header("Sound by break method. set NULL for nothing")]

    [SerializeField] private GameObject     soundSpot;
    [SerializeField] private AudioClip[]    soundClips;
    [SerializeField] private GameObject     deadPrefab;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion
    private void Start()
    {
        gameObjectPrefabs.parentGameObject = this.gameObject;
    }
    private void OnCollisionEnter   (Collision surface)
    {
        if (!checkGravity) return;

        if (!GetComponent(nameof(Rigidbody))) return;
        if (System.Math.Abs(rigidBody.velocity.magnitude) * damageMultiptier < 1) return;

        if(!properties.invulnerable) properties.health -= (short)(System.Math.Abs(rigidBody.velocity.magnitude)*damageMultiptier);
        PlaySoundsOfBreak();
    }
    private void PlaySoundsOfBreak  ()  
    {
        if (soundSpot is null) return;
        if (soundClips.Length <= 0) return;
        GameObject spot = Instantiate(soundSpot, this.gameObject.transform.position, Quaternion.identity);
        spot.GetComponent<AudioSource>().clip = soundClips[Random.Range(0, soundClips.Length)];
        spot.GetComponent<AudioSource>().pitch = Random.Range(0.25f, 1.25f);
        spot.GetComponent<AudioSource>().Play();
    }
    public  void SetDamage          (in int damageValue, in bool setBloodLust) => properties.health -= (short)(System.Math.Abs(damageValue) * damageMultiptier);
    public  void KillForm           (in bool WithBlood) => properties.health = 0;
    private void Update             ()  
    {
        if (properties.health > 0 || properties.invulnerable) return;
        SpawnEffects();
        gameObjectPrefabs.TryDrop();
        Destroy(this.gameObject);
    }
    private void SpawnEffects       ()  
    {
        if (deadPrefab != null)
        {
            GameObject effectPrefab = Instantiate(deadPrefab, transform.position, Quaternion.identity);
        }
    }

    public ObjectFormProperties GetProperties() => properties;
}

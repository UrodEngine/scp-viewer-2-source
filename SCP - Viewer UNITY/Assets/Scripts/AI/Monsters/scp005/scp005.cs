using UnityEngine;

[AddComponentMenu("SCP/005 - rust key")]
public sealed class scp005 : MonoBehaviour
{
    private SimpleDelayer delayer = new SimpleDelayer(5);

    [SerializeField] private AudioClip onGetKeySound;

    private void FixedUpdate()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 15);

            foreach (Collider collider in colliders)
            {
                if(collider.gameObject.TryGetComponent<Man>(out Man man))
                {
                    man.DClassConfigs.walking = 100;
                    man.DClassConfigs.interestPoint = transform.position;

                    if (Vector3.Distance(man.transform.position, transform.position) < 4)
                    {
                        man.canOpenDoors = true;

                        man.DClassConfigs.walking = 0;
                        man.DClassConfigs.interestPoint = man.transform.position;

                        man.DClassConfigs.components.animator.Play(man.additionalAnimations.getItem.name);

                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }
    }
    private void OnDestroy()
    {
        if (onGetKeySound)
        {
            SoundSpots.Generate(transform,onGetKeySound, out AudioSource aSource);
            aSource.spatialBlend = 1;
            aSource.minDistance = 4;
            aSource.maxDistance = 54;
            aSource.rolloffMode = AudioRolloffMode.Linear;
        }
    }
}

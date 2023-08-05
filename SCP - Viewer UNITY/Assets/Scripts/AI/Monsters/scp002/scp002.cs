using System;
using UnityEngine;

[AddComponentMenu("SCP/002 - meat Room")]
public class scp002 : MonoBehaviour
{
    [SerializeField] private Transform          trigger;
    [SerializeField] private ParticleSystem     particles;
    [SerializeField] private DoorProperties     doorProperties;
    [SerializeField] private Boolean            isClosed        = false;
    [SerializeField] private Boolean            isDoorAudio     = false;
    [SerializeField] private Int16              anxioty;
    [SerializeField] private AudioSwitcher      audioSwitcher;

    private void FixedUpdate        ()
    {
        Collider[] collided = Physics.OverlapBox(trigger.transform.position, trigger.transform.lossyScale, trigger.transform.rotation);
        DoorTransform();
        isClosed = isOtherCollided(collided);
        if      (isClosed)
        {
            anxioty++;
        }
        else if (!isClosed && anxioty > 0)
        {
            anxioty--;
        }

        if(collided.Length>0)
        {
            KillMans(collided);
        }

        if (!isClosed && anxioty<5)
        {
            particles.Emit(1);
        }
    }
    private bool isOtherCollided    (in Collider[] collided)
    {       
        return collided.Length > 0 ? true : false;
    }
    private void KillMans           (in Collider[] collided)
    {
        if (anxioty > 256)
        {
            anxioty = 500;

            GameObject[] mens = default;
            if (mens.Length != 0)
            {
                for (Int16 i = 0; i < mens.Length; i++)
                {
                    Destroy(mens[i]);
                }
            }
            RenderSettings.fogDensity = 0;
        }
    }
    private void DoorTransform      ()
    {
        if (anxioty > 256)
        {
            doorProperties.door.localPosition = Vector3.Lerp(doorProperties.door.localPosition, doorProperties.startPos, 0.65f);
            isDoorAudio = true;
        }
        else if (anxioty < 5 )
        {
            doorProperties.door.localPosition = Vector3.Lerp(doorProperties.door.localPosition, doorProperties.finalPos, 0.05f);
            isDoorAudio = false;
        }
        if (isDoorAudio) audioSwitcher.FixedPlay();
    }
    

    [System.Serializable]
    public struct DoorProperties
    {
        public Vector3      startPos;
        public Vector3      finalPos;
        public Transform    door;
    }
}



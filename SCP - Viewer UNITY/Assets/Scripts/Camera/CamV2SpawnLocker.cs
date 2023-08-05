using UnityEngine;

public sealed class CamV2SpawnLocker : MonoBehaviour
{
    public enum LockerMode          {Default, Manual_movement }
    public LockerMode lockerMode    = LockerMode.Default;

    private void OnEnable()
    {
        //Debug.Log("SpawnBlocked");
        CamMove_v2.instance.isWindowOpened = true;
        if (lockerMode is LockerMode.Manual_movement)
        {
            CamMove_v2.instance.isManualMovement = true;
        }
    }
    private void OnDisable()
    {
        //Debug.Log("SpawnUnblocked");
        CamMove_v2.instance.isWindowOpened = false;
        if (lockerMode is LockerMode.Manual_movement)
        {
            CamMove_v2.instance.isManualMovement = false;
        }
    }
}

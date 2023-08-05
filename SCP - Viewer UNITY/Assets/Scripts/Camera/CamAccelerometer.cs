using UnityEngine;

public sealed class CamAccelerometer : MonoBehaviour
{
    [SerializeField]    private float       accelerometerSensitive   = 4f;
    [SerializeField]    private float       lerpSensitive            = 0.1f;

    [HideInInspector]   public  Quaternion  startQuaternion;

    private Quaternion  lerpQuaternion;
    private Camera      ccamera;


    private void Start()
    {
        ccamera = GetComponent<Camera>();
        startQuaternion = ccamera.transform.rotation;
    }
    private void Update()
    {
        lerpQuaternion = GetAcceleratedQuaternion();
        ccamera.transform.rotation = Quaternion.Lerp(ccamera.transform.rotation, lerpQuaternion, lerpSensitive);
    }
    private Quaternion GetAcceleratedQuaternion()
    {
        return new Quaternion(startQuaternion.x + Accelerated().x, startQuaternion.y + Accelerated().y, startQuaternion.z + Accelerated().z, startQuaternion.w);
    }
    private Quaternion Accelerated()
    {
        Vector3 _accelerometer = Input.acceleration;
        return Quaternion.Euler
            (
            _accelerometer.y * accelerometerSensitive,
            _accelerometer.x * accelerometerSensitive,
            _accelerometer.z
            );
    }
}

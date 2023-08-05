using UnityEngine;

public class CKtransform : MonoBehaviour
{
    public float xpos, ypos, zpos;
    public float xrot, yrot, zrot;
    void Start()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;
        zpos = transform.position.z;

        xrot = transform.rotation.eulerAngles.x;
        yrot = transform.rotation.eulerAngles.y;
        zrot = transform.rotation.eulerAngles.z;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(xpos, ypos, zpos),0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xrot, yrot, zrot), 0.05f);
    }
}

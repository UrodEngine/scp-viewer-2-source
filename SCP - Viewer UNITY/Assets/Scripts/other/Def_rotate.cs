using UnityEngine;
sealed class Def_rotate : MonoBehaviour
{
    /*======================================================*/
    public Vector3 rotateSpeed;
    /*======================================================*/

    void FixedUpdate(){
        transform.Rotate(rotateSpeed, Space.Self);
    }
}

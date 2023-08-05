using UnityEngine;

public sealed class MoveToPoint : MonoBehaviour
{
    public  Vector3  toPoint;
    public  float    speed = 0.3f;

    private void FixedUpdate()
    {
        transform.position += (toPoint - transform.position) * speed;
    }
}

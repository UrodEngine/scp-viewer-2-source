using UnityEngine;

[AddComponentMenu("Start Tools/Camera movement bounds setter")]
public sealed class CameraMovementBoundsSetter : MonoBehaviour
{
    private byte timer = 20;

    [Header("”станавливает границы движени€ камеры на карте")]
    public float minXdist = CamMove_v2.DEFAULT_MIN_X_DIST;
    public float maxXdist = CamMove_v2.DEFAULT_MAX_X_DIST;
    public float minYdist = CamMove_v2.DEFAULT_MIN_Y_DIST;
    public float maxYdist = CamMove_v2.DEFAULT_MAX_Y_DIST;

    public float speed = CamMove_v2.DEFAULT_SPEED_MOVE;

    private void FixedUpdate()
    {
        timer--;
        if (CamMove_v2.instance is null)
        {
            return;
        }
        if (timer <= 0)
        {
            CamMove_v2.instance.minXdist = minXdist;
            CamMove_v2.instance.maxXdist = maxXdist;

            CamMove_v2.instance.minYdist = minYdist;
            CamMove_v2.instance.maxYdist = maxYdist;

            CamMove_v2.instance.cameraMoveSpeed = speed;

            timer = 20;
            return;
        }
    }
}

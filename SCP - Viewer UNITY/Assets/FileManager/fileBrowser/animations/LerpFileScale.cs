using UnityEngine;
[DisallowMultipleComponent]
public class LerpFileScale : MonoBehaviour
{
    #region alterable values
    private SimpleDelayer delayer = new SimpleDelayer(2);

    private readonly Vector3 defaultScale   = Vector3.one;
    private readonly Vector3 zeroScale      = Vector3.zero;
    #endregion


    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    private void FixedUpdate()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            if (transform.position.y > transform.parent.parent.position.y - 5 && transform.position.y < transform.parent.parent.position.y + 5)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, 0.3f);
            }
            else if (transform.position.y < transform.parent.parent.position.y - 5 || transform.position.y > transform.parent.parent.position.y + 5)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, zeroScale, 0.3f);
            }
        }
    }
}

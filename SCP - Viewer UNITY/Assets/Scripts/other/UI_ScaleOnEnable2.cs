using UnityEngine;

[AddComponentMenu("Start Tools/UI/Scale animate on enable")]
public sealed class UI_ScaleOnEnable2 : MonoBehaviour
{
    [SerializeField] private Vector3    resultScale     = new Vector3(1,1,1);
    [SerializeField] private Vector3    atStartScale    = new Vector3(0,0,0);
    [SerializeField] private float      speed = 0.1f;

    private void OnEnable()
    {
        gameObject.transform.localScale = atStartScale;
    }
    private void FixedUpdate()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale,resultScale, speed);
    }

    public void SetZeroScale()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
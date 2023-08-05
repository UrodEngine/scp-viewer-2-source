using UnityEngine;

[AddComponentMenu("Start Tools/UI/Scale animate on enable (Obsolete)")]
public sealed class UI_ScaleAnimate : MonoBehaviour
{
    [SerializeField] private float  speed = 0.1f;
    [SerializeField] private bool   setTo1_1_1All = true;

    private Vector3 standartScale = new Vector3(1,1,1);
    private Vector3 originalScale;

    private void    OnEnable   ()
    {
        originalScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
    private void    FixedUpdate  ()
    {
        gameObject.transform.localScale = Vector3.Lerp(
            gameObject.transform.localScale,
            setTo1_1_1All is true ? standartScale : originalScale,
            speed);
    }

    public      void    SetZeroScale()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}

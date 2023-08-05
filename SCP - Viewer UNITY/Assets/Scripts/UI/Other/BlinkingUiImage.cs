using UnityEngine;
using Image = UnityEngine.UI.Image;
using Timer = System.Timers.Timer;

[DisallowMultipleComponent,AddComponentMenu("UROD Engine/UI/Image blinking")]
public class BlinkingUiImage : MonoBehaviour
{
    [SerializeField, Tooltip("ссылка на UI/Image")] 
    private Image  image;
    [SerializeField, Tooltip("В миллисекундах")]    
    private ushort tickrate    = 128;
    [SerializeField, Range(0f,1f)] 
    private float  minAlpha    = 0.7f;
    [SerializeField, Range(0f, 1f)] 
    private float  maxAlpha    = 1f;
    [SerializeField, Header("Автоматический подбор Image компонента в текущем Transform")] 
    bool autoGetComponent = false;

    private Timer timer;


    private void Start()
    {
        timer = new Timer(tickrate);
        timer.Start();
        timer.Elapsed += Elapse;

        #region component reference check
        if (autoGetComponent)
        {
            image = transform.GetComponent<Image>();
        }
        if (image is null)
        {
            Destroy(this);
            timer.Stop();
            timer.Dispose();
            return;
        }
        #endregion
    }
    private void Elapse(object _object, System.Timers.ElapsedEventArgs _eventArgs)
    {
        try
        {
            MainThreadHandler.AddOther(() => { image.color = new Color(image.color.r, image.color.g, image.color.b, Random.Range(minAlpha, maxAlpha)); });
        }
        catch
        {
            timer.Stop();
            timer.Dispose();
        }
    }
    private void OnDestroy()
    {
        timer.Stop();
        timer.Dispose();
    }
    private void OnApplicationQuit()
    {
        timer.Stop();
        timer.Dispose();
    }
}

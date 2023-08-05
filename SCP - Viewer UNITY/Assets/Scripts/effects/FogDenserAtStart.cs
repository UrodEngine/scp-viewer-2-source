using UnityEngine;

[AddComponentMenu("Start Tools/Set smooth fog at start")]
public sealed class FogDenserAtStart : MonoBehaviour
{
    [SerializeField] private float fogDensityAtStart = 2f;
    [SerializeField] private float speed = 0.02f;
    private async void Start()
    {
        await System.Threading.Tasks.Task.Delay(1);
        RenderSettings.fogDensity = fogDensityAtStart;
        await System.Threading.Tasks.Task.Yield();
    }
    private void FixedUpdate()
    {
        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, SkyboxOnLoad.publicFogDensity, speed*3.1f);
    }
}

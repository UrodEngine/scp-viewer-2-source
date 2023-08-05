using UnityEngine;

[AddComponentMenu("Start Tools/Set skyBox on start")]
public class SkyboxOnLoad : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public static float publicFogDensity = 0;
    public  Material skyboxOnLoad;
    public  float    fogOnLoad;
    public  Color    fogColorOnLoad;
    public  FogMode  fogmodeRS;
    /*=========================================================================================================================================================*/
    #endregion

    void Start()
    {
        publicFogDensity            = fogOnLoad;
        RenderSettings.skybox       = skyboxOnLoad;
        RenderSettings.fogMode      = fogmodeRS;
        RenderSettings.fogColor     = fogColorOnLoad;
        RenderSettings.fogDensity   = fogOnLoad;    
    }
}

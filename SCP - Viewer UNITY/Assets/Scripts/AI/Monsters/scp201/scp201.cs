using UnityEngine;

[AddComponentMenu("SCP/201 - empty world")]
public class scp201 : MonoBehaviour, IPassportData
{
    [SerializeField] private int seconds = 5;
    [SerializeField] private float SkyBoxOnLoad_fog_setter;
    [SerializeField] private Color SkyBox_fog_color_setter;

    public string   aliveName       { get { return "SCP - 201"; } set { } }
    public string   aliveSurname    { get { return "Empty world"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    async void Start()
    {
        await System.Threading.Tasks.Task.Delay(seconds*1000);

        try
        {
            transform.GetChild(0).gameObject.SetActive(true); //enable the trigger - aliveForm destroyer
            SkyboxOnLoad.publicFogDensity   = SkyBoxOnLoad_fog_setter;
            RenderSettings.fogColor         = SkyBox_fog_color_setter;
            RenderSettings.fogDensity       = 0.5f;
        }
        catch
        {
            return;
        }
    }
}

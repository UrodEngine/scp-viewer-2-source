using UnityEngine;

[AddComponentMenu("Start Tools/Delete all baked lights at start")]
public sealed class DeleteAllBakedLightsAtStart : MonoBehaviour
{
    [SerializeField] private Light[] allLights;
    void Start()
    {
        allLights = FindObjectsOfType<Light>();

        foreach (Light light in allLights)
        {
            Destroy(light.gameObject);
        }
        Destroy(this);
    }
}

using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
[AddComponentMenu("Rendering/PostProcessingPlayerPrefs")]
public class PostProcessingPlayerPrefs : MonoBehaviour
{
    private PostProcessLayer   postProcessLayer;
    private PostProcessVolume  postProcessVolume;

    void FixedUpdate()
    {
        TryGetComponent<PostProcessLayer>(out postProcessLayer);
        TryGetComponent<PostProcessVolume>(out postProcessVolume);
        if (PlayerPrefs.GetInt("DpostProcessing") is 1){
            if (postProcessLayer != null) postProcessLayer.enabled = true;
            if (postProcessVolume != null) postProcessVolume.enabled = true;
        }
        else{
            if (postProcessLayer != null) postProcessLayer.enabled = false;
            if (postProcessVolume != null) postProcessVolume.enabled = false;
        }
        if (postProcessLayer != null) PropertiesChange();
    }

    void PropertiesChange()
    {
        postProcessVolume.profile.TryGetSettings<Bloom>(out Bloom bloom);
        bloom.active = PlayerPrefs.GetInt("Dbloom") is 1 ? true : false;
        postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out ChromaticAberration chromatic);
        chromatic.active = PlayerPrefs.GetInt("Dchromatic") is 1 ? true : false;
    }
}

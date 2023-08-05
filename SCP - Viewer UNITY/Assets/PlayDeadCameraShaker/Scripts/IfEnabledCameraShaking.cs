using UnityEngine;

public class IfEnabledCameraShaking : MonoBehaviour
{
    public static IfEnabledCameraShaking ifEnabledStatic;

    private FrustumJitter           _frustumJitter;
    private VelocityBuffer          _velocityBuffer;
    private TemporalReprojection    _temporalReprojection;

    private void Start(){
        ifEnabledStatic         = this;
        _frustumJitter          = GetComponent<FrustumJitter>();
        _velocityBuffer         = GetComponent<VelocityBuffer>();
        _temporalReprojection   = GetComponent<TemporalReprojection>();
        CheckBlurOptions();
    }
    public  void CheckBlurOptions(){
        _frustumJitter.enabled          = PlayerPrefs.GetInt("MBlur") is 1 ? true : false;
        _velocityBuffer.enabled         = PlayerPrefs.GetInt("MBlur") is 1 ? true : false;
        _temporalReprojection.enabled   = PlayerPrefs.GetInt("MBlur") is 1 ? true : false;
    }
}

using UnityEngine;

[AddComponentMenu("SCP Tools/Dclass/Mens Idle Animations")]
public class DclassRandomIdleAnimations : MonoBehaviour
{
    [Header("This component will be destroyed at start")]
    [SerializeField] Man                menParameters;
    [SerializeField] AnimationClip[]    animations;
    private void Start()
    {
        menParameters.DClassConfigs.OnThinked += () => 
        {
            if (Random.Range(0, 100) > 60)
            {
                menParameters.DClassConfigs.components.animator.Play(animations[Random.Range(0, animations.Length)].name, 0);
            }
        };
        Destroy(this);
    }
}

using UnityEngine;
public class AnimatorTimer : MonoBehaviour
{
    public string StateName;
    private Animator animator => GetComponent(nameof(Animator)) as Animator;
    void FixedUpdate() => animator.SetInteger(StateName, Mathf.Clamp(animator.GetInteger(StateName) - 1, 0, 999));
}

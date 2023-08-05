using UnityEngine;

public class AnimatorSpeedRandomizer : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public  Animator animator;
    public  float    SubstractValue;
    public  int      MinSpeed;
    public  int      MaxSpeed;
    /*=========================================================================================================================================================*/
    #endregion

    void Start(){
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(MinSpeed, MaxSpeed)*SubstractValue;
    }
}

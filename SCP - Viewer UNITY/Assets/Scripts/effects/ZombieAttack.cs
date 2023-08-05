using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    [SerializeField]    private     GameObject[]        Effects;
                        private     Animator            _animator;
                        private     AnimatorStateInfo   _animatorStateInfo;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion

    void Start  ()  {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update ()  {       
        foreach (var item in Effects)
        {
            foreach (AnimatorClipInfo slot in _animator.GetCurrentAnimatorClipInfo(0))
            {
                item.SetActive(slot.clip.name == "DclassBones|ZAttack" ? true : false);
            }    
        }
    }
}

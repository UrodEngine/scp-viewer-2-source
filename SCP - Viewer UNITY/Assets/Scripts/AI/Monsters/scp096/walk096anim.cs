
using UnityEngine;

public class walk096anim : MonoBehaviour
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private Animator    _animSource;
    private int         _layer = 2;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    void Start()
    {
        _animSource = GetComponent<Animator>();
    }
    void Update()
    {       
        _animSource.SetLayerWeight(_layer, _animSource.GetBool("IsWalking") is true ? 
            _animSource.GetLayerWeight(_layer) + (1 - _animSource.GetLayerWeight(_layer)) * 0.1f
            :
            _animSource.GetLayerWeight(_layer) + (0 - _animSource.GetLayerWeight(_layer)) * 0.1f
            );
    }
}

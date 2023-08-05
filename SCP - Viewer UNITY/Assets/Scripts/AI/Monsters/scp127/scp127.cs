using UnityEngine;

[AddComponentMenu("SCP/127 - alive mp5 weapon")]
public class scp127 : MonoBehaviour
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private int         _timerToIncreaseAmmo;
    private bool        _parentFindedOnce;

    [SerializeField] private Man      _dclassInstance = null;
    [SerializeField] private GameObject  _disParent;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void    LateUpdate  ()  
    {
        _timerToIncreaseAmmo = _timerToIncreaseAmmo > 0 ? _timerToIncreaseAmmo - 1 : AddPatron();

        if (transform.parent is null || _dclassInstance != null) return;
        SearchDclass();
    }
    private int     AddPatron   ()  
    {
        if (_dclassInstance is null) return 256;
        _dclassInstance.patrons = _dclassInstance.patrons < (short)32 ? (short)(_dclassInstance.patrons + 1) : _dclassInstance.patrons;
        return 256;
    }
    private void    SearchDclass()  
    {
        if (_parentFindedOnce is false)
        {
            _disParent = transform.parent.gameObject;
            _parentFindedOnce = true;
        }
        if (_dclassInstance is null)
        {
            if (!_disParent.GetComponent<Man>())
            {
                _disParent = _disParent.transform.parent.gameObject;
            }
            else
            {
                _dclassInstance = _disParent.GetComponent<Man>();
            }
        }
    }
}

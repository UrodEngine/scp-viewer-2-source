using UnityEngine;

public sealed class CheckInformed : MonoBehaviour
{
    
    #region Alterable value
    /// Сохраняет начальный размер объекта по осям

    private float   _savedXscale;
    private float   _savedYscale;
    private float   _savedZscale;

    /// Флаги, включает <see Lerp методы> сжатия и разжатия по осям
    
    public bool     isXlerp = true;
    public bool     isYlerp = false;
    public bool     isZlerp = false;

    /// Скорость сжатия и разжатия <see Lerp метода>

    public float    speed = 0.1f;

    /// Скорость сжатия и разжатия <see Lerp метода>
    public BTN_is_Informed checkLocal;
    #endregion

    private void    Start   (){
        _savedXscale = transform.localScale.x;
        _savedYscale = transform.localScale.y;
        _savedZscale = transform.localScale.z;
    }
    private void    Update  (){
        //===============================================================================================================
        ///Тернарный оператор сжимания и расжатия, если  <see STATIC> <see BTN_is_Informed.isInformed> равен TRUE
        //===============================================================================================================
        if (checkLocal is null) 
            transform.localScale = BTN_is_Informed.isInformed ?
            Vector3.Lerp(transform.localScale, new Vector3(
                _savedXscale,
                _savedYscale,
                _savedZscale),
                speed) //Normal
            :
            Vector3.Lerp(transform.localScale, new Vector3(
                isXlerp ? 0 : _savedXscale,
                isYlerp ? 0 : _savedYscale,
                isZlerp ? 0 : _savedZscale),
                speed); //Lerped
        else 
            transform.localScale = checkLocal.isLocalInformed ?
            Vector3.Lerp(transform.localScale, new Vector3(
                _savedXscale,
                _savedYscale,
                _savedZscale),
                speed) //Normal
            :
            Vector3.Lerp(transform.localScale, new Vector3(
                isXlerp ? 0 : _savedXscale,
                isYlerp ? 0 : _savedYscale,
                isZlerp ? 0 : _savedZscale),
                speed); //Lerped
    }
}

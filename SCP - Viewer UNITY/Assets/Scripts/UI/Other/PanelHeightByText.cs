using UnityEngine.UI;
using UnityEngine;

public class PanelHeightByText : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    private RectTransform   _rTransform;
    private Text            _texter;
    private float           deleterValue = 17.25f;
    private bool            updateTextPanelHeightSize = false;
    private ushort          linesOfCachedText;
    /*=========================================================================================================================================================*/
    #endregion

    private void Start(){
        _rTransform = GetComponent<RectTransform>();
        _texter     = GetComponent<Text>();
    }
    private void FixedUpdate(){
        if (updateTextPanelHeightSize)
        {
            _rTransform.sizeDelta = new Vector2(4236.4f, 300f * (_texter.text.Length / deleterValue));
            if (_texter.cachedTextGenerator.lineCount>1)
            {
                linesOfCachedText = (ushort)_texter.cachedTextGenerator.lineCount;
                updateTextPanelHeightSize = false;
            }
        }
        else
        {
            _rTransform.sizeDelta = new Vector2(4236.4f, linesOfCachedText * _texter.fontSize);
        }
    }
    private void OnEnable()
    {
        updateTextPanelHeightSize = true;
    }
    private void OnDisable()
    {
        _texter.text = null;
        linesOfCachedText = 0;
        _texter.fontSize = 248;
    }
}

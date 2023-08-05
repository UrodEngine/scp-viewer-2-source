using UnityEngine;

public class numeric_gen : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.InputField  inputField;
    [SerializeField] private UnityEngine.UI.Text        barCodeText;
    [SerializeField] private UnityEngine.UI.Text        releaseCodeText;


    private void Start()
    {
        inputField.onValueChanged.AddListener(delegate { BarCodeConvert();ReleaseCodeConvert(); });
    }

    private void Update()
    {

    }
    private void ReleaseCodeConvert()
    {
        System.Char[]   chars = inputField.text.ToCharArray();
        System.Int32[]  ints = new System.Int32[chars.Length];
        releaseCodeText.text = "";
        for (int i = 0; i < chars.Length; i++)
        {
            double temp = System.Char.GetNumericValue(chars[i]);
            ints[i] = (int)temp;
            releaseCodeText.text = releaseCodeText.text + System.Math.Abs(ints[i]);
        }      
    }
    private void BarCodeConvert()
    {
        System.Char[] chars = inputField.text.ToCharArray();
        System.Int32[] ints = new System.Int32[chars.Length];
        barCodeText.text = "";
        for (int i = 0; i < chars.Length; i++)
        {
            ints[i] = chars[i].GetHashCode();
            ints[i] = ints[i] % 2;

            barCodeText.text = $"{barCodeText.text + (ints[i] == 0 ? "||":" |")}";
        }
    }
}

using UnityEngine;

public sealed class Localizator_local : MonoBehaviour
{
    #region Alterable values
    /*========================================================================================================================*/
    private char            betweenWords = ';';
    private char            lineSeparator = '\n';
    public  string          gettedText;
    public  string[]        ReadedText;
    public  TextAsset       FileToRead;
    public  bool            LockTextAnchorChange = false;
    /*========================================================================================================================*/
    #endregion

    private void OnEnable       ()
    {
        ReadAndApply();
    }
    private void Start          ()
    {
        ReadAndApply();
    }
    private void ReadAndApply   ()
    {
        ReadedText = FileToRead?.text?.Split('§');
        ContinueOutPut();
    }
    private void ContinueOutPut ()
    {
        if (FileToRead != null && ReadedText != null)
        {
            int languageMultiplier          = System.Enum.GetNames(typeof(LocalizatorClass.Languager)).Length; //Определить кол-во указанных языков локализации.
            UnityEngine.UI.Text text_comp   = GetComponent<UnityEngine.UI.Text>();
            text_comp.text                  = ReadedText[(int)LocalizatorClass.language];
            if(LockTextAnchorChange is false)
            {
                if (LocalizatorClass.language == LocalizatorClass.Languager.arab)
                {
                    text_comp.alignment = TextAnchor.UpperRight;
                }
                else
                {
                    text_comp.alignment = TextAnchor.UpperLeft;
                }
            }
        }
        else
        {
            UnityEngine.UI.Text text_comp   = GetComponent<UnityEngine.UI.Text>();
            text_comp.text                  = "Отсутствуют строки информации в CSV файле локализации";
            Debug.Log("Информация отсутствует, обновите список объектов, либо дополните CSV файл локализации");
        }
    }
    private void Update         ()
    {
        if (LocalizatorClass.isUpdating is false)
        {
            return;
        }
        ReadAndApply();
    }
}

using UnityEngine;
using UnityEngine.UI;
public sealed class Localizator_SCP : MonoBehaviour
{
    #region Alterable values
    /*=======================   Alterable values   ===========================================================================*/
    public      enum        TOfLtr          { menu = 0, descriptions = 1 }

    /// <summary> Menu - �������� ����� ��������� �����, Description - ����� ��� ���������� ������. </summary>
    public      TOfLtr      LocalizatorMode; 

    /// <summary> ���-��������������� ��������</summary>
    private     char        betweenWords    = ';';

    /// <summary> �������� ����� �����</summary>
    private     char        lineSeparator   = '\n';

    /// <summary> �������������� �����, ������ �� ��������� ������ � ������� � �������������. </summary>
    public      string      gettedText;

    /// <summary> ��� ����� ������ ����������� ����� </summary>
    public      string[]    ReadedText;

     /// <summary> ������ �� ���� ����������� </summary>
    public      TextAsset   CsvFile , SCP_file, PROP_file, NPC_file;

    /// <summary> �����, ������������, ����� ������ �� ������ ����� ��������</summary>
                [Header ("���� ��������� ����� ����� localizator_class"                 )]
                [Tooltip("����� �� ��������� ������, � ������� ���������� �����������"  )]
    public      int         lineToRead;
    /*========================================================================================================================*/
    #endregion



    private void    Start               ()
    {
        SCP_file = Resources.Load<TextAsset>($"Localization/Descriptions/SCP/SCP-173");
    }
    public  void    Update              ()
    {
        if (LocalizatorClass.isUpdating)
        {
            UpdateLocalization();
        }
    }
    private void    OnEnable            ()
    {
        UpdateLocalization();
    }

    #region read csv's
    public  void    ReadCSV_Menu        ()
    {
        ReadedText                      = CsvFile.text.Split(lineSeparator);
        string[]    localizated_words   = ReadedText[lineToRead].Split(betweenWords);
        Text        text_comp           = GetComponent<Text>();

        text_comp.text  = localizated_words[(int)LocalizatorClass.language + 1];
        gettedText      = localizated_words[(int)LocalizatorClass.language + 1];
    }
    private void ReadTextAsset(in string path)
    {
        PROP_file = Resources.Load<TextAsset>(path);
        ReadedText = PROP_file?.text?.Split('�');
    }
    private void ReadString(string input)
    {
        ReadedText = input.Split('�');
    }
    #endregion

    public async  void    UpdateLocalization  (){

        LocalizatorClass.language = (LocalizatorClass.Languager)PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_LANGUAGE);

        if (LocalizatorMode == TOfLtr.menu)
        {
            ReadCSV_Menu();
        }
        else if (LocalizatorMode == TOfLtr.descriptions)
        {
            await System.Threading.Tasks.Task.Delay(64);

            ReadCSV_Description();
        }
        await System.Threading.Tasks.Task.Yield();
    }
    public async  void    ReadCSV_Description ()
    {
        int delaySpeed = 150;
        await System.Threading.Tasks.Task.Delay(delaySpeed);

        string modTextAsset = null;
        if (SPAWN_lists.instance != null)
        {
            SPAWN_lists.UltimateModReferences modRef = SPAWN_lists.instance.TryGetModResources(SPAWN_lists.instance.SelectedInstance.name);
            if (modRef != null)
            {
                modTextAsset = modRef.description;
            }
        }
        if (modTextAsset != null)
        {
            ReadString(modTextAsset);
        }
        else
        {
            switch (SPAWN_lists.instance.ArrayMode)
            {
                case SPAWN_lists.ArraySPAWNLIST.Prop:       ReadTextAsset($"{Directories.DIR_PROP}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
                case SPAWN_lists.ArraySPAWNLIST.NPC:        ReadTextAsset($"{Directories.DIR_NPC}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
                case SPAWN_lists.ArraySPAWNLIST.SCP:        ReadTextAsset($"{Directories.DIR_SCP}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
                case SPAWN_lists.ArraySPAWNLIST.Weapons:    ReadTextAsset($"{Directories.DIR_WEAPONS}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
                case SPAWN_lists.ArraySPAWNLIST.Tools:      ReadTextAsset($"{Directories.DIR_TOOLS}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
                case SPAWN_lists.ArraySPAWNLIST.Maps:       ReadTextAsset($"{Directories.DIR_MAPS}{SPAWN_lists.instance.SelectedInstance?.name}");
                    break;
            }
        }



        if (SCP_file != null && ReadedText != null)
        {
            int     languageCount   = System.Enum.GetNames(typeof(LocalizatorClass.Languager)).Length; //���������� ���-�� ��������� ������ �����������.
            Text    textComponent   = GetComponent<Text>();
            textComponent.text      = ReadedText[(int)LocalizatorClass.language];

            if (LocalizatorClass.language == LocalizatorClass.Languager.arab)
            {
                textComponent.alignment = TextAnchor.UpperRight;
            }
            else
            {
                textComponent.alignment = TextAnchor.UpperLeft;
            }
        }
        else
        {
            Text text_comp   = GetComponent<Text>();
            text_comp.text  = "CSV localization file is not exist or interrupted";
        }
        await System.Threading.Tasks.Task.Yield();
    }      


    public readonly struct Directories
    {
        public const string DIR_SCP     = "Localization/Descriptions/SCP/"      ;
        public const string DIR_NPC     = "Localization/Descriptions/NPC/"      ;
        public const string DIR_PROP    = "Localization/Descriptions/Prop/"     ;
        public const string DIR_WEAPONS = "Localization/Descriptions/Weapons/"  ;
        public const string DIR_TOOLS   = "Localization/Descriptions/Tools/"    ;
        public const string DIR_MAPS    = "Localization/Descriptions/Maps/"     ;
    }
}
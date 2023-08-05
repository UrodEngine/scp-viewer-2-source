/// <summary>
/// readonly ���������, ���������� � ���� ��������� - ����� ������ PlayerPrefs.
/// </summary>
public readonly struct PlayerPrefsKeys
{
    /// <summary> ����������. ���������� ������� �����. </summary> <remarks> System.Int32 </remarks>
    public const string STATISTICS_DEADMENS             = "statistic_deadMens";
    /// <summary> ���������, ��� �� �������� ������ ������ ������� </summary>
    public const string STATISTICS_FIRST_LOAD           = "F_load";
    /// <summary> ��������� DPI <br/> 1 = min, 3 = max</summary>
    public const string OPTIONS_DPI                     = "DPI";
    /// <summary> ��������� �������� ������� <br/>  0 = max, 2 = min</summary>
    public const string OPTIONS_TEXTURE_QUALITY         = "Qtexture";
    /// <summary> ��������� �������� ��� �������� </summary>
    public const string OPTIONS_MOTION_BLUR             = "MBlur";
    /// <summary> ��������� �������� ��� �������� </summary>
    public const string OPTIONS_FPS                     = "MFps";
    /// <summary> ��������� ��������� ��� ���������� ������ ������� � �������� </summary>
    public const string OPTIONS_DECAL_DETAILS           = "Ddetails";
    /// <summary> ��������� ��������� ��� ���������� ����-��������� </summary>
    public const string OPTIONS_POST_PROCESSING         = "DpostProcessing";
    /// <summary> ��������� ��������� ��� ���������� �������� </summary>
    public const string OPTIONS_BLOOM                   = "Dbloom";
    /// <summary> ��������� ��������� ��� ���������� ������������� ��������� </summary>
    public const string OPTIONS_CHROMATIC               = "Dchromatic";
    /// <summary> ��������� ����� ���� <br/> ������ ������: "<see langword="float,float,float,float"/>" <br/>������: "<see langword="1f,1f,1f,1f"/>" <br/>��������: <see langword="red,green,blue,alpha"/>" </summary>
    public const string OPTIONS_NOISE_COLOR             = "NoiseColor";
    /// <summary> ��������� ������� ���������� ����� </summary>
    public const string OPTIONS_NOISE_COLOR_INDEX       = "NoiseColorID";
    /// <summary> ��������� ����������� ����������� ����, ��� �������. � ����������� ��������� ��� ������ ���� ������������� </summary>
    public const string OPTIONS_NOISE_ENABLED           = "NoiseEnabled";
    /// <summary> ��������� ����� ���� </summary>
    public const string OPTIONS_LANGUAGE                = "Lang";
    /// <summary> ��������� ��������� ��� ���������� ������ ����������� � �������� ���������</summary>
    public const string OPTIONS_FILE_MANAGER_PREVIEWS   = "FM_preview";

    #region ADS KEYS
    public const string OBJECTS_LIST_ADS        = "CameraADS_turn";
    public const string AFTER_SCENE_MENU_ADS    = "InterADS_turn";
    #endregion
}

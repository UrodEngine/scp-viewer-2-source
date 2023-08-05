/// <summary>
/// readonly структура, содержащая в себе константы - ключи класса PlayerPrefs.
/// </summary>
public readonly struct PlayerPrefsKeys
{
    /// <summary> Статистика. Количество мертвых людей. </summary> <remarks> System.Int32 </remarks>
    public const string STATISTICS_DEADMENS             = "statistic_deadMens";
    /// <summary> Описывает, был ли совершен первый запуск игроком </summary>
    public const string STATISTICS_FIRST_LOAD           = "F_load";
    /// <summary> настройка DPI <br/> 1 = min, 3 = max</summary>
    public const string OPTIONS_DPI                     = "DPI";
    /// <summary> настройка качества текстур <br/>  0 = max, 2 = min</summary>
    public const string OPTIONS_TEXTURE_QUALITY         = "Qtexture";
    /// <summary> настройка размытия при движении </summary>
    public const string OPTIONS_MOTION_BLUR             = "MBlur";
    /// <summary> настройка размытия при движении </summary>
    public const string OPTIONS_FPS                     = "MFps";
    /// <summary> настройка включения или отключения мелких деталей и эффектов </summary>
    public const string OPTIONS_DECAL_DETAILS           = "Ddetails";
    /// <summary> настройка включения или отключения пост-обработки </summary>
    public const string OPTIONS_POST_PROCESSING         = "DpostProcessing";
    /// <summary> настройка включения или отключения свечения </summary>
    public const string OPTIONS_BLOOM                   = "Dbloom";
    /// <summary> настройка включения или отключения хроматической абберации </summary>
    public const string OPTIONS_CHROMATIC               = "Dchromatic";
    /// <summary> настройка цвета шума <br/> формат записи: "<see langword="float,float,float,float"/>" <br/>Пример: "<see langword="1f,1f,1f,1f"/>" <br/>Описание: <see langword="red,green,blue,alpha"/>" </summary>
    public const string OPTIONS_NOISE_COLOR             = "NoiseColor";
    /// <summary> настройка индекса выбранного цвета </summary>
    public const string OPTIONS_NOISE_COLOR_INDEX       = "NoiseColorID";
    /// <summary> настройка отображения изображения шума, как объекта. В выключенном состоянии шум должен быть деактивирован </summary>
    public const string OPTIONS_NOISE_ENABLED           = "NoiseEnabled";
    /// <summary> настройка языка игры </summary>
    public const string OPTIONS_LANGUAGE                = "Lang";
    /// <summary> настройка включения или отключения превью изображений в файловом менеджере</summary>
    public const string OPTIONS_FILE_MANAGER_PREVIEWS   = "FM_preview";

    #region ADS KEYS
    public const string OBJECTS_LIST_ADS        = "CameraADS_turn";
    public const string AFTER_SCENE_MENU_ADS    = "InterADS_turn";
    #endregion
}

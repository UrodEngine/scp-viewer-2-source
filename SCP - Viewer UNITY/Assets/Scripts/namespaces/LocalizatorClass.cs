public static class LocalizatorClass
{
    public enum Languager { eng = 0, rus = 1, italian = 2, arab = 3 };
    public static Languager language { get; set; }
    /// <summary> ���� ���� �������, ��� MonoBehaviour ������������ ��������� ���� ����� </summary>
    public static bool isUpdating { get; set; }
}

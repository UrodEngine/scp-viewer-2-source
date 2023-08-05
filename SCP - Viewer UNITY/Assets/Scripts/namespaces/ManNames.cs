///<summary>������������ ���������, �������� ������ ���� � �������</summary> 
public readonly struct ManNames 
{
    /// <summary> ��������� �����������</summary>
    public  static   System.Random   systemRandomizer   = new System.Random(); 
    /// <summary> ����� </summary>
    public  static   string[]        names              = new string[]{
        //------------------16
        "James",
        "Robert",
        "John",
        "Michael",
        "William",
        "David",
        "Richard",
        "Joseph",
        "Thomas",
        "Charles",
        "Christopher",
        "Daniel",
        "Matthew",
        "Anthony",
        "Mark",
        "Donald",
        //------------------32
        "Peter",
        "Nick",
        "Valeriy",
        "Louis",
        "Bill",
        "Walter",
        "William",
        "Zachariah",
        "Sebastian",
        "Saul",
        "Oscar",
        "Max",
        "Mortimer",
        "Anton",
        "Ivan",
        "Umar",
        "Hasbulla",
        "Mauricio",
        //------------------21.04.2022
        "Mustafa",
        "Nikos",
        "Abdul",
    };
    /// <summary> ������� </summary>
    public  static   string[]        surnames           = new string[] {
        //------------------16
        "Rezo",
        "Farenick",
        "Cirss",
        "Bambanss",
        "Denil",
        "Nelso",
        "Haci",
        "Qoe",
        "Zebu",
        "Zebul",
        "Shora",
        "Vynek",
        "Birara",
        "Overse",
        "Kuleev",
        "Auriche",
        //------------------32
        "Hoxha",
        "Shebu",
        "Weber",
        "Becker",
        "Boyko",
        "Rudenko",
        "Petrenko",
        "Demir",
        "Kaya",
        "Navarro",
        "Diaz",
        "Mohammed",
        "Rodin",
        "Iglesias",
        "Ortiz",
        "Sanz",
    };


    /// <summary> �������� ��������� ��� �� ������ ����</summary>
    public static  string  TakeName() 
    {
        int     number = systemRandomizer.Next(0, names.Length);
        return  names[number];
    }

    /// <summary> �������� ��������� ������� �� ������ �������</summary>
    public static  string  TakeSurname() 
    {
        int     number2 = systemRandomizer.Next(0, surnames.Length);
        return  surnames[number2];
    }
}

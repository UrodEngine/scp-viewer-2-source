using UnityEngine;


public struct MathUE
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public static System.Random randomizer = new System.Random();
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    /// <summary> 
    /// ------------------------------------------------------------------------------- <br/>
    /// ���� ������� ����� ����� <see langword="pointA_1"/> � <see langword="pointA_2"/>. <br/>
    /// �� ����� PointA_output �������� ������� ����� <see langword="pointB_1"/> � <see langword="pointB_2"/>. <br/>
    /// �������� ���� ������ �� <see langword="sensitive"/> (������ : 0.1f) <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// ������ ������������� - ��� ������ <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// </summary>
    public static float DistanceOf2points   (in float pointA_1, in float pointA_2, in float pointB_1, in float pointB_2, in float sensitive)
    {
        return (Mathf.Abs(((pointA_1 - pointA_2) / 2)) - Mathf.Abs(((pointB_1 - pointB_2) / 2))) * sensitive;
    }
    public static int   GetRandom           (in int min, in int max) => randomizer.Next(min, max);
}


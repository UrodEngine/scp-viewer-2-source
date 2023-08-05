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
    /// Ищет среднюю точку между <see langword="pointA_1"/> и <see langword="pointA_2"/>. <br/>
    /// От итога PointA_output отнимает среднюю точку <see langword="pointB_1"/> и <see langword="pointB_2"/>. <br/>
    /// Конечный итог множит на <see langword="sensitive"/> (Пример : 0.1f) <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// Пример использования - Зум камеры <br/>
    /// ------------------------------------------------------------------------------- <br/>
    /// </summary>
    public static float DistanceOf2points   (in float pointA_1, in float pointA_2, in float pointB_1, in float pointB_2, in float sensitive)
    {
        return (Mathf.Abs(((pointA_1 - pointA_2) / 2)) - Mathf.Abs(((pointB_1 - pointB_2) / 2))) * sensitive;
    }
    public static int   GetRandom           (in int min, in int max) => randomizer.Next(min, max);
}


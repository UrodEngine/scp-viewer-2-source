using UnityEngine;
using UnityEngine.UI;
using FractalMandelbrotURODEngine.Float;
using FractalMandelbrotURODEngine.Decimal;
using FractalMandelbrotURODEngine.Double;
/// <summary>
/// ---------------------------------------------------------------<br/>
/// <see cref="FractalMandelbrotURODEngine.Float.MandelbrotFractalFloat"/>   - Самое низкокачественное и быстрое изображение. <br/>
/// <see cref="FractalMandelbrotURODEngine.Double.MandelbrotFractalDouble"/>  - Обычное и средней скорости изображение. <br/>
/// <see cref="FractalMandelbrotURODEngine.Decimal.MandelbrotFractalDecimal"/> - Самое требовательное и медленное изображение. <br/>
/// ---------------------------------------------------------------<br/>
/// </summary>
[ExecuteInEditMode]
public sealed class FractalCreate : MonoBehaviour
{
    #region values
    //-----------------------------------------------------------------------------------------
    private MandelbrotFractalFloat fractal = new MandelbrotFractalFloat();

    private byte[]      buffer;
    private const byte  res     = 2;
    private ushort      width   = 128;
    private ushort      height  = 128;

    private SimpleDelayer delayer = new SimpleDelayer(3);

    private float randomWx;
    private float randomWy;

    private RawImage raw;
    //-----------------------------------------------------------------------------------------

    #endregion


    private void Start()
    {
        fractal.parameters.width = width;
        fractal.parameters.height = height;
        fractal.parameters.res = res;

        raw = GetComponent<RawImage>();
        randomWx = Random.Range(-5.3f, 5.3f);
        randomWy = Random.Range(-5.3f, 5.3f);
        Draw();
    }
    private void FixedUpdate()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            fractal.zoom -= fractal.zoom * 0.1f;
            fractal.wx -= randomWx * fractal.zoom;
            fractal.wy -= randomWy * fractal.zoom;

            Draw();
        }
    }
    /// <summary>
    /// ------------------------------------------------------------------------------------------------------<br/>
    /// <see cref="MandelbrotFractalFloat.Draw"/> возвращает одномерный массив, равный количеству пикселей отрендеренного изображения фрактала.<br/>
    /// Если размеры в <see cref="MandelbrotFractalFloat.parameters"/> равны 128 х 128, то массив на выходе будет равен 128*128 <br/>
    /// Элементы массива являются значением яркости пикселя. А сам массив описывает собой изображение фрактала<br/>
    /// ------------------------------------------------------------------------------------------------------<br/>
    /// </summary>
    private void Draw()
    {
        // Нарисовать фрактал и вернуть изображение в виде массива, а после преобразовать его из int[] в byte[]
        int[] gettedIntBuffer   = fractal.Draw();
        buffer                  = new byte[gettedIntBuffer.Length];

        for (ushort index = 0; index < gettedIntBuffer.Length; index++)
        {
            buffer[index] = (byte)gettedIntBuffer[index];
        }

        // Создать изображение, в котором будет фрактал
        Texture2D   texture = new Texture2D(width / res, height / res);
        texture.name = $"fractal_{width}x{height}_res{res}";

        // Установить пикселям цвет, взятый из каждого элемента массива фрактала
        for (ushort x = 0; x < width / res; x++)
        {
            for (ushort y = 0; y < height / res; y++)
            {
                texture.SetPixel(x, y, new Color(buffer[x * width/ res + y] * 0.01f, buffer[x * width/ res + y] * 0.01f, buffer[x * width/ res + y] *0.01f, 1));
            }
        }
        // Применить
        texture.Apply();
        raw.texture = texture;
    }
}

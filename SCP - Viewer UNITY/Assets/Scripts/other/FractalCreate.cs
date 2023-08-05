using UnityEngine;
using UnityEngine.UI;
using FractalMandelbrotURODEngine.Float;
using FractalMandelbrotURODEngine.Decimal;
using FractalMandelbrotURODEngine.Double;
/// <summary>
/// ---------------------------------------------------------------<br/>
/// <see cref="FractalMandelbrotURODEngine.Float.MandelbrotFractalFloat"/>   - ����� ����������������� � ������� �����������. <br/>
/// <see cref="FractalMandelbrotURODEngine.Double.MandelbrotFractalDouble"/>  - ������� � ������� �������� �����������. <br/>
/// <see cref="FractalMandelbrotURODEngine.Decimal.MandelbrotFractalDecimal"/> - ����� �������������� � ��������� �����������. <br/>
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
    /// <see cref="MandelbrotFractalFloat.Draw"/> ���������� ���������� ������, ������ ���������� �������� �������������� ����������� ��������.<br/>
    /// ���� ������� � <see cref="MandelbrotFractalFloat.parameters"/> ����� 128 � 128, �� ������ �� ������ ����� ����� 128*128 <br/>
    /// �������� ������� �������� ��������� ������� �������. � ��� ������ ��������� ����� ����������� ��������<br/>
    /// ------------------------------------------------------------------------------------------------------<br/>
    /// </summary>
    private void Draw()
    {
        // ���������� ������� � ������� ����������� � ���� �������, � ����� ������������� ��� �� int[] � byte[]
        int[] gettedIntBuffer   = fractal.Draw();
        buffer                  = new byte[gettedIntBuffer.Length];

        for (ushort index = 0; index < gettedIntBuffer.Length; index++)
        {
            buffer[index] = (byte)gettedIntBuffer[index];
        }

        // ������� �����������, � ������� ����� �������
        Texture2D   texture = new Texture2D(width / res, height / res);
        texture.name = $"fractal_{width}x{height}_res{res}";

        // ���������� �������� ����, ������ �� ������� �������� ������� ��������
        for (ushort x = 0; x < width / res; x++)
        {
            for (ushort y = 0; y < height / res; y++)
            {
                texture.SetPixel(x, y, new Color(buffer[x * width/ res + y] * 0.01f, buffer[x * width/ res + y] * 0.01f, buffer[x * width/ res + y] *0.01f, 1));
            }
        }
        // ���������
        texture.Apply();
        raw.texture = texture;
    }
}

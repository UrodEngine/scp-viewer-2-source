using UnityEngine;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class Signs_offset : MonoBehaviour
{
    public Vector2      OffsetField;  
    public Vector2      ScaleField;
    public Vector2      OffsetSpeed;
    public bool         EnableMove;
    public sbyte        _matIndex;  //Если материалов несколько, выбрать нужный для изменений
    private Material    _material;
    private Renderer    _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();      
    }
    private void FixedUpdate()
    {
        MainThreadHandler.AddOther(() => 
        {
            _material = GetComponent<MeshRenderer>().materials[_matIndex];
            if (EnableMove)
            {
                _material.mainTextureOffset += OffsetSpeed * 0.02f;
            }
            else
            {
                _material.mainTextureOffset = OffsetField * 0.02f;
            }
            _material.mainTextureScale = ScaleField * 0.02f;
        });
    }
}

using UnityEngine;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]

public sealed class GlobalHatsObject : MonoBehaviour
{
    public static       GlobalHatsObject instance;
    public Mesh[]       meshes;
    public HatsList     hatlist;
    /// <summary>����� JSON ������������� �����, ������ ����������� ������ <see langword="��������������"/> ������</summary>
    public bool[]       enabledHatsArray;
    /// <summary>����� JSON ������������� �����, ������ ����������� ������ <see langword="���������"/> ������</summary>
    public int[]        purchasedHats;


    private void Start()
    { 
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        Mesh[] finded = new Mesh[hatlist.hats.Length];

        for (ushort i = 0; i < finded.Length; i++)
        {
            finded[i] = hatlist.hats[i].hatMesh;
        }

        meshes = finded;

        Object.DontDestroyOnLoad(this.gameObject);
    }
}

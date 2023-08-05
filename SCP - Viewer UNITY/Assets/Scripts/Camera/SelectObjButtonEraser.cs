// ���������������.
// ������ ��� ������, ������� ������� ������� ��������� ������ ��� ������.
// ����� ��� ��������.

using UnityEngine;

public class SelectObjButtonEraser : MonoBehaviour
{
    private UnityEngine.UI.Button btnSource; //��������������� ������

    void Start          ()  
    {
        btnSource = GetComponent<UnityEngine.UI.Button>();
        SPAWN_lists.instance.SelectedInstance = null;
    }
    void FixedUpdate    ()  
    {
        if (SPAWN_lists.instance.SelectedInstance is null) btnSource.interactable = false;
        else btnSource.interactable = true;
    }
}

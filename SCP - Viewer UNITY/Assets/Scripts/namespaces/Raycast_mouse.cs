using UnityEngine;

[System.Obsolete("�� ��������� � �������������")]
public class Raycast_mouse : MonoBehaviour
{
    public      static  Raycast_mouse   Rmouse;
    protected           Camera          _mainCamera;


    protected void Start()
    {
        _mainCamera = this.GetComponent<Camera>();
        if (_mainCamera == null)
            Debug.Log($"_mainCamera is NULL");
    }

    protected   void    Update() 
    {
        if (Input.touchCount > 0){
            
            Ray ray3D = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray3D, out RaycastHit raycastHit))
                Debug.DrawLine(transform.position, raycastHit.point, Color.red, 0.1f);                        
        }        
    }      //��������� ���, ���� ������� ������ ��� ����� 1. ������� �������� � �������� �� � SCPMethods.Personal.lastObjects
}

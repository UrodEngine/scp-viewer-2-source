using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("���� ������ ������ �� ������������ � �������")]
public class Item_slots : MonoBehaviour
{
    public  Image[]     _imga = new Image[10];
    public  Sprite[]    _sourca;
    private void Awake(){
        for (short i = 0; i < _imga.Length; i++){
            _imga[i] = transform.GetChild(i).GetComponent<Image>();
            _imga[i].transform.localScale = new Vector3(0, 0, 0);
        }
    }
}//���� ����� ���������� ������ � ������� Infos. � ���� ������� ����������� ��� ���������� � ��������.

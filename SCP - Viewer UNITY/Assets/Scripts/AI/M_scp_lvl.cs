// ���� ������ �������� �� �������, ������� ��� ���������
// ����� �� ��������� �������.
//
// ������� � ���� �������� �� ����� �������� ��������.

using UnityEngine;
[AddComponentMenu("SCP Tools/SCP map loader")]
public class M_scp_lvl : MonoBehaviour
{
    public string M_SCP_level_name;
    void Start(){
        GetComponent<LoadLevel>().LevelLoading(M_SCP_level_name);
    }
}

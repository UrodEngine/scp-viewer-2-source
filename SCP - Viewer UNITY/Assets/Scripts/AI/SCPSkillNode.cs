using UnityEngine;

[System.Serializable]
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public class SCPSkillNode
{
    public delegate void OnUseSkill();
    public event OnUseSkill isUsedSkill = delegate { };
    public SCPSkillNode(string name, ushort reload)
    {
        this.name   = name;
        this.reload = reload;
    }
    public  string  name        = "undefined";
    public  ushort  reload      = 0;
    public  bool    isReload    = false;

    public bool         UseSkill()
    {
        if (isReload is true) return true;  //���� ��� �������������� - ��������� ��������� �����
        Reload();                           //����������� async
        isReload = true;
        isUsedSkill();
        return isReload;                    // ������� isReload -> � ����� true
    }
    public async void   Reload()
    {
        try
        {
            await System.Threading.Tasks.Task.Delay(reload);
            isReload = false;
        }
        catch
        {
            return;
        }
    }
}

/// <summary>
/// �������� ������ �������-�����, ������� ����� ������������ � ������ ���������� ���������
/// </summary>
public interface ISCPSkillRequest
{
    public SCPSkillNode[] skills { get; set; }
    public void SkillsSet();
}

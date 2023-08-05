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
        if (isReload is true) return true;  //Если уже перезаряжается - перестать проводить метод
        Reload();                           //перезарядка async
        isReload = true;
        isUsedSkill();
        return isReload;                    // вернуть isReload -> в общем true
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
/// Содержит массив скиллов-нодов, которые можно использовать в ручном управлении сущностью
/// </summary>
public interface ISCPSkillRequest
{
    public SCPSkillNode[] skills { get; set; }
    public void SkillsSet();
}

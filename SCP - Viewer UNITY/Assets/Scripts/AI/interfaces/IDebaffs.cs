using UnityEngine;
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;

/// <summary>
/// ����������� �����, ���������� ������������� ������, ���� ����� ������������ ����������, ����� ����������������� � ������� ����������
/// </summary>
public static class InteractiveMethods  
{
    /// <summary>
    /// ����������� �����, ���������� �� ����� ��������, ������� ��������� <see cref="NavMeshAgent"/>
    /// </summary>
    /// <param name="fearPoint"> �����, �������� ��������</param>
    /// <param name="target"> ��������� �������� </param>
    public static void  FearByObject(in Vector3 fearPoint,in Transform target)
    {
        NavMeshAgent _navMeshAgent = target.GetComponent<NavMeshAgent>();
        if (_navMeshAgent.enabled is false || _navMeshAgent.isOnNavMesh is false)
        {
            return;
        }
        Ray fearDirection = new Ray(target.position, target.position - fearPoint);
        _navMeshAgent.SetDestination(target.position + fearDirection.direction.normalized * 15);
    }
}
public interface    ISoundSwitcher      
{
    public void FixedPlay();
}

public interface    IDebaffs            
{
    public void SetStan     (in int stanValue);
    [System.Obsolete("Old")]
    public void isBurn      ();  //������� �� value
}
/// <summary>
/// �������� ���� � ������� ����-���������� ����������� � ����� ��������
/// </summary>

public interface    IPassportData       
{
    public string   aliveName       { get; set;}
    public string   aliveSurname    { get; set; }
    public short    aliveAges       { get; set; }
}
/// <summary>
/// ������������ ��� ������ ����� <see cref="ManProperties"/>, <see cref="MurderProperties"/> � �.�.
/// </summary>

public interface    IAliveConfigs       
{
    public void SetDamage   (in int damageValue, in bool setBloodLust);
    public void KillForm    (in bool WithBlood);
}
/// <summary>
/// �������� ��������� <see cref="IAliveConfigs"/><br/>
/// ���������� ���� ������: <see cref="Man"/>, <see cref="MurderProperties"/> ,<see cref="AliveForm"/>.<br/>
/// ���� ���� - ������, ������������� �� <see cref="AliveForm"/>. +���������
/// </summary>

public interface    IAliveForm          
{
    /// <summary>�������� ���������, ���������� ������, ��������� ��������� ���� ��������. ���� �� ���������� <see cref="IAliveForm"/></summary>
    /// <returns><see cref="IAliveConfigs"/>.</returns>
    public IAliveConfigs GetField();

    /// <summary> ������ �������� � ������. ���� �� ���������� <see cref="IAliveForm"/> </summary>
    public float heigh { get; }
}
/// <summary>
/// �������� ��� <see cref="ObjectFormProperties"/><br/>
/// ���� ���� - ������ <see cref="ObjectFormProperties"/>. +���������
/// </summary>

public interface    IObjectParameters   
{
    public ObjectFormProperties GetProperties();
}
/// <summary>
/// ���������, ����������� ��������� ������������� �������� �� �����
/// </summary>
/// 
public interface IInteractiveInjector
{
    /// <summary>
    /// ����� ����� ������������� ����� ������ MonoBehaviour, ������� ����� ��������� IInteractiveInjector
    /// </summary>
    public void InteractiveRun();
}
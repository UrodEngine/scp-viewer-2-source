using UnityEngine;
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;

/// <summary>
/// Статический класс, содержащий интерактивные методы, чаще всего используемые существами, чтобы взаимодействовать с другими существами
/// </summary>
public static class InteractiveMethods  
{
    /// <summary>
    /// Статический метод, отгоняющий от точки существо, имеющее компонент <see cref="NavMeshAgent"/>
    /// </summary>
    /// <param name="fearPoint"> точка, пугающая существо</param>
    /// <param name="target"> компонент существа </param>
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
    public void isBurn      ();  //Поджечь на value
}
/// <summary>
/// Содержит поля с краткой мета-личностной информацией о живом существе
/// </summary>

public interface    IPassportData       
{
    public string   aliveName       { get; set;}
    public string   aliveSurname    { get; set; }
    public short    aliveAges       { get; set; }
}
/// <summary>
/// используется для поиска полей <see cref="ManProperties"/>, <see cref="MurderProperties"/> и т.д.
/// </summary>

public interface    IAliveConfigs       
{
    public void SetDamage   (in int damageValue, in bool setBloodLust);
    public void KillForm    (in bool WithBlood);
}
/// <summary>
/// Содержит интерфейс <see cref="IAliveConfigs"/><br/>
/// Возвращает типы данных: <see cref="Man"/>, <see cref="MurderProperties"/> ,<see cref="AliveForm"/>.<br/>
/// Ищет поля - классы, наследованные от <see cref="AliveForm"/>. +параметры
/// </summary>

public interface    IAliveForm          
{
    /// <summary>Получает интерфейс, содержащий методы, способные причинить вред существу. Поле от интерфейса <see cref="IAliveForm"/></summary>
    /// <returns><see cref="IAliveConfigs"/>.</returns>
    public IAliveConfigs GetField();

    /// <summary> высота существа в юнитах. Поле от интерфейса <see cref="IAliveForm"/> </summary>
    public float heigh { get; }
}
/// <summary>
/// Содержит тип <see cref="ObjectFormProperties"/><br/>
/// Ищет поля - классы <see cref="ObjectFormProperties"/>. +параметры
/// </summary>

public interface    IObjectParameters   
{
    public ObjectFormProperties GetProperties();
}
/// <summary>
/// Интерфейс, описывающий поведение интерактивных объектов на сцене
/// </summary>
/// 
public interface IInteractiveInjector
{
    /// <summary>
    /// Точка входа интерактивной части любого MonoBehaviour, который имеет интерфейс IInteractiveInjector
    /// </summary>
    public void InteractiveRun();
}
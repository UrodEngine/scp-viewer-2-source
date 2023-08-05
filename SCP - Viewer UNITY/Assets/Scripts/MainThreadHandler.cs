using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent, AddComponentMenu("Start Tools/Main Thread Handler")]
public sealed class MainThreadHandler : MonoBehaviour
{
    public static event System.Action onAddedAction = () => { };
    public static event System.Action onAddedOtherAction = () => { };

    public static MainThreadHandler     instance;
    public static byte                  actionsPerTime = 10;

    public static ushort invokedActions         { get; private set; }
    public static ushort invokedOtherActions    { get; private set; }

    public static System.Action[] actionMembers { get => actions.ToArray(); }
    public static System.Action[] otherActionMembers { get => otherActions.ToArray(); }

    [Header("¬ызывает добавленные Action через основной поток")]
    private static List<System.Action> actions          = new List<System.Action>(128);
    private static List<System.Action> otherActions     = new List<System.Action>(128);


    private void Start  ()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update ()
    {
        InvokeActions();
        InvokeOther();
    }

    private static void InvokeActions   ()
    {
        #region steppedActions
        if (actions is null || actions.Count == 0)
        {
            return;
        }
        ushort actionCount = 0;

        if (actions.Count < 256)
        {
            actionCount = actionsPerTime;
        }
        else
        {
            actionCount = (ushort)Mathf.Clamp(actions.Count / 2f, 1, actions.Count);
        }

        if (actionsPerTime > actions.Count)
        {
            actionCount = (ushort)actions.Count;
        }

        invokedActions = 0;
        for (short index = 0; index < actionCount; index++)
        {
            try
            {
                actions[index]();
                invokedActions++;
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        actions.RemoveRange(0, actionCount);
        #endregion
    }
    private static void InvokeOther     ()
    {
        #region steppedActions
        if (otherActions is null || otherActions.Count == 0)
        {
            return;
        }
        int realMembers = 0;

        if (otherActions.Count < 256)
        {
            realMembers = actionsPerTime;
        }
        else
        {
            realMembers = (int)Mathf.Clamp(otherActions.Count / 1.2f, 1, otherActions.Count);
        }

        if (actionsPerTime > otherActions.Count)
        {
            realMembers = otherActions.Count;
        }

        invokedActions = 0;
        for (int i = 0; i < realMembers; i++)
        {
            try
            {
                otherActions[i]();
                invokedActions++;
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        otherActions.RemoveRange(0, realMembers);
        #endregion
    }


    #region actions adding
    /// <summary> ƒобавл€ет в список событий новое исполн€емое событие </summary>
    public static void AddActions(in System.Action _action) 
    {
        if (instance == null)
        {
            return;
        }

        actions.Add(_action);
        onAddedAction();
    }
    /// <summary>
    /// ƒобавл€ет в список событий новое исполн€емое событие. „ем ниже цифра приоритета, тем первее будет исполн€емое событие. ћожно указать, при каком уровне заполненности событие не будет добавлено
    /// </summary>
    /// <param name="_action"> - —обытие.</param>
    /// <param name="priority"> - ѕриоритет. 0 = событие будет обработано самым первым.</param>
    /// <param name="oversizeLimit"> - ѕереполнение. если oversizeLimit больше кол-ва событий, вызов метода прекратитс€ и список не будет пополнен новым событием </param>
    public static void AddActions(in System.Action _action, in int? priority = null, in bool _allowDublicates = false) 
    {
        if (instance == null)
        {
            return;
        }
        if (!_allowDublicates)
        {
            for (int i = 0; i < otherActions.Count; i++)
            {
                if (otherActions[i].Target.GetHashCode() == _action.Target.GetHashCode())
                {
                    return;
                }
            }
        }
        if (priority != null && priority>0)
        {
            if (priority < actions.Count)
            {
                actions.Insert((int)priority, _action);
            }
            else
            {
                actions.Insert(actions.Count-1, _action);
            }
        }
        else
        {
            actions.Add(_action);
        }
        onAddedAction();
    }

    /// <summary> добавл€ет в список второстепенных событий новое второстепенное событие </summary>
    public static void AddOther(in System.Action _action, in bool _allowDublicates = false)
    {
        if (instance == null)
        {
            return;
        }
        if (!_allowDublicates)
        {
            for (int i = 0; i < otherActions.Count; i++)
            {
                if (otherActions[i].Target.GetHashCode() == _action.Target.GetHashCode())
                {
                    return;
                }
            }
        }
        otherActions.Add(_action);
        onAddedOtherAction();
    }
    #endregion
}

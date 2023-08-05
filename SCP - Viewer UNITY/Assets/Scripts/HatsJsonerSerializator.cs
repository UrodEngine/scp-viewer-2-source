using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class HatsJsonerSerializator : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    /// <summary>JSON сериализуемый класс, описывающий <see langword="АКТИВИРОВАННЫЕ"/> шапки</summary>
    [System.Serializable]
    public class EnabledHats { public bool[] enabledHatsArray; }
    /// <summary>JSON сериализуемый класс, описывающий <see langword="АКТИВИРОВАННЫЕ"/> шапки</summary>
    public EnabledHats enabledHats;

    /// <summary>JSON сериализуемый класс, описывающий <see langword="КУПЛЕННЫЕ"/> шапки</summary>
    [System.Serializable]
    public class HatsJsoner { public List<int> SavedSlots = new List<int>(); }
    /// <summary>JSON сериализуемый класс, описывающий <see langword="КУПЛЕННЫЕ"/> шапки</summary>
    public HatsJsoner purchasedHats = new HatsJsoner();
    /*=========================================================================================================================================================*/
    #endregion

    private async void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/HatsActivated.dll") is false || File.Exists(Application.persistentDataPath + "/Hats.dll") is false)
        {
            File.WriteAllText(Application.persistentDataPath + "/HatsActivated.dll", JsonUtility.ToJson(purchasedHats));
            File.WriteAllText(Application.persistentDataPath + "/Hats.dll", JsonUtility.ToJson(enabledHats));
        }
        enabledHats     = JsonUtility.FromJson<EnabledHats>(File.ReadAllText(Application.persistentDataPath + "/HatsActivated.dll"));
        purchasedHats   = JsonUtility.FromJson<HatsJsoner> (File.ReadAllText(Application.persistentDataPath + "/Hats.dll"));

        GlobalHats.takedhat = purchasedHats.SavedSlots.ToArray();
        
        await System.Threading.Tasks.Task.Delay(20);

        AttachDataToInstances();
    }

    private void AttachDataToInstances()
    {
        try
        {
        GlobalHatsObject.instance.purchasedHats     = purchasedHats.SavedSlots.ToArray();
        GlobalHatsObject.instance.enabledHatsArray  = enabledHats.enabledHatsArray;
        }
        catch (System.NullReferenceException)
        {
            return;
        }
    }
}



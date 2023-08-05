using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrefabManager",menuName = "Create Prefab Manager")]
public sealed class PrefabManager : ScriptableObject
{
    private static List<PrefabManager> allPrefabsManagers = new List<PrefabManager>(32);

    public  GameObject[]    prefabs;
    /// <summary>�������������� ���� ���������. ����� ��� ����������� ������ ���������� ��������� ��������</summary>
    public  string          managerKey;


    /// <summary>
    /// ���������� ������, ��������� � ������� ��������� ��������, ����� ����� �� �����
    /// </summary>
    /// <param name="_name">��� �������</param>
    /// <returns>GameObject</returns>
    public GameObject GetPrefab(in string _name)
    {
        foreach (GameObject prefab in prefabs)
        {
            if (prefab.name == _name)
            {
                return prefab;
            }
        }
        return null;
    }
    /// <summary>
    /// ���������� ��� �������, ��������� � ������� ��������� ��������
    /// </summary>
    /// <param name="_name">��� �������</param>
    /// <returns>GameObject[]</returns>
    public GameObject[] GetPrefabs()
    {

        return prefabs;
    }

    /// <summary>
    /// ���������� �������� �������� ����� ����� �� ����� ������
    /// </summary>
    /// <param name="_name">��� ������</param>
    /// <returns>PrefabManager</returns>
    public static PrefabManager GetManager(in string _name)
    {
        foreach (PrefabManager manager in allPrefabsManagers)
        {
            if (manager.name == _name)
            {
                return manager;
            }
        }
        return null;
    }

    /// <summary>
    /// ���������� �������� �������� ����� ����� �� ����� ��������� ��������
    /// </summary>
    /// <param name="_name">���� ��������� ��������</param>
    /// <returns>PrefabManager</returns>
    public static PrefabManager GetManagerByKey(in string _key)
    {
        foreach (PrefabManager manager in allPrefabsManagers)
        {
            if (manager.managerKey is null || manager.managerKey.Length <= 0)
            {
                continue;
            }
            if (manager.managerKey == _key)
            {
                return manager;
            }
        }
        return null;
    }
    PrefabManager() => allPrefabsManagers.Add(this);
}

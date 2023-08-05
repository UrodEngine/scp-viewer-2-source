using UnityEngine;

[System.Obsolete("Õ≈ Õ”∆ƒ¿≈“—ﬂ ¬ »—œŒÀ‹«Œ¬¿Õ»»")]
public class SearchAlivesCamera : MonoBehaviour
{
    public static int nextIterationDclass;
    public static int nextIterationDangers;

    public static GameObject[] Dclasses;
    public static GameObject[] DangerMonsters;
    void LateUpdate()
    {
        Collider[] all = Physics.OverlapSphere(transform.position, 128);

        System.Collections.Generic.List<GameObject> onlyDclassTemp  = new System.Collections.Generic.List<GameObject>();
        System.Collections.Generic.List<GameObject> onlyDangersTemp = new System.Collections.Generic.List<GameObject>();

        foreach (var active in all)
        {
            if (active.GetComponent<Man>())          onlyDclassTemp  .Add(active.transform.gameObject);
            if (active.GetComponent<DangerChecker>())   onlyDangersTemp .Add(active.transform.gameObject);
            continue;
        }
        Dclasses        = onlyDclassTemp    .ToArray();
        DangerMonsters  = onlyDangersTemp   .ToArray();

        nextIterationDclass  ++;
        nextIterationDangers ++;

        if (nextIterationDclass     >= Dclasses.Length) nextIterationDclass          = 0;
        if (nextIterationDangers    >= DangerMonsters.Length) nextIterationDangers   = 0;
    }
}

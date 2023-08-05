using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public class ConsistentObjContainer
{
    public List<GameObject> list = new List<GameObject>();
    public int ID = 0;

    public void AddToList   (in GameObject gameobject)  
    {
        if(gameobject != null) list.Add(gameobject);
    }
    public void Circulate   (in GameObject byObject)    
    {
        if (list.ToArray().Length <= 0 || list is null) return;

        ///ÅÑËÈ ÍÓËÅÂÎÉ ÎÁÚÅÊÒ ËÈÑÒÀ ÐÀÂÅÍ ÎÁÚÅÊÒÓ, ÂÛÇÛÂÀÞÙÅÌÓ ÖÈÊË - ID++. 
        /// Ýòî ñäåëàíî òàê. ÷òîáû öèêë âûçâàòü ìîã òîëüêî îäèí îáúåêò èç êó÷è
        if (list[0] == byObject){         
            ID++;
        }

        if (ID >= list.ToArray().Length )
            ID = 0;

        for (int i = 0; i < list.ToArray().Length; i++){
            if (list[i] == null){
                list.Remove(list[i]);
                break;
            }
        } 
    }
    public bool isMyID      (in GameObject target)      
    {
        try
        {
            return (target != null && target == list[ID]) ? true : false;
        }
        catch
        {
            return false;
        }
    }
}
*/

using System;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    [Header("Max static = 64, Non static max = 128")]
    #region Alterable values
    //===============================================================================================
    public      int updateTickrate = 1024;
    protected   int _timer;
    //===============================================================================================
    #endregion

    protected   void    Start                       (){
        _timer = updateTickrate;
        GameObject[] StaticObjectArray      = GameObject.FindGameObjectsWithTag("RaycastSpawner");
        GameObject[] NonStaticObjectsArray  = GameObject.FindGameObjectsWithTag("Interactable");
        GetStaticObjectsArray(StaticObjectArray);
        GetNonStaticObjectsArray(NonStaticObjectsArray);
    }
    private     void    Update                      (){
        _timer = _timer > 0 ? _timer - 1 : UpdateInteractableList(); //Таймер для UpdateInteractableObjects()

        if (Input.GetKeyDown(KeyCode.D)) new InterestingObjectsSearcher().GetOnlyStaticNearestPoints(transform.position,7);
        
    }                              //Сердце экземпляра
    protected   void    GetStaticObjectsArray       (in GameObject[] gameobjects){
        for (int i = 0; i < gameobjects.Length; i++){
            Array.Resize(ref InterestingObjects.StaticPoints, gameobjects.Length);
            InterestingObjects.StaticPoints[i] = gameobjects[i].gameObject.transform.position;
        }
    }   //получает список СТАТИЧНЫХ объектов. глобальному классу возвращает массив точек этих объектов. GameObject[].transform.position -> Vector3[]
    protected   void    GetNonStaticObjectsArray    (in GameObject[] gameobjects){
        for (int i = 0; i < gameobjects.Length; i++){
            Array.Resize(ref InterestingObjects.NonStaticPoints, gameobjects.Length);
            InterestingObjects.NonStaticPoints[i] = gameobjects[i].gameObject.transform.position;          
        }
    }   //получает список ДИНАМИЧНЫХ объектов. глобальному классу возвращает массив точек этих объектов. GameObject[].transform.position -> Vector3[]
    protected   int     UpdateInteractableList      (){
        GameObject[]    InteractableArray = GameObject.FindGameObjectsWithTag("Interactable");
        GetNonStaticObjectsArray(InteractableArray);

        
        return updateTickrate;
    }                              //Каждые (int)IntrstngObjectCheckTickrate обновляет список динамичных объектов
}

public static class InterestingObjects  //Глобальный класс со всеми точками объектов
{
    public  static  Vector3[]   NonStaticPoints = new Vector3[128];
    public  static  Vector3[]   StaticPoints    = new Vector3[64];

}
public class        InterestingObjectsSearcher
{
        float   distance        = Mathf.Infinity;
        float   distanceMinus   = Mathf.NegativeInfinity;
        Vector3 closest         = new Vector3();
    public Vector3 GetNearestPoint              (Vector3 selfPosition, in float nearBreaker)
    {
        System.Collections.Generic.List<Vector3> allPoints = new System.Collections.Generic.List<Vector3>();
        allPoints.AddRange(InterestingObjects.NonStaticPoints);
        allPoints.AddRange(InterestingObjects.StaticPoints);

        foreach (Vector3 active in allPoints.ToArray()){
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance){
                if (Vector3.Distance(active, selfPosition) < nearBreaker) { Debug.Log($"{active} very near"); break; };
                closest = active;
                distance = currentDistance;
            }       
            continue;
        }
        Debug.Log(closest);
        return closest;
    }
    public Vector3 GetOnlyStaticNearestPoints   (Vector3 selfPosition, in float nearBreaker)
    {
        foreach (Vector3 active in InterestingObjects.StaticPoints){
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance){
                if (Vector3.Distance(active, selfPosition) < nearBreaker) { Debug.Log($"{active} very near");break; };
                closest = active;
                distance = currentDistance;
            }
            continue;
        }
        return closest;
    }
    public Vector3 GetOnlyNonStaticNearestPoints(Vector3 selfPosition, in float nearBreaker)
    {
        foreach (Vector3 active in InterestingObjects.NonStaticPoints){
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance){
                if (Vector3.Distance(active, selfPosition) < nearBreaker) { Debug.Log($"{active} very near"); break; };
                closest = active;
                distance = currentDistance;
            }         
            continue;
        }
        return closest;
    }

    public Vector3 GetFurtherPoint              (Vector3 selfPosition)
    {
        System.Collections.Generic.List<Vector3> allPoints = new System.Collections.Generic.List<Vector3>();
        allPoints.AddRange(InterestingObjects.NonStaticPoints);
        allPoints.AddRange(InterestingObjects.StaticPoints);
        foreach (Vector3 active in allPoints.ToArray())
        {
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance > distanceMinus)
            {
                closest = active;
                distanceMinus = currentDistance;
            }
            continue;
        }
        return closest;
    }
    public Vector3 GettOnlyStaticFurtherPoint   (Vector3 selfPosition)
    {
        foreach (Vector3 active in InterestingObjects.StaticPoints)
        {
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance > distanceMinus)
            {
                closest = active;
                distanceMinus = currentDistance;
            }
            continue;
        }
        return closest;
    }
    public Vector3 GetOnlyNonStaticFurtherPoint (Vector3 selfPosition)
    {
        foreach (Vector3 active in InterestingObjects.NonStaticPoints)
        {
            Vector3 difference = active - selfPosition;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance > distanceMinus)
            {
                closest = active;
                distanceMinus = currentDistance;
            }
            continue;
        }
        return closest;
    }


}


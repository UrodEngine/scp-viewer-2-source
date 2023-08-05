//-----------------------------------------------------------------------------------------------------------
// Этот класс позволяет объекту искать цель около себя.
//-----------------------------------------------------------------------------------------------------------
// методы с Tag             ищут объекты с нужным тегом
// методы с NoRay           ищут объекты сквозь стены
// методы с GameObject[]    возвращают массив найденных объектов с подходящими параметрами Tag или Component.
//-----------------------------------------------------------------------------------------------------------
// findedObject - поле в котором содержится найденный через GameObject методы объект
//-----------------------------------------------------------------------------------------------------------
#region UPDATES
//-----------------------------------------------------------------------------------------------------------
// 31.05.2022 - добавлен NearObiUtilitiesSimplified.
// 31.05.2022 - Более простая форма поиска объектов
// 31.05.2022 - работает через NonAlloc функции
// 31.05.2022 - требует четкого обозначения буфера хранения объектов
// 20.05.2023 - удалены неиспользуемые методы
//-----------------------------------------------------------------------------------------------------------
#endregion

using UnityEngine;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ DEFAULT ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public struct NearObjUtilities              
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    private GameObject findedObject;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion    
    public GameObject   NearestTarget               (in Transform transform, in float add_Y, in float Radius, in string componenter, in string componenterIgnore){
        Vector3     pos             = transform.position + new Vector3(0, add_Y, 0);
        float       distance        = Mathf.Infinity;
        Collider[]  colliderItems   = Physics.OverlapSphere(transform.transform.position, Radius);

        for (int i = 0; i < colliderItems.Length; i++){
            if (Physics.Raycast(pos , colliderItems[i].transform.position + new Vector3(0, 0.5f, 0) - pos, out RaycastHit TargetChecker))
            {
                if (TargetChecker.transform.gameObject == colliderItems[i].gameObject && TargetChecker.transform.gameObject != transform.gameObject)
                {
                    float difference = (pos - colliderItems[i].transform.position).sqrMagnitude;

                    if (difference < distance && colliderItems[i].GetComponent(componenter) && !colliderItems[i].GetComponent(componenterIgnore))
                    {
                        distance = difference;
                        findedObject = colliderItems[i].gameObject;
                        Debug.DrawLine(pos, colliderItems[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                    }
                }//Check wall between you and target
            }
            continue;
        }
        return findedObject;
    }
    public GameObject   NearestTarget               (in Transform transform, in float add_Y, in float Radius, in string componenter){
        Vector3     pos             = transform.position + new Vector3(0, add_Y, 0);
        float       distance        = Mathf.Infinity;
        Collider[]  colliderItems   = Physics.OverlapSphere(pos, Radius);

        for (int i = 0; i < colliderItems.Length; i++){
            if (Physics.Raycast(pos, colliderItems[i].transform.position - pos, out RaycastHit TargetChecker)){ 
                if (TargetChecker.transform.gameObject == colliderItems[i].gameObject && TargetChecker.transform.gameObject != transform.gameObject){
                    float difference = (pos - colliderItems[i].transform.position).sqrMagnitude;

                    if (difference < distance && colliderItems[i].GetComponent(componenter)){
                        distance        = difference;
                        findedObject    = colliderItems[i].gameObject;
                        Debug.DrawLine(transform.position, colliderItems[i].gameObject.transform.position, Color.red);
                    }
                }
            }//Check wall between you and target
            continue;
        }
        return findedObject;
    } 
    public GameObject   NearestComponent<T>         (in Transform transform, in float add_Y, in float Radius)
    {
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;
        Collider[] colliderItems = Physics.OverlapSphere(transform.transform.position, Radius);

        for (int i = 0; i < colliderItems.Length; i++)
        {
            if (Physics.Raycast(pos, colliderItems[i].transform.position + new Vector3(0, 0.5f, 0) - pos, out RaycastHit TargetChecker))
            {
                if (TargetChecker.transform.gameObject == colliderItems[i].gameObject && TargetChecker.transform.gameObject != transform.gameObject)
                {
                    float difference = (pos - colliderItems[i].transform.position).sqrMagnitude;

                    if (difference < distance && colliderItems[i].TryGetComponent<T>(out T  interfaceComponent) )
                    {
                        distance = difference;
                        findedObject = colliderItems[i].gameObject;
                        Debug.DrawLine(pos, colliderItems[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                    }
                }//Check wall between you and target
            }
            continue;
        }
        return findedObject;
    }
    public GameObject[] RaycastedArrayByComponent   (in Transform transform, in float Radius, in string componenter, in float add_Y)
    {
        Vector3 pos = transform.transform.position;
        Collider[] colliderItems = Physics.OverlapSphere(transform.transform.position, Radius);
        System.Collections.Generic.List<GameObject> FutureObjectArray = new System.Collections.Generic.List<GameObject>();
        //System.Runtime.InteropServices.Marshal.SizeOf(FutureObjectArray);
        for (int i = 0; i < colliderItems.Length; i++){
            if (Physics.Raycast(pos + new Vector3(0, add_Y, 0), colliderItems[i].transform.position - transform.position, out RaycastHit RayCheckedItem)){
                if (RayCheckedItem.transform.gameObject == colliderItems[i].gameObject && colliderItems[i].gameObject.GetComponent(componenter)){
                    FutureObjectArray.Add(RayCheckedItem.transform.gameObject);
                }
            }
        }
        return FutureObjectArray.ToArray();
    }
}
public struct NearObjUtilitiesV2            
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    private GameObject findedObject;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion

    public GameObject NearestTargetNoRay    (in Transform transform, in GameObject[] colliders, in float add_Y, in string componenter)
    {
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;
        System.Collections.Generic.List<GameObject> onlySelected = new System.Collections.Generic.List<GameObject>();

        foreach (var item in colliders)
        {
            if (item.GetComponent(componenter)) onlySelected.Add(item);
            continue;
        }
        for (int i = 0; i < onlySelected.ToArray().Length; i++)
        {
            float difference = (pos - onlySelected[i].transform.position).sqrMagnitude;

            if (difference < distance && onlySelected[i].GetComponent(componenter) && onlySelected[i] != transform.gameObject)
            {
                distance = difference;
                findedObject = onlySelected[i].gameObject;
                Debug.DrawLine(transform.position, onlySelected[i].gameObject.transform.position, Color.yellow);
            }
            continue;
        }
        return findedObject;
    }
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ STATIC ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public struct NearObjUtilitiesStatic        
{
    public static void NearestTarget(in Transform transform, in Collider[] colliders, in float add_Y, in string compName, out GameObject outObject)
    {
        outObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.GetComponent(compName))
            {
                Physics.Raycast(pos, colliders[i].transform.position + new Vector3(0, 0.2f, 0) - pos, out RaycastHit raycastHit);
                if (raycastHit.transform.gameObject == colliders[i].gameObject && raycastHit.transform.gameObject != transform.gameObject)
                {
                    float difference = (pos - colliders[i].transform.position).sqrMagnitude;
                    if (difference < distance)
                    {
                        distance = difference;
                        outObject = colliders[i].gameObject;
                        Debug.DrawLine(pos, colliders[i].gameObject.transform.position + new Vector3(0, 0.2f, 0), Color.yellow);
                    }
                }//Check wall between you and target
            }
            continue;
        }
    }
    public static void NearestTargetTagNoRay(in Transform transform, in Collider[] colliders, in float add_Y, in string Tag, out GameObject outObject)
    {
        outObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            try
            {
                float difference = (pos - colliders[i].transform.position).sqrMagnitude;
                if (difference < distance && colliders[i].tag == Tag && colliders[i] != transform.gameObject)
                {
                    distance = difference;
                    outObject = colliders[i].gameObject;
                    Debug.DrawLine(transform.position, colliders[i].gameObject.transform.position, Color.yellow);
                }
            }
            catch
            {
                i++;
            }
            continue;
        }
    }
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ SIMPLIFIED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public struct NearObiUtilitiesSimple        
{
    ///THIS IS NON ALLOCATED <s Nearest object search utility>. Optimized version.
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    private GameObject findedObject;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion

    public GameObject NearestTargetComponent    (in Transform transform, in float add_Y, in Collider[] importedColliders, in string componenter)
    {
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                float difference = (pos - importedColliders[i].transform.position).sqrMagnitude;

                if (difference < distance && importedColliders[i].GetComponent(componenter))
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;
            }
        }
        return findedObject;
    }
    public Collider[] SimpleOverlapSphere       (in Vector3 position, in float radius, in int BUFFER_SIZE)
    {
        Collider[]  colliders           = new Collider[BUFFER_SIZE];
        short countOfOverlaps    = (short)Physics.OverlapSphereNonAlloc(position, radius, colliders);

        if (countOfOverlaps <= 0) return null;

        Collider[]  clearedColliders    = new Collider[countOfOverlaps];

        for (short i = 0; i < clearedColliders.Length; i++)
        {
            clearedColliders[i] = colliders[i];
            //Debug.DrawLine(position, clearedColliders[i].transform.position, Color.yellow, 0.1f);
            continue;
        }

        return clearedColliders;
    }
    public Collider[] SimpleRaycastAll          (in Vector3 position, in Collider[] colliders, in float radius, in float add_Y)
    {
        if (colliders is null || colliders.Length <= 0) return null;
        Vector3 pos                 = position + new Vector3(0, add_Y, 0);
        Collider[] clearedCollider  = new Collider[colliders.Length];

        for (short i = 0; i < colliders.Length; i++)
        {
            Ray ray = new Ray(pos, colliders[i].transform.position + new Vector3(0, 0.2f, 0) - pos); //directionToTarget
            Physics.Raycast(ray, out RaycastHit output,Mathf.Infinity,-1,QueryTriggerInteraction.Ignore);
            if (output.collider != null)
            {
                clearedCollider[i] = output.collider;
                //Debug.DrawLine(pos, colliders[i].transform.position + new Vector3(0, 0.2f, 0), Color.magenta, 0.04f);
            }
                
            continue;
        }

        return clearedCollider;
    }
}
public struct NearObiUtilitiesSimpleStatic  
{
    public static void NearestDclass            (in Transform transform, in float add_Y, in Collider[] importedColliders, in Man.ManTypeEnum DclassType, out GameObject findedObject)
    {
        findedObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject && importedColliders[i].GetComponent(nameof(Man)) && importedColliders[i].GetComponent<Man>().ManType == DclassType)
            {

                float difference = (pos - importedColliders[i].transform.position).sqrMagnitude;

                if (difference < distance)
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;

            }
        }
    }
    public static void NearestTargetGeneric<T>  (in Transform transform, in float add_Y, in Collider[] importedColliders, out GameObject findedObject)
    {
        findedObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                float difference = (pos - importedColliders[i].transform.position).sqrMagnitude;

                if (difference < distance && importedColliders[i].TryGetComponent<T>(out T generically))
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;
            }
        }
    }
    public static void TargetsGeneric<T>        (in Transform transform, in float add_Y, in Collider[] importedColliders, out GameObject[] findedObjects)
    {
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        findedObjects = new GameObject[importedColliders.Length];
        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                if (importedColliders[i].TryGetComponent<T>(out T generically))
                {
                    findedObjects[i] = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;
            }
        }
    }
    public static void NearestTargetComponent   (in Transform transform, in float add_Y, in Collider[] importedColliders, in string componenter, out GameObject findedObject)
    {
        findedObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                float difference = (pos - importedColliders[i].transform.position).sqrMagnitude;

                if (difference < distance && importedColliders[i].GetComponent(componenter))
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;
            }
        }
    }
    public static void NearestTargetComponent   (in Transform transform, in float add_Y, in Collider[] importedColliders, in string componenter, in string IgnoreComponenter, out GameObject findedObject)
    {
        findedObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                float difference = (pos - importedColliders[i].transform.position).sqrMagnitude;

                if (difference < distance && importedColliders[i].GetComponent(componenter) && !importedColliders[i].GetComponent(IgnoreComponenter))
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;
            }
        }
    }
    public static void NearestTargetTag         (in Transform transform, in float add_Y, in Collider[] importedColliders, in string tag, out GameObject findedObject)
    {
        findedObject = null;
        Vector3 pos = transform.position + new Vector3(0, add_Y, 0);
        float distance = Mathf.Infinity;

        for (short i = 0; i < importedColliders.Length; i++)
        {
            if (importedColliders[i] != null && importedColliders[i] != transform.gameObject)
            {
                float difference = (pos - importedColliders[i].gameObject.transform.position).sqrMagnitude;

                if (difference < distance && importedColliders[i] != null && importedColliders[i].gameObject.CompareTag(tag))
                {
                    distance = difference;
                    findedObject = importedColliders[i].gameObject;
                    Debug.DrawLine(pos, importedColliders[i].gameObject.transform.position + new Vector3(0, 0.5f, 0), Color.yellow);
                }
                continue;

            }
        }
    }

    public static void SimpleRaycastAll            (in Vector3 position, in Collider[] colliders, in float radius, in float add_Y, out Collider[] collidersOutput)
    {
        collidersOutput = new Collider[colliders.Length];
        if (colliders is null || colliders.Length <= 0) return;
        Vector3 pos = position + new Vector3(0, add_Y, 0);
        Collider[] clearedCollider = new Collider[colliders.Length];

        for (short i = 0; i < colliders.Length; i++)
        {
            Ray ray = new Ray(pos, colliders[i].transform.position + new Vector3(0, 0.2f, 0) - pos); //directionToTarget
            Physics.Raycast(ray, out RaycastHit output, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
            if (output.collider != null)
            {
                clearedCollider[i] = output.collider;
                //Debug.DrawLine(pos, colliders[i].transform.position + new Vector3(0, 0.2f, 0), Color.magenta, 0.04f);
            }
            continue;
        }
        collidersOutput = clearedCollider;
    }
}

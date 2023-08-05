using UnityEngine;

[AddComponentMenu("SCP Tools/Danger checker")]
public sealed class DangerChecker : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    //public static ConsistentObjContainer dangersContainer = new ConsistentObjContainer();
    public short  discoveredTimer; //Таймер обнаруженности
    /*=========================================================================================================================================================*/
    #endregion
    private void Start(){
        //dangersContainer.AddToList(this.gameObject);
    }

    private void FixedUpdate             (){
        discoveredTimer     = discoveredTimer   > (short)0 ? (short)(discoveredTimer   - 5) : discoveredTimer;
        MainThreadHandler.AddActions(() => 
        {
            SetDclassAnxioty();
        });
    }
    private void SetDclassAnxioty   (){
        Collider[] DclassNearest = new NearObiUtilitiesSimple().SimpleOverlapSphere(transform.position, 90,512);
        for (ushort i = 0; i < DclassNearest.Length; i++){
            if (DclassNearest[i] != null && DclassNearest[i].GetComponent(nameof(Man))){
                DclassNearest[i].GetComponent<Man>().enemyIsNear = true;
            }
        }
    }
}
using UnityEngine;
public sealed class DestroyTimer : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public float        TimerToDie;
    public  GameObject  SpawnAfterDie;
    /*=========================================================================================================================================================*/
    #endregion
    private void FixedUpdate(){
        TimerToDie = TimerToDie - 0.05f;
        if (TimerToDie <= 0) KillThis();
    }
    private void KillThis(){
        if (SpawnAfterDie != null){
            GameObject bro = Instantiate(SpawnAfterDie, transform.position, Quaternion.identity);
        }           
        Destroy(gameObject);
    }
}

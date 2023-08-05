using UnityEngine;

public class Shake : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    private     Vector3     StartPos;
    public      float       SubstractorValue=0.01f;
    private     int         randomForShake;
    private     int         timer;
    /*=========================================================================================================================================================*/
    #endregion
    void    Start       (){
        StartPos = transform.position;
    }
    void    Update      (){

        timer = timer < 0 ? timer-- : timerReset();

        if (randomForShake < 950) return;
        transform.position = StartPos + new Vector3(Random.Range(0, 10) * SubstractorValue, Random.Range(0, 10) * SubstractorValue, Random.Range(0, 10) * SubstractorValue);       
    }
    int     timerReset  (){
        randomForShake = Random.Range(0, 1000);
        return 100;
    }
}

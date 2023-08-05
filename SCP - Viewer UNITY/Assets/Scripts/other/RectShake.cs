using UnityEngine;
using UnityEngine.UI;
public class RectShake : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    private     RectTransform   rectT;
    private     int             randomForShake;
    private     int             timer;

    public      Vector2         StartPos;
    public      float           SubstractorValue = 0.01f;
    /*=========================================================================================================================================================*/
    #endregion

    private         void    Start       (){
        rectT       = GetComponent<RectTransform>();
        StartPos    = rectT.anchoredPosition;
    }
    private         void    LateUpdate  ()
    {
        AsyncUpdate();
    }
    private async   void    AsyncUpdate ()
    {
        timer = timer < 0 ? timer-- : timerReset();

        if (randomForShake < 950) return;
        rectT.anchoredPosition = StartPos + new Vector2(Random.Range(0, 10) * SubstractorValue, Random.Range(0, 10) * SubstractorValue);

        await System.Threading.Tasks.Task.Yield();
    }
    private         int     timerReset  (){
        randomForShake = Random.Range(0, 1000);
        return 100;
    }
}

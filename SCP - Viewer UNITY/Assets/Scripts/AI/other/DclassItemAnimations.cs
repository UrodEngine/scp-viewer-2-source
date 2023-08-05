using UnityEngine;

public sealed class DclassItemAnimations : MonoBehaviour
{
    /*======================================================*/
    [SerializeField] private GameObject[]   propsInHand;
    [SerializeField] private GameObject     GasetaInHand;
    /*======================================================*/

    void TakeGaseta (int truefalser)
    {
        if (GasetaInHand != null)
        {
            GasetaInHand.SetActive(truefalser == 1 ? true : false);
        }
    }
    void TakeEat    (int truefalser)
    {
        int randFood = Random.Range(0, propsInHand.Length);
        if (propsInHand[randFood] != null)
        {
            propsInHand[randFood].SetActive(truefalser == 1 ? true : false);
        }
    }
}

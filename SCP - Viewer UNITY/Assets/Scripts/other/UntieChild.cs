using UnityEngine;

public sealed class UntieChild : MonoBehaviour
{
    [Header("�������� �� �������� ������")]
    public  GameObject  UntieChildObject;
    public  float       TimerToUntie;

    private void Awake(){
        Invoke("UntieMeyhod", TimerToUntie);
    }    
    private void UntieMeyhod(){
        if (UntieChildObject != null){
            UntieChildObject.transform.parent = null;
            UntieChildObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

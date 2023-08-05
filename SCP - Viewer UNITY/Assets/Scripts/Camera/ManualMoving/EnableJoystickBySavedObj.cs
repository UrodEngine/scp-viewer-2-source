using UnityEngine;
using UnityEngine.AI;

public class EnableJoystickBySavedObj : MonoBehaviour
{
    [SerializeField] private GameObject joystick;

    void LateUpdate()
    {
        joystick.SetActive((CamMove_v2.instance.SavedGameObject != null && CamMove_v2.instance.SavedGameObject.GetComponent<NavMeshAgent>()) ? true : false);
    }
}

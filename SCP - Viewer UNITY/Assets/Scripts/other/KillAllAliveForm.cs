using UnityEngine;

[AddComponentMenu("SCP Tools/Destroy AliveForm by trigger")]
public class KillAllAliveForm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAliveForm>(out IAliveForm aliveForm) || other.TryGetComponent<IAliveConfigs>(out IAliveConfigs aliveConfigs))
        {
            Destroy(other.gameObject);
        }
    }
}

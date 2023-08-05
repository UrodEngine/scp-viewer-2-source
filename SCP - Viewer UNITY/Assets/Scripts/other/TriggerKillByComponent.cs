using UnityEngine;

[AddComponentMenu("SCP Tools/Kill AliveForm by trigger")]
public class TriggerKillByComponent : MonoBehaviour
{
    public bool WithBlood;
    private void OnTriggerEnter (Collider other)
    {
        if (other.TryGetComponent<IAliveForm>(out IAliveForm component))
        {
            component?.GetField().KillForm(WithBlood);
        }
    }
}

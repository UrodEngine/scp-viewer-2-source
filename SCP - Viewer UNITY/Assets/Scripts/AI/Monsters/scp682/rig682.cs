using UnityEngine.Animations.Rigging;
using UnityEngine;

public class rig682 : MonoBehaviour
{
    public Rig rig;
    [SerializeField] private GameObject target;
    [SerializeField] private float      heightValue  = 6;
    [SerializeField] private float      forwardValue = 9;
    [SerializeField] private float      smoothValue  = 0.35f;
    private Vector3 lastVector3;
    private void Start()
    {
        lastVector3 = transform.position;
    }
    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, lastVector3);
        lastVector3 = Vector3.Lerp(lastVector3,transform.position+new Vector3(0, heightValue, 0)+transform.forward * forwardValue, smoothValue);
        target.transform.position = lastVector3;
        rig.weight = Mathf.Lerp(rig.weight, 1, 0.18f);
    }
}

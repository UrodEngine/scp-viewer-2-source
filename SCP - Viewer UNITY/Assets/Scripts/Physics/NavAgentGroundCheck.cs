using UnityEngine;
using UnityEngine.AI;

public sealed class NavAgentGroundCheck : MonoBehaviour
{
    private NavMeshAgent    _navMeshAgent;
    private short           _afterPuttedTimer;

    [SerializeField] private float CheckLenghtRay = 2;

    private void Start          ()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }   
    private void FixedUpdate    (){
        if (gameObject != null)
        {
            _afterPuttedTimer = _afterPuttedTimer > (short)0 ? (short)(_afterPuttedTimer - 1) : _afterPuttedTimer;
            if (_navMeshAgent.enabled is true || _navMeshAgent.isOnNavMesh is true)
            {
                return;
            }
            if (GetComponent<NavMeshAgent>())
            {
                GroundCheck();
            }
        }        
    }
    public  void GroundCheck    ()
    {
        if (Physics.Raycast(new Ray(transform.position,transform.up*-1*CheckLenghtRay), out RaycastHit OUTPUT,CheckLenghtRay) && OUTPUT.collider != null && _afterPuttedTimer <= 1)
        {
            Debug.DrawLine(transform.position, OUTPUT.point,Color.green,2);
            GetComponent<NavMeshAgent>().enabled = true;
        }     
    }
    public  void DisableNavmesh ()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        _afterPuttedTimer = 10;
    } /* Getted message from "CameraFOV" script */
}

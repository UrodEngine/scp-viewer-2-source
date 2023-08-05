using UnityEngine;
public sealed class ObjShadowRealizer : MonoBehaviour
{
    private Ray ray = new Ray();
    private Renderer rendererThis => GetComponent<Renderer>();
    private void Start()
    {
        ray.direction = transform.up * -1;  
    }

    private void FixedUpdate()
    {
        MainThreadHandler.AddOther(() =>
        {
            ray.origin = transform.position;
            Physics.Raycast(ray, out RaycastHit OUTPUT);
            rendererThis.enabled = Vector3.Distance(transform.position, OUTPUT.point) < 1.5f ? true : false;
        });
    }
}

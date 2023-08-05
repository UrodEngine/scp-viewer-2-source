using UnityEngine;

public class GoPoint : MonoBehaviour
{
    private float _minuserSpeed = 0.1f;

    private void FixedUpdate()
    {
        if (transform.localScale.x < 0) Destroy(this.gameObject);

        transform.localScale = ScaleMinuser();

        Collider[] NearestMens = Physics.OverlapSphere(transform.position, 90);
        if (NearestMens != null){
            for (int i = 0; i < NearestMens.Length; i++){     
                if (NearestMens[i].TryGetComponent<Man>(out Man dclass)){
                    dclass.DClassConfigs.interestPoint = transform.position;
                    dclass.DClassConfigs.walking = 100;
                }
            }
        }
    }
    private Vector3 ScaleMinuser() => transform.localScale - new Vector3(_minuserSpeed, _minuserSpeed, _minuserSpeed);
}

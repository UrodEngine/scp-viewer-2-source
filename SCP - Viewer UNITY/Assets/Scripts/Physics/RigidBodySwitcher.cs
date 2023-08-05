// Скрипт вешается на объект с физикой.
//
//
//

using UnityEngine;

public class RigidBodySwitcher : MonoBehaviour
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    private Rigidbody   _rbSource;
    private BoxCollider _bcSource;
    public  bool        isParented;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion

    private void OnEnable   ()  
    {
        if (transform.parent is null)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<BoxCollider>();

            _rbSource = GetComponent<Rigidbody>();
            _rbSource.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rbSource.interpolation             = RigidbodyInterpolation.Extrapolate;

            isParented = false;
        }
        else
        {
            GameObject.Destroy(gameObject.GetComponent<Rigidbody>());
            GameObject.Destroy(gameObject.GetComponent<BoxCollider>());
            isParented = true;
        }
    }
    private void LateUpdate ()  
    {
        if (gameObject.GetComponent<Rigidbody>()    is null) return;
        if (gameObject.GetComponent<BoxCollider>()  is null) return;

        if (transform.parent != null)
        {
            GameObject.Destroy(gameObject.GetComponent<Rigidbody>());
            GameObject.Destroy(gameObject.GetComponent<BoxCollider>());
            isParented = true;
        }
    }
}

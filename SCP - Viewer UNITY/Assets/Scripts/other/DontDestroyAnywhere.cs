using UnityEngine;
[AddComponentMenu("Globalizate/DontDestroyAnywhere")]
public class DontDestroyAnywhere : MonoBehaviour
{
    private void Start() => DontDestroyOnLoad(this.gameObject);
}

using UnityEngine;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
public sealed class BigPropSpawn : MonoBehaviour
{
    public Vector3      OriginalSize;
    public float        TransformSpeed;
    public GameObject   EnableAfterSpawn;
    public bool         killComponentAfter = false;
    
    void OnEnable()
    {
        OriginalSize                            = transform.localScale;
        transform.localScale                    = new Vector3(0, 0, 0);
        GetComponent<BigPropSpawn>().enabled    = true;
    }    
    void Update(){
        transform.localScale += (OriginalSize - transform.localScale) * TransformSpeed;

        if (transform.localScale.magnitude >= OriginalSize.magnitude-0.085f)
        {
            GetComponent<BigPropSpawn>().enabled    = false;
            transform.localScale                    = OriginalSize;

            if (EnableAfterSpawn != null)
            {
                EnableAfterSpawn.SetActive(true);
            }
            if (killComponentAfter)
            {
                Destroy(this);
            }
        }
    }
}

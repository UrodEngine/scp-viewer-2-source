using UnityEngine;

sealed class corros106 : MonoBehaviour
{
    /*=============================================================================*/
    private scp106                          scp106instance;
    private UnityEngine.AI.NavMeshAgent     navmeshagent;
    private float                           objScale    = 0;
    [SerializeField]private float           fadeSpeed   = 0.065f;
    [SerializeField]private GameObject      corrosPortal;
    [SerializeField]private AudioSource     corrosSound;
    [SerializeField]private AudioClip[]     corrosClips;
    /*=============================================================================*/

    private void Start          (){
        scp106instance  = GetComponent<scp106>();
        navmeshagent    = scp106instance.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    private void Update         (){
        corrosPortal.transform.localScale += ((new Vector3(1,1,1) * objScale) - corrosPortal.transform.localScale) * 0.6f;
    }
    private void FixedUpdate    (){
        if (objScale>0) objScale -= fadeSpeed;
    }
    private void OpenPortal     (){
        SetNormRotate();
        objScale = 1.5f;

        corrosSound.clip = corrosClips[Random.Range(0, corrosClips.Length)];
        corrosSound.Play();
    } //By animator
    private void SetNormRotate  (){
        Ray         checknormals = new Ray(transform.position, -transform.up);
        RaycastHit  pointofcheck;
        Physics.Raycast(checknormals, out pointofcheck);
        if (pointofcheck.transform.gameObject != null) 
            corrosPortal.transform.rotation = Quaternion.FromToRotation(transform.up, pointofcheck.normal);

    }
}

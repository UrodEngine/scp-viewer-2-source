using UnityEngine;
using UnityEngine.Animations.Rigging;

[AddComponentMenu("SCP/017 - shadow of the man")]
public sealed class scp017 : MonoBehaviour, IPassportData
{
    #region Alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private BonesSinuser   bonesSinuser;
    [SerializeField] private AudioSwitcher  audioSwitcher;
    [SerializeField] private Rig            rig;
    [SerializeField] private byte           rageValue;
    [SerializeField] private float          moveSpeed;

    public string   aliveName       { get { return "SCP - 017"; } set { } }
    public string   aliveSurname    { get { return "Shadow of the man"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    private Rigidbody thisRigidbody => GetComponent<Rigidbody>();
    private Transform thisTransform => GetComponent<Transform>();
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void Start          ()
    {
        bonesSinuser.SetSave();
    }
    private void FixedUpdate    ()
    {
        rageValue = rageValue > (byte)0 ? (byte)(rageValue - 1) : rageValue;
        bonesSinuser.BonesAnimate();
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            rageValue > 0 ? new Vector3(4, 4, 4) : new Vector3(1, 1, 1),
            rageValue > 0 ? 0.1f : 0.06f
            );
        rig.weight = Mathf.Lerp(rig.weight, rageValue > (byte)0 ? 1 : 0, 0.07f);
        moveSpeed =  Mathf.Lerp(moveSpeed, rageValue > 0 ? 85:0, 0.01f);
        if (IsMansNear(out GameObject Men))
        {
            RageVoid(Men);         
            rageValue = 15;
            audioSwitcher.FixedPlay();
        }
    }
    private void RageVoid       (in GameObject target)
    {
        try
        {
            transform.LookAt(target.transform, Vector3.up);
            thisRigidbody.velocity = transform.forward * moveSpeed;
            InteractiveMethods.FearByObject(transform.position, target.transform);
            if (target.TryGetComponent<Man>(out Man dclass))
            {
                dclass.navMeshAgent.speed = 54;
            }
        }
        catch { }
        
        
        if (Vector3.Distance(transform.position, target.transform.position) < 9) Destroy(target.gameObject);
    }
    private bool IsMansNear     (out GameObject menTarget)
    {
        Collider[] colliders    = Physics.OverlapSphere(transform.position,666);   
        NearObiUtilitiesSimpleStatic.SimpleRaycastAll(transform.position, colliders, 666, 0, out Collider[] cleared);
        NearObiUtilitiesSimpleStatic.NearestTargetGeneric<IAliveForm>(transform, 0, cleared, out menTarget);
        return menTarget != null? true : false;
    }
}

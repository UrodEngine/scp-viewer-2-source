using UnityEngine;
using UnityEngine.Animations.Rigging;

[AddComponentMenu("SCP/354 - krakens")]
public class scp354 : MonoBehaviour
{
    [SerializeField] private BonesSinuser bonesSinuser;
    [SerializeField] private ChainIKConstraint chain;

    private Animator _animator => GetComponent<Animator>();

    void Start()
    {
        bonesSinuser.SetSave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bonesSinuser.BonesAnimate();
        if(isAliveFormNear(out GameObject target,140))
        {
            _animator.enabled = true;
            chain.weight = Mathf.Lerp(chain.weight, 1, 0.3f);
            chain.transform.GetChild(0).transform.position = Vector3.Lerp(chain.transform.GetChild(0).transform.position, target.transform.position, 0.1f);
            if (Vector3.Distance(chain.transform.GetChild(0).transform.position, target.transform.position) < 5)
            {
                var AliveForm = target.GetComponent<IAliveForm>();
                AliveForm.GetField().SetDamage(155,true);
            }
        }
        else
        {
            _animator.enabled = false;
            chain.weight = Mathf.Lerp(chain.weight, 0, 0.1f);
        } 
    }

    private bool isAliveFormNear(out GameObject output,in float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        NearObiUtilitiesSimpleStatic.SimpleRaycastAll(transform.position, colliders, range, 45, out Collider[] cleared);
        NearObiUtilitiesSimpleStatic.NearestTargetGeneric<IAliveForm>(transform, 45, cleared, out GameObject aliveForm);
        output = aliveForm != null ? aliveForm : null;
        return aliveForm != null ? true : false;
    }
}

using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("SCP/939 - So many voices")]
public class scp939 : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    private float               temporalSpeed = 0;

    public  MurderProperties              murderConfigs = new MurderProperties();
    public  Animator            animator;
    public  NavMeshAgent        navMeshAgent;
    public  Rigidbody           rigidBody;

    public  AudioClip[]         lureSounds;
    public  SoundSpotsMonoBehaviour.Parameters soundsParameters;
    public  float               heigh => 3;

    public  string              aliveName       { get { return "SCP - 939"; } set { } }
    public  string              aliveSurname    { get { return "So many voices"; } set { } }
    public  short               aliveAges       { get { return 0; } set { } }
    public  SCPSkillNode[]      skills          { get; set; } = new SCPSkillNode[2] 
    {
        new SCPSkillNode("Run", 2000),
        new SCPSkillNode("Speak", 1500)
    };


    private void            Start           ()
    {
        SkillsSet();
        murderConfigs.IncludedObjects.parentGameObject = gameObject;

        murderConfigs.OnThinked += () => 
        { 
            if (Random.Range(0, 256) > 240)
            {
                Speak();
                SoundSpots.Generate(transform, lureSounds[Random.Range(0, lureSounds.Length)], out AudioSource aSource);
                SoundSpots.AppendParameters(aSource, soundsParameters);
            } 
        };

        skills[1].isUsedSkill += () => 
        {
            SoundSpots.Generate(transform, lureSounds[Random.Range(0, lureSounds.Length)], out AudioSource aSource);
            SoundSpots.AppendParameters(aSource, soundsParameters);
        };
    }
    private void            FixedUpdate     ()
    {
        MainThreadHandler.AddActions(() =>{murderConfigs.CirclingHeart();});
        murderConfigs.CheckDie(murderConfigs.IncludedObjects.parentGameObject);
        try
        {
            NearObiUtilitiesSimpleStatic.NearestTargetGeneric<Man>(transform, 5, murderConfigs.allRaycastedObjects, out GameObject men);

            foreach (Collider active in murderConfigs.allRaycastedObjects)
            {
                if (active.gameObject.TryGetComponent<Man>(out Man dclass))
                {
                    if (Vector3.Distance(dclass.transform.position, transform.position) < 15)
                    {
                        InteractiveMethods.FearByObject(transform.position, active.transform);
                    }
                }
            }

            if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
            {
                if (men)
                {
                    IWannaKillMen(men);
                    temporalSpeed = 100;
                }
                else
                {
                    if (murderConfigs.walking > 50)
                    {
                        navMeshAgent.SetDestination(murderConfigs.interestPoint);
                    }
                    else
                    {
                        navMeshAgent.SetDestination(transform.position);
                    }
                }
            }
        }
        catch
        {
            
        }

        WalkingAnimationUtility.Animate(animator, navMeshAgent, rigidBody, 1, 0.02f, 2, 0.1f);

        if (temporalSpeed > 0)
        {
            temporalSpeed--;
            navMeshAgent.speed = 12;
        }
        else
        {
            navMeshAgent.speed = 3.5f;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("speak"))
        {
            navMeshAgent.velocity = Vector3.zero;
        }


        if (navMeshAgent.speed > 7f)
        {
            if (!animator.GetCurrentAnimatorStateInfo(1).IsTag("run"))
            {
                animator.Play("run", 1);
            }
        }
        else
        {  
            if (!animator.GetCurrentAnimatorStateInfo(1).IsTag("walk"))
            {
                animator.Play("walk", 1);
            }
        }
    }
    private void            IWannaKillMen   (in GameObject men)
    {
        navMeshAgent.SetDestination(men.transform.position);
        if (Vector3.Distance(transform.position, men.transform.position) < 7)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            {
                animator.Play($"attack{Random.Range(1, 4)}");
                men.GetComponent<IAliveForm>().GetField().SetDamage(Random.Range(35,66),true);
            }
            else
            {
                navMeshAgent.velocity = Vector3.zero;
                rigidBody.velocity = Vector3.zero;
            }
        }
    }
    public  IAliveConfigs   GetField        () => murderConfigs;
    public  void            SkillsSet       ()
    {
        skills[0].isUsedSkill += () => 
        {
            temporalSpeed = 100;
        };
        skills[1].isUsedSkill += () => 
        {
            Speak();
        };
    }
    private void            Speak           ()
    {
        murderConfigs.walking = 0;
        murderConfigs.thinkingTimer = 1024;
        animator.Play("speak", 0);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 100);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Man>(out Man dclass))
            {
                dclass.DClassConfigs.interestPoint = transform.position;
                dclass.DClassConfigs.walking = 100;
            }
        }
    }
}

using UnityEngine;
using Dclasses  = System.Collections.Generic.List<Man>;
using Timer     = System.Timers.Timer;


[AddComponentMenu("SCP/008 - zombie plague")]
public class Scp008 : MonoBehaviour , IInteractiveInjector
{
    private bool        toggleDoor  = false;
    private Timer       timer       = new Timer(1024);
    private Animator    animator;

    [SerializeField] private Dclasses           dclasses = new Dclasses(32);
    [SerializeField] private ParticleSystem     particles;
    [SerializeField] private ParticleSystem     particlesForVictims;
    [SerializeField] private PreZombiePrefab[]  preZombiePrefabs;


    private void Start()
    {
        animator = GetComponent<Animator>();
        particles.Stop();
        //particlesForVictims.Play();

        timer.Start();
        timer.Elapsed += TimerEvent;
    }
    public void InteractiveRun()
    {
        toggleDoor = !toggleDoor;
        if (toggleDoor)
        {
            animator.Play("open");
            particles.Play();
        }
        else
        {
            animator.Play("close");
            particles.Stop();
        }
    }
    public void TimerEvent(object _obj, System.Timers.ElapsedEventArgs _eArgs)
    {
        try
        {
            MainThreadHandler.AddActions(() =>
            {
                try
                {
                    #region find dclass
                    if (toggleDoor)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.position, 16);
                        foreach (Collider collider in colliders)
                        {
                            if (collider.gameObject.TryGetComponent<Man>(out Man dclass))
                            {
                                if (!dclasses.Contains(dclass))
                                {
                                    dclasses.Add(dclass);
                                }
                            }
                        }
                    }
                    #endregion

                    #region damage
                    foreach (Man dclass in dclasses)
                    {
                        try
                        {
                            if (dclass.DClassConfigs.properties.health <= 15)
                            {
                                switch (dclass.ManType)
                                {
                                    case Man.ManTypeEnum.Dclass:
                                        GameObject dclassZombie = Instantiate(Get("dclass"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                    case Man.ManTypeEnum.Scientist:
                                        GameObject scientistZombie = Instantiate(Get("scientist"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                    case Man.ManTypeEnum.Security:
                                        GameObject securityZombie = Instantiate(Get("security"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                    case Man.ManTypeEnum.MTF:
                                        GameObject mtfZombie = Instantiate(Get("mtf"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                    case Man.ManTypeEnum.Chaos:
                                        GameObject chaosZombie = Instantiate(Get("chaos"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                    default:
                                        GameObject defaultZombie = Instantiate(Get("dclass"), dclass.gameObject.transform.position, dclass.gameObject.transform.rotation);
                                        break;
                                }
                                Destroy(dclass.gameObject);
                                dclasses.Remove(dclass);
                                return;
                            }
                            particlesForVictims.Emit(new ParticleSystem.EmitParams() { position = dclass.transform.position+new Vector3(0,7,0) }, 1);
                            dclass.DClassConfigs.properties.health -= 3;
                            dclass.DClassConfigs.components.animator.Play("Xhurt", 1);
                            dclass.DClassConfigs.components.animator.SetLayerWeight(1, 1);
                        }
                        catch
                        {
                            dclasses.Remove(dclass);
                            return;
                        }
                    }
                    #endregion
                }
                catch
                {
                    return;
                }
            }, priority: 0);
        }
        catch
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
        }

    }
    private GameObject Get(string key)
    {
        foreach (PreZombiePrefab item in preZombiePrefabs)
        {
            if (item.key == key)
            {
                return item.prefab;
            }
        }
        return null;
    }


    [System.Serializable]
    public class PreZombiePrefab
    {
        public GameObject prefab;
        public string key;
    }
}

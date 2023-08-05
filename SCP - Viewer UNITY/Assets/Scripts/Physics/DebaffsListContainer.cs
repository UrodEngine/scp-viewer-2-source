using System.Collections.Generic;
using UnityEngine;



public sealed class DebaffsListContainer : MonoBehaviour
{
    public static DebaffsListContainer instance;
    #region alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public  static List<GameObject>     burningObjects = new List<GameObject>();
    private static GameObject           firePrefab;

    private static SimpleDelayer        delayer = new SimpleDelayer(5);
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    public  void    AddObject   (in GameObject target, in List<GameObject> list)
    {
        if (!list.Contains(target))
        {
            list.Add(target);
        }
    }
    private void    Start       ()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            firePrefab = GameObject.Instantiate(PrefabManager.GetManagerByKey("debaffs").GetPrefab("Burn_prefab"));
            DontDestroyOnLoad(firePrefab);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public  void    FixedUpdate ()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            Burning();
        }
    }
    public  void    Burning     ()
    {
        ParticleSystem fire = firePrefab.GetComponent(nameof(ParticleSystem)) as ParticleSystem;

        for (short index = 0; index < burningObjects.Count; index++)
        {
            try
            {
                if (burningObjects[index].TryGetComponent<IAliveForm>(out IAliveForm alive))
                {

                    AliveForm form = (AliveForm)alive.GetField();

                    if (form.properties.heatLevel <= 0)
                    {
                        burningObjects.RemoveAt(index);
                        return;
                    }

                    fire.Emit(new ParticleSystem.EmitParams() { position = burningObjects[index].transform.position + new Vector3(Random.Range(-1f,1f), alive.heigh, Random.Range(-1f, 1f)) }, 1);

                    form.SetDamage(Random.Range(3, 7), true);
                    form.properties.heatLevel -= (short)Random.Range(3, 7);
                }
                else if (burningObjects[index].TryGetComponent<IObjectParameters>(out IObjectParameters @object))
                {
                    if (@object.GetProperties().heatLevel <= 0)
                    {
                        burningObjects.RemoveAt(index);
                        return;
                    }

                    fire.Emit(new ParticleSystem.EmitParams() { position = burningObjects[index].transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) }, 1);

                    @object.GetProperties().heatLevel   -= (short)Random.Range(3, 7);
                    @object.GetProperties().health      -= (short)Random.Range(3, 7);
                }
            }
            catch
            {
                burningObjects.RemoveAt(index);
                return;
            }
        }
    }
}


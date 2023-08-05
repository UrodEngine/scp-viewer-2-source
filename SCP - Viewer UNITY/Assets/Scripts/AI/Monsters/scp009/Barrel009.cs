using UnityEngine;

[AddComponentMenu("SCP/009 - barrel")]
public sealed class Barrel009 : MonoBehaviour , IPassportData
{
    #region Alterable values
    ///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public PhysicsPropProperties   breakByGravity;

    [SerializeField] private ParticleSystem mainParticle;
    [SerializeField] private ParticleSystem fogParticle;
    [SerializeField] private ParticleSystem plagueParticle;
    [SerializeField] private GameObject     decor;

    private SimpleDelayer delayer = new SimpleDelayer(6);
    public string   aliveName       { get { return "SCP - 009"; } set { } }
    public string   aliveSurname    { get { return "Red ice barrel"; } set { } }
    public short    aliveAges       { get { return 0; } set { } }
    ///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void FixedUpdate()
    {
        delayer.Move();

        if (delayer.OnElapsed())
        {
            if (breakByGravity.GetProperties().heatLevel > 0)
            {
                decor.SetActive(true);
                mainParticle.Emit(1);
                fogParticle.Emit(1);

                Collider[] colliders = Physics.OverlapSphere(transform.position, 15);

                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.TryGetComponent<IAliveForm>(out IAliveForm iAliveForm))
                    {
                        plagueParticle.Emit(new ParticleSystem.EmitParams() 
                        { 
                            position    = collider.transform.position + new Vector3(Random.Range(-0.5f,0.5f), iAliveForm.heigh, Random.Range(-0.5f, 0.5f)) ,
                            rotation3D  = new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360))
                            
                        }, 1);
                        AliveForm iAlive = (AliveForm)iAliveForm.GetField();
                        iAlive.SetDamage((int)(iAlive.properties.health/9.2f)+1, true);
                    }
                }
            }
            else
            {
                decor.SetActive(false);
            }
        }
    }
}

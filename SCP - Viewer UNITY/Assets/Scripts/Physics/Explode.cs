using UnityEngine;

sealed class Explode : MonoBehaviour
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public  enum         KillingMode    {NotFatal, AbsoluteKill }
    public  KillingMode  DamageMode     = KillingMode.NotFatal;
    public  float        explodeImpulse;
    public  float        radius;
    public  int          damage;
    /*=========================================================================================================================================================*/
    #endregion
    private void Start                      ()
    {
        if (CamMove_v2.instance != null)
        {
            CamMove_v2.instance.SetShake(Random.Range(0.2f,2.9f));
        }
        SearchRigidBodies();      
    }
    private void SearchRigidBodies          ()
    {
        GameObject[]    interactable = new NearObjUtilities().RaycastedArrayByComponent(transform, radius, "Rigidbody", 0);
        foreach (var item in interactable){
            if (item.GetComponent(typeof(Rigidbody)))
            {
                item.GetComponent<Rigidbody>().AddExplosionForce(explodeImpulse, transform.position, radius, 2, ForceMode.Impulse);
            }
            SearchObjToDamageOrKill(item);
        }
    }                                           //Search interactable RigidBody objects.
    private void SearchObjToDamageOrKill    (in GameObject target)
    {
        ///====================<s ÝÒÀ ÑÒÐÎÊÀ ÎÒÏÐÀÂËßÅÒ ÎÁÚÅÊÒ Â ÑÏÈÑÎÊ ÂÎÇÃÎÐÀÅÌÛÕ ÎÁÚÅÊÒÎÂ>====================================

        DebaffsListContainer.instance.AddObject(target, DebaffsListContainer.burningObjects);

        if      (DamageMode == KillingMode.NotFatal)
        {
            DamageTarget(target, damage);
        }
        else if (DamageMode == KillingMode.AbsoluteKill)
        {
            KillAbsolute(target);
        }
    }                       //Analysis to damage or kill this.
    private void DamageTarget               (in GameObject targata, in int damaga){
        if (targata.TryGetComponent<IAliveForm>(out IAliveForm component)){
            AliveForm field;
            component.GetField().SetDamage(damaga, true);
            new AliveFormFieldGetter(component, out field); //Âîçãîðàíèå äëÿ IAliveForm
            field.properties.heatLevel = (short)damaga;
        }
        else if (targata.TryGetComponent<IAliveConfigs>(out IAliveConfigs componentB)){
            AliveForm field;
            componentB.SetDamage(damaga, true);

            new AliveFormFieldGetter(componentB, out field);  //Âîçãîðàíèå äëÿ IAliveConfigs
            field.properties.heatLevel = (short)damaga;

        }      
        else if (targata.TryGetComponent<IObjectParameters>(out IObjectParameters componentC)){
            componentC.GetProperties().heatLevel = (short)damaga; //Âîçãîðàíèå äëÿ IObjectParameters
        }
    }       //Damage target if have interface and GetField.
    private void KillAbsolute               (in GameObject targata){
        if (targata.TryGetComponent<IAliveForm>(out IAliveForm component)) 
            component.GetField().KillForm(true);

        else if (targata.TryGetComponent<IAliveConfigs>(out IAliveConfigs componentB))
            componentB.KillForm(true);
    }                       //Kill target if have interface and GetField.
}

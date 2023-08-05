using UnityEngine;

public class DamageOnZone : MonoBehaviour
{
    public float    radius;
    public int      damage;
    public bool     withBlood;

    void FixedUpdate()
    {
        GameObject[] onlyDclass = new NearObjUtilities().RaycastedArrayByComponent(transform, radius, nameof(Man), 0);
        foreach (var item in onlyDclass)
        {
            if(item.TryGetComponent<IAliveForm>(out IAliveForm component))
            {
                component.GetField().SetDamage(damage,withBlood);
            }
        }
    }
}

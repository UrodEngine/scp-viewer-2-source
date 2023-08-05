using UnityEngine;
using System;

public sealed class BulletAI : MonoBehaviour
{
    public  GameObject  ñurrentVictim;
    public  Action      onMoved;
    public  ushort      damage;


    private void Awake                      ()
    {
        onMoved = MoveToSelfVector;
    }
    public  void MoveToSelfVector           ()
    {
        GameObject  trail       = transform.GetChild(1).transform.gameObject;
        trail.transform.parent  = null;

        if (Physics.Raycast(transform.position, transform.forward,out RaycastHit output))
        {
            if (output.point != null) 
            { 
                transform.position                          = output.point;
                trail.GetComponent<MoveToPoint>().toPoint   = output.point;
                transform.parent                            = output.transform; 
                ñurrentVictim                               = output.transform.gameObject;

                SearchAliveFormInstance();
            }
            else
            {
                Destroy(gameObject);
                Destroy(trail);
            }
        }
    }
    ///<summary>If object have <see cref="IAliveForm"/> = SetDamage.</summary>
    private void SearchAliveFormInstance    ()
    {

        if (transform.parent.gameObject == ñurrentVictim)
        {
            if (ñurrentVictim.TryGetComponent<IAliveForm>(out IAliveForm component)) component.GetField().SetDamage(damage, true);
        }
    }  
}

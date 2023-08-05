using UnityEngine;

public class scp457_mini : MonoBehaviour,IObjectParameters
{
    [SerializeField] private GameObject scp457prefab;
    [SerializeField] private ObjectFormProperties properties;

    public ObjectFormProperties GetProperties()
    {
        return properties;
    }
    
    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15);
        if(colliders!=null)
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<IAliveForm>(out IAliveForm field))
                {
                    AliveForm form;
                    new AliveFormFieldGetter(field, out form);
                    form.properties.heatLevel = 40;
                    properties.heatLevel++;
                    DebaffsListContainer.instance.AddObject(colliders[i].gameObject, DebaffsListContainer.burningObjects);
                }
            }
        if (properties.heatLevel > 120)
        {
            Instantiate(scp457prefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

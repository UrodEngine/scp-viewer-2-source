using UnityEngine;

public class AuraCheckItems : MonoBehaviour
{
    public ImportantTools.ImportantToolsList  _thisType;
    void LateUpdate()
    {
        Collider[] DclassNearest = Physics.OverlapSphere(transform.position, 90);
        foreach (var item in DclassNearest)
        {
            if (item != null && item.GetComponent<Man>())
            {
                switch (_thisType)
                {
                    case ImportantTools.ImportantToolsList.Medkit:
                        //item.GetComponent<Dclass>().MedkitIsNear = true;
                        break;
                    case ImportantTools.ImportantToolsList.AmmoBox:
                        //item.GetComponent<Dclass>().AmmoIsNear = true;
                        break;
                }              
            }
        }
    }
}

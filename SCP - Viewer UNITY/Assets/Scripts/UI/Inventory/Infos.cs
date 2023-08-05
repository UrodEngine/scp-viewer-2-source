using UnityEngine;
using UnityEngine.UI;

public class Infos : MonoBehaviour
{
    public Text         AliveFormInformation;
    protected void Update(){
        if (CamMove_v2.instance.SavedGameObject is null)
        {
            AliveFormInformation.text = "Select random man";
            return;
        }
        if (CamMove_v2.instance.SavedGameObject != null){

            string  name        = "-";
            string  surname     = "-";
            short   ages        = 0;
            int     health      = 0;
            int     armor       = 0;

            GameObject selectedObject = CamMove_v2.instance.SavedGameObject.transform.gameObject;

            if(selectedObject.TryGetComponent<IPassportData>(out IPassportData passport))
            {
                 name        = passport.aliveName;
                 surname     = passport.aliveSurname;
                 ages        = passport.aliveAges;
            }
            if (selectedObject.TryGetComponent<IAliveForm>(out IAliveForm Ialiveform))
            {
                new AliveFormFieldGetter(Ialiveform, out AliveForm aliveForm);
                health  = aliveForm.properties.health;
                armor   = aliveForm.properties.armor;
            }
            AliveFormInformation.text = 
                $"Name: {name}" +
                $"\nSurname: {surname}" +
                $"\nAge: {ages}" +
                $"\nHealth: {health}" +
                $"\nArmor: {armor}  ";
            //InventorySlots.GetComponent<Item_slots>().SlotsUpdate();
        }        
    }
}

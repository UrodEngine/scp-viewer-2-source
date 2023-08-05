using UnityEngine;
using UnityEngine.UI;
public class HatsMagazine : MonoBehaviour
{
    #region alterable values
    //````````````````````````````````````````````````````````````````````````````````````````````````````
    [SerializeField] private GameObject exampleButton;
    [SerializeField] private Transform  contentTransform;
    [SerializeField] private Text       availableTicketsText;

    [SerializeField, Header("Men:")]        private MeshFilter          menHat;
    [SerializeField, Header("Ads:")]        private AdHatsInitializer   ads;
    [SerializeField, Header("Buttons:")]    private Button              purchaseButton;

    private HatButtonItem selectedHat;
    //````````````````````````````````````````````````````````````````````````````````````````````````````
    #endregion

    private void Start()
    {
        exampleButton.SetActive(false);

        PrefabManager hats = PrefabManager.GetManagerByKey("hats");

        for (int i = 0; i < hats.GetPrefabs().Length; i++)
        {
            //create button
            GameObject button   = Instantiate(exampleButton, contentTransform);
            button.name         = $"btn-{hats.GetPrefabs()[i].name}";
            button.SetActive(true);

            //get button item script
            HatButtonItem item = button.GetComponent<HatButtonItem>();
            item.id             = (short)i;
            item.itemName.text  = hats.GetPrefabs()[i].name;
            item.mesh           = hats.GetPrefabs()[i].GetComponent<MeshFilter>().sharedMesh;

            //set button action
            item.mainButton.onClick.AddListener(() => 
            {
                menHat.mesh = item.mesh;
                selectedHat = item;

                if (item.purchased || ads.availableBuyTickets <= 0)
                {
                    purchaseButton.interactable = false;
                }
                else
                {
                    purchaseButton.interactable = true;
                }
            });

            //set activate-btn action
            item.enableButton.onClick.AddListener(() =>
            {
                item.activated = !item.activated;
            });
        }

        //set purchase button
        purchaseButton.onClick.AddListener(() =>
        {
            ads.availableBuyTickets--;
            if (ads.availableBuyTickets <= 0) purchaseButton.interactable = false;
            
            selectedHat.purchased = true;
        });
    }
    private void FixedUpdate()
    {
        availableTicketsText.text = $"You can purchase: {ads.availableBuyTickets} hats";
    }
}

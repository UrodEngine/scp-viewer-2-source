using UnityEngine;
using UnityEngine.UI;
using System.IO;

public sealed class HatButtonItem : MonoBehaviour
{
    #region alterable values
    //````````````````````````````````````````````````````````````````````````````````````````````````````
    public static Color purchasedItemColor      = new Color(0.19f, 0.43f, 0.79f, 1);
    public static Color activatedItemColor      = new Color(0.19f, 0.43f, 0.79f, 1);
    public static Color nonActivatedItemColor   = Color.gray;

    public short    id;
    public Text     itemName;
    public Button   mainButton;
    public Button   enableButton;
    public Mesh     mesh;

    //purchased status
    private bool    _purchased = false;
    public  bool    purchased
    {
        get => _purchased; set
        {
            _purchased = value;
            if (value is true)
            {
                enableButton.interactable = true;
                mainButton.image.color = purchasedItemColor;

                CreateOrCheckPurchasedFile();
            }
            else
            {
                enableButton.interactable = false;
                mainButton.image.color = nonActivatedItemColor;
            }
        }
    }

    //activated status
    private bool    _activated = false;
    public  bool    activated { get => _activated; set 
        {
            _activated = value;
            if (value is true)
            {
                enableButton.image.color = purchasedItemColor;
                if (File.Exists(ModsDirectories.hats + "/" + itemName.text))
                    File.WriteAllText(ModsDirectories.hats + "/" + itemName.text, "purchased");
            }
            else
            {
                enableButton.image.color = nonActivatedItemColor;
                if (File.Exists(ModsDirectories.hats + "/" + itemName.text))
                    File.WriteAllText(ModsDirectories.hats + "/" + itemName.text, null);
            }
        } 
    }
    //````````````````````````````````````````````````````````````````````````````````````````````````````
    #endregion

    private void Start()
    {
        enableButton.interactable = false;
        if (File.Exists(ModsDirectories.hats + "/" + itemName.text))
        {
            purchased = true;
            if (File.ReadAllText(ModsDirectories.hats + "/" + itemName.text) != null && File.ReadAllText(ModsDirectories.hats + "/" + itemName.text).Length>0)
            {
                activated = true;
            }
        }
        HatToHeadImporter.updated = false;
    }
    private void CreateOrCheckPurchasedFile()
    {
        if (!File.Exists(ModsDirectories.hats + "/" + itemName.text))
        {
            File.Create(ModsDirectories.hats + "/" + itemName.text);
        }
        HatToHeadImporter.updated = false;
    }
}

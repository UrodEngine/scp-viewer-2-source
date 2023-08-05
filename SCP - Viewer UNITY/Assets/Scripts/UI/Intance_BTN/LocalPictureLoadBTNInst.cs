using UnityEngine.UI;
using UnityEngine;

public class LocalPictureLoadBTNInst : MonoBehaviour
{
    private Image image => GetComponent<Image>();
    private readonly SimpleDelayer delayer = new SimpleDelayer(255);
    private string updateTextCheck;

    private void Update()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            try 
            {
                string slotName = transform.parent.GetChild(0).GetComponent<Text>().text;
                if (updateTextCheck != slotName)
                {
                    updateTextCheck = slotName;
                    SPAWN_lists.UltimateModReferences modReferences = SPAWN_lists.instance.TryGetModResources(slotName);
                    if (modReferences != null)
                    {
                        image.sprite = modReferences.icon;
                        return;
                    }
                    else
                    {
                        image.sprite = Resources.Load($"SpawnListPictures/{slotName}", typeof(Sprite)) as Sprite;
                    }
                }                
            }
            catch { return; }
        }
    }
}

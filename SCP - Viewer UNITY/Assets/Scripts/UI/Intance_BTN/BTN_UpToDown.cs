using UnityEngine;

public class BTN_UpToDown : MonoBehaviour
{
    [SerializeField] private AudioClip bipClip;

    private int offsetScale = 5;

    public void ArraySetUp    ()
    {
        StartCoroutine(MoveUp());
    }
    public void ArraySetDown  ()
    {
        StartCoroutine(MoveDown());
    }
    public System.Collections.IEnumerator MoveUp()
    {
        for (short i = 0; i < offsetScale; i++)
        {
            yield return new WaitForSeconds(0.07f);
            if (SPAWN_lists.instance.ArrayOffset == 0)
            {
                Camera.main.GetComponent<SoundOnClick>().PlaySound(3);
                yield break;
            }

            SPAWN_lists.instance.ArrayOffset = Mathf.Clamp(SPAWN_lists.instance.ArrayOffset - 1, 0, 999);

            if (Camera.main.GetComponent(nameof(SoundOnClick)))
            {
                Camera.main.GetComponent<SoundOnClick>().PlaySound(bipClip, true, 0.3f);
            }
        }
    }
    public System.Collections.IEnumerator MoveDown()
    {
        for (short i = 0; i < offsetScale; i++)
        {
            yield return new WaitForSeconds(0.07f);
            SPAWN_lists.instance.ArrayOffset = Mathf.Clamp(SPAWN_lists.instance.ArrayOffset + 1, 0, 999);
            if (Camera.main.GetComponent(nameof(SoundOnClick)))
            {
                Camera.main.GetComponent<SoundOnClick>().PlaySound(bipClip,true, 0.3f);
            }
        }
    }
}

using UnityEngine;

[AddComponentMenu("SCP Tools/Comment cloud")]
public class CommentCloud : MonoBehaviour
{
    private Man          dclass          => transform.parent.GetComponent(nameof(Man)) as Man;
    private MeshRenderer    meshRenderer    => GetComponent(nameof(MeshRenderer)) as MeshRenderer;
    private AudioSwitcher   aSwitcher       => GetComponent(nameof(AudioSwitcher)) as AudioSwitcher;
    private void FixedUpdate()
    {
        if (dclass.dialogTimer > 0)
        {
            meshRenderer.enabled = true;
            aSwitcher.FixedPlay();
        }
        else meshRenderer.enabled = false;
    }
}

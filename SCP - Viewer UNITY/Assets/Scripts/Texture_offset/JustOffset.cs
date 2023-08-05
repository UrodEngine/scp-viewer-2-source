using UnityEngine;

public class JustOffset : MonoBehaviour
{
    public float speedSubstract;
    void Update()
    {
        var texture = GetComponent<Renderer>().material;
        float offset = Time.time * speedSubstract;
        texture.mainTextureOffset = new Vector2(offset, 0);
    }
}

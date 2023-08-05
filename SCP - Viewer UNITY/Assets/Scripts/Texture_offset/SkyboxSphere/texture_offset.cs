using UnityEngine;

public class texture_offset : MonoBehaviour
{
    public  float       speedSubstract;
    private GameObject  _parent;

    private void Start(){
        _parent = transform.parent.gameObject;
    }
    void Update(){
        var     texture = GetComponent<Renderer>().material;
        float   offset = Time.time * speedSubstract;

        texture.mainTextureOffset = new Vector2(offset, 0);
        RenderSettings.skybox.SetFloat("_Rotation", (Time.time * speedSubstract*15));        
    }
}

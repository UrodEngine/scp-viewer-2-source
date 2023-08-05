using UnityEngine;

public class Mat_instance_setter : MonoBehaviour
{
    [SerializeField] private Renderer   myObj;
    [SerializeField] private Texture    setTexture;
    [SerializeField] private bool       enableRandom;
    [SerializeField] private Texture[]  randomTextures;
    private sbyte randomSeed;
    private void Start()
    {
        randomSeed = (sbyte)Random.Range(0, randomTextures.Length);
    }
    void FixedUpdate()
    {
        if (enableRandom)
        {
            if (myObj.material.mainTexture != randomTextures[randomSeed]) myObj.material.mainTexture = randomTextures[randomSeed];
        }
        else
        {
            if (myObj.material.mainTexture != setTexture) myObj.material.mainTexture = setTexture;
        }
       
    }
}

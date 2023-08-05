using UnityEngine;
using UnityEngine.UI;

public class TextureRandomSet : MonoBehaviour
{
    private RawImage rawImage => GetComponent<RawImage>();
    [SerializeField] private Texture2D[] textures;
    private void OnEnable() => rawImage.texture = textures[Random.Range(0, textures.Length)];
}

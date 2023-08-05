using UnityEngine;

[AddComponentMenu("Start Tools/Enable by parent")]
public sealed class EnableByParent : MonoBehaviour
{
    [Header("Привязать объекты:")]
    [SerializeField] private GameObject[] attached;
    [SerializeField] private bool setActive = true;

    
    private void Start()
    {
        for (ushort index = 0; index < attached.Length; index++)
        {
            attached[index].SetActive(setActive);
        }
        Destroy(this);
    }
}

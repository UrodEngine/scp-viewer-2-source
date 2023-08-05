using UnityEngine;

[AddComponentMenu("Start Tools/Material changer at start")]
public class ChangeMatAtStart : MonoBehaviour
{
    public MaterialReplacer[] materialReplacer;   
    void OnEnable()
    {
        foreach (var item in materialReplacer)
        {
            item.SetParentObj(this.gameObject);
            item.ChangeMat();
        }       
    }
}
[System.Serializable]
public sealed class MaterialReplacer
{
    public Material[]   atStartMat;
    public int          indexOfWhatChange;
    private GameObject  parentObj;
    public void ChangeMat()
    {
        Material[] tempMats             = parentObj.GetComponent<MeshRenderer>().sharedMaterials;
        tempMats[indexOfWhatChange]     = atStartMat[Random.Range(0, atStartMat.Length)];
        parentObj.GetComponent<MeshRenderer>().sharedMaterials = tempMats;
    }
    public void SetParentObj(in GameObject setter) => parentObj = setter;
}
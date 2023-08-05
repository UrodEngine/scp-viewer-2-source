using UnityEngine;

[System.Serializable]
public struct BonesSinuser
{
    public Transform[] bonesList;
    [System.NonSerialized] public float[] savedWvalue;
    [System.NonSerialized] public float[] offsetSin;
    public float bonesSinSpeed;
    public float bonesSinmultiplier;
    public  void    SetSave         ()
    {
        savedWvalue = new float[bonesList.Length];
        offsetSin = new float[bonesList.Length];
        for (short i = 0; i < bonesList.Length; i++)
        {
            savedWvalue[i] = bonesList[i].transform.localRotation.w;
            offsetSin[i] = Random.Range(1.0f, 5.0f);
        }
    }
    private float   SinConvert      (in float @new, in short @index)
    {
        return savedWvalue[@index] + (float)System.Math.Sin(Time.time + offsetSin[@index] * bonesSinSpeed) * bonesSinmultiplier;
    }
    public  void    BonesAnimate    ()
    {
        for (short i = 0; i < bonesList.Length; i++)
        {
            bonesList[i].transform.localRotation = new Quaternion
                (
                bonesList[i].transform.localRotation.x,
                bonesList[i].transform.localRotation.y,
                bonesList[i].transform.localRotation.z,
                SinConvert(bonesList[i].transform.localRotation.w, i)
                );
        }
    }
}

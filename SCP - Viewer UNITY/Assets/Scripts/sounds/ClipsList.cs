using UnityEngine;
[System.Serializable]
public sealed class ClipsList 
{
    [System.Serializable]
    public class Elements
    {
        public AudioClip clip;
        public string name;
    }
    public Elements[] elements;
    
    public AudioClip Get(in string _name)
    {
        for (byte index = 0; index < elements.Length; index++)
        {
            if (elements[index].name == _name)
            {
                return elements[index].clip;
            }
        }
        return null;
    }
    public AudioClip Get(in AudioClip _clip)
    {
        for (byte index = 0; index < elements.Length; index++)
        {
            if (elements[index].clip == _clip)
            {
                return elements[index].clip;
            }
        }
        return null;
    }
}

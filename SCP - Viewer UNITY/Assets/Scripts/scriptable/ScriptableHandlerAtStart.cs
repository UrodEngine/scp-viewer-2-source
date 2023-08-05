using UnityEngine;

public class ScriptableHandlerAtStart : MonoBehaviour
{
    private static ScriptableHandlerAtStart instance;
    [SerializeField] private ScriptableObject[] attached;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

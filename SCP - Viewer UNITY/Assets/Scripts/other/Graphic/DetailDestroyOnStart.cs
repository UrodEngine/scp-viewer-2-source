using UnityEngine;

[AddComponentMenu("Start Tools/Settings/Delete object by low graphic")]
public class DetailDestroyOnStart : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.OPTIONS_DECAL_DETAILS) is 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLogs : MonoBehaviour
{
    [SerializeField] private bool disableLogs = true;
    void Start()
    {
        if (disableLogs)
        {
            Debug.unityLogger.logEnabled = false;
            Destroy(this);
        }
    }
}

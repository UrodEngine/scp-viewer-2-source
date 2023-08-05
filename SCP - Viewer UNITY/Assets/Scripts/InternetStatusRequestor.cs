using System;
using System.Collections;
using UnityEngine;

public sealed class InternetStatusRequestor : MonoBehaviour
{
    #region alterable values
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public static InternetStatusRequestor instance;
    public static bool isConnected = false;
    public Action OnNetAvailable = () => { };
    public Action OnNetFailure   = () => { };
    private bool useEvents = true;

    private SimpleDelayer delayer = new SimpleDelayer(byte.MaxValue);
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #endregion

    private void Awake()
    {
        instance = this;
    }
    private IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
            if (useEvents)
            {
                OnNetFailure();
                useEvents = false;
            }
        }
        else
        {
            action(true);
            if (useEvents)
            {
                OnNetAvailable();
                useEvents = false;
            }
        }
    }
    private void FixedUpdate()
    {
        delayer.Move();
        if (delayer.OnElapsed())
        {
            StartCoroutine(checkInternetConnection((isConnected) => 
            {
                InternetStatusRequestor.isConnected = isConnected;
            }));
        }
    }
}

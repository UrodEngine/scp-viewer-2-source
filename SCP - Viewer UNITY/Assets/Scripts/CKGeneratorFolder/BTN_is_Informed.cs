using UnityEngine;

public class BTN_is_Informed : MonoBehaviour
{
    public static bool  isInformed;
    public bool         isLocalInformed;

    public void SetInformed     () => isInformed = true;
    public void ResetInformed   () => isInformed = false;

    public void LocalSetInformed() => isLocalInformed = true;
    public void LocalResetInformed() => isLocalInformed = false;
}

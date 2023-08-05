using UnityEngine;

public class Movement_joystick : MonoBehaviour
{
    //=========== alterable values =====================================================================================================================
    private FixedJoystick   fixedJoystick   { get => GetComponent<FixedJoystick>(); }
    private AliveForm       selectedAliveForm 
    { 
        get 
        {
            if (CamMove_v2.instance.SavedGameObject && CamMove_v2.instance.SavedGameObject.TryGetComponent<IAliveForm>(out IAliveForm aliveform))
            {
                AliveForm field;
                new AliveFormFieldGetter(aliveform,out field);
                return field;
            }
            else
                return null; 
        } 
    }
    [SerializeField] private LayerMask mask;
    [SerializeField] private UnityEngine.UI.Text debugger;
    [SerializeField] private GameObject pointVisualiser;
    private Collider debuggerRaycasted;
    //==================================================================================================================================================

    private void Update(){

        #region debugger
        if (debugger && debugger.gameObject.activeSelf)
        {
            debugger.text =
            $"direction: {fixedJoystick.Direction}\n" +
            $"horizontal: {fixedJoystick.Horizontal}\n" +
            $"vertical: {fixedJoystick.Vertical}\n"+
            $"raycasted: {debuggerRaycasted?.name ?? "null"}";
        }
        #endregion

        RectTransform joystickRect  = GetComponent<RectTransform>();
        RectTransform distancedRect = transform.GetChild(0).GetComponent<RectTransform>();

        if (Vector2.Distance(joystickRect.position, distancedRect.position) < 0.1f)
        {
            return;
        }
        if (selectedAliveForm is null)
        {
            return;
        }

        selectedAliveForm.walking = 100;
        GameObject selectedObject = selectedAliveForm.IncludedObjects.parentGameObject;

        Vector3 startRay = selectedObject.transform.position + new Vector3(0, 3, 0);
        Vector3 directionRay = new Vector3(fixedJoystick.Direction.x*55, selectedObject.transform.position.y-1f, fixedJoystick.Direction.y * 55);

        Ray dirToWalk = new Ray(startRay,directionRay);

        Physics.Raycast(dirToWalk, out RaycastHit result, 15,mask,QueryTriggerInteraction.Ignore);
        debuggerRaycasted = result.collider;
        if (result.collider != null)
        {
            Debug.DrawLine(startRay, result.point, Color.green);
            selectedAliveForm.interestPoint = result.point;
            if (pointVisualiser && pointVisualiser.activeSelf)
            {
                pointVisualiser.transform.position = result.point;
            }
        }
        else
        {
            Debug.DrawLine(startRay, selectedObject.transform.position + dirToWalk.direction * 15, Color.yellow);
            selectedAliveForm.interestPoint = selectedObject.transform.position + new Vector3(dirToWalk.direction.x, transform.position.y + 1f, dirToWalk.direction.z);
        }
    }
}

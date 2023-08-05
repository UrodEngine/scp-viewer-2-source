// Camera v2. 10.04.2022 - Update: Теперь обработка нажатий работает корректно.
// Camera v2. 08.06.2023 - Update: Добавлен _shakeValue.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public sealed class CamMove_v2 : MonoBehaviour
{
    #region Alterable values
    /*========================================================================================================================*/
    public const float DEFAULT_MIN_X_DIST = -118.1f;
    public const float DEFAULT_MAX_X_DIST = 118.1f;
    public const float DEFAULT_MIN_Y_DIST = -42.15f;
    public const float DEFAULT_MAX_Y_DIST = 42.15f;

    public const float DEFAULT_SPEED_MOVE = 125;

    public const float MIN_FOV = 30;
    public const float MAX_FOV = 70.4f;
    /*========================================================================================================================*/
    public static CamMove_v2 instance;

    /// <summary>
    /// ГРАНИЦЫ КАМЕРЫ. НЕОБХОДИМО, ЧТОБЫ КАМЕРА НЕ БЛУЖДАЛА БЕСКОНЕЧНО
    /// </summary>
    public float        minXdist, maxXdist, minYdist, maxYdist; 

    public float        cameraMoveSpeed         = 125;
    public float        distanceToSpawnBreak    = 1f;
    public float        camMoveSmoothSpeedValue = 0.12f;
    public enum         SettedMode {None = 0, CameraMove = 1, ObjectMove = 2, CameraZoom = 3, ObjectSpawn = 4, InteractiveTip = 5, Awaiting = 6}
    public SettedMode   CameraMode;

    [HideInInspector] public bool   isUItouched;
    [HideInInspector] public bool   isWindowOpened;
    [HideInInspector] public bool   isTippedOnce;
    [HideInInspector] public bool   isManualMovement;
    [HideInInspector] public bool   isDclassManualMovementOpened;
    [HideInInspector] public bool   IDontWannaSpawnObject;

    public GameObject   SelectedObject;
    public GameObject   SavedGameObject;
    public GameObject   SpawningEffect;
    public LayerMask    layersToIgnore;
    public Image        faderImg;

    [SerializeField] private float       _tempFOV        = 0;
    [SerializeField] private float       _shakeValue     = 0;
    private bool        _spawnLock;
    private int         _tempTouchsCount;
    private Camera      _cameraV2;
    private Vector2[]   _touchIDPos          = new Vector2[10];
    private Vector3     _tempCameraPos       = new Vector3();    //Нужен для сохранения последней позиции камеры, от которой позже будет отклонение.
    private Vector3     _tempDirectionRay    = new Vector3();    //Нужен для сохранения последней позиции камеры, от которой позже будет отклонение.
    private Vector3     _distance;
    /*========================================================================================================================*/
    #endregion


    private void    Start                   ()
    {
        instance = this;
        SetDefaultOptions();
        _cameraV2                = transform.GetComponent<Camera>();
        _cameraV2.fieldOfView    = 70f;
        _tempFOV = _cameraV2.fieldOfView;
    }
    private void    Update                  ()
    {
        #region shake value
        if (_shakeValue > 0.1f)
        {
            _shakeValue += (0 - _shakeValue) * 0.1f;
            _cameraV2.fieldOfView = _tempFOV + _shakeValue; 
        }
        else
        {
            _tempFOV = Mathf.Clamp(_tempFOV, MIN_FOV, MAX_FOV);
            
            _shakeValue = 0;
        }
        #endregion

        #region move object by mouse
        if (isManualMovement && SavedGameObject != null)
        {
            MoveTowardsReservedObj();
        }
        #endregion

        #region touch modes
        if (Input.touchCount != 0)
        {
            ChangeFingerCountMethod();
            CameraFunctionPerMode(CameraMode);            
        }

        if (Input.touchCount is 1 && CameraMode is SettedMode.CameraZoom)
        {
            CameraMode = SettedMode.Awaiting;
        }

        if (Input.touchCount <= 0)
        {
            CameraMode              = SettedMode.None;
            isUItouched             = false;
            isTippedOnce            = false;
            _distance               = new Vector3();
            _touchIDPos             = new Vector2[10];
            _tempTouchsCount        = 0;
            SelectedObject          = null;
            IDontWannaSpawnObject   = false;
            _spawnLock              = false;
        }
        else if (Input.touchCount is 1)
        {
            ///Break method if UI touched .
            if (RunCheckFingerOnUI() is true)   return;
            if (isUItouched is true)            return;

            //-----------------------------------------------------------------------
            Ray checkPoint = Camera.main.ScreenPointToRay(Input.touches[0].position);
            Physics.Raycast(checkPoint, out RaycastHit raycastHit);
            //-----------------------------------------------------------------------

            ///Setted tip position of first finger;
            if (isTippedOnce is false) 
            { 
                _distance = raycastHit.point; 
                isTippedOnce = true; 
            }

            ///Setted reservedSelObject by raycastHit. <s FOR MANUAL MOVEMENT>
            if (SavedGameObject == null && raycastHit.transform.gameObject.TryGetComponent<IPassportData>(out IPassportData field))
            {
                //new AliveFormFieldGetter(field, out AliveForm aliveform);
                SavedGameObject = raycastHit.collider.gameObject;
            }

            ///Try set Cursor mode if <s NONE>
            if (CameraMode != SettedMode.None && IDontWannaSpawnObject is true) return;
            if      (raycastHit.transform.gameObject   != null && raycastHit.transform.tag == "Interactable") 
            {
                CameraMode = SettedMode.ObjectMove; 
                SelectedObject = raycastHit.transform.gameObject;
            }
            else if (raycastHit.transform.gameObject   != null && raycastHit.transform.tag == "W_Interactive") 
            {
                CameraMode = SettedMode.InteractiveTip;
                SelectedObject = raycastHit.transform.gameObject;
            }
            else if ((raycastHit.transform.gameObject  is null || raycastHit.transform.tag != "Interactable")) CameraMode = SettedMode.ObjectSpawn;
            else    CameraMode = SettedMode.CameraMove;

            IDontWannaSpawnObject = true;
        }
        else if (Input.touchCount == 2) CameraMode = SettedMode.CameraZoom;
        #endregion 
    }
    private void    ChangeFingerCountMethod ()
    {
        if (_tempTouchsCount != Input.touchCount && Input.touchCount != 0)
        {
            Ray checkPoint      = Camera.main.ScreenPointToRay(Input.touches[0].position);
            _tempDirectionRay   = checkPoint.direction;

            _tempTouchsCount    = Input.touchCount;
            _tempFOV            = Mathf.Clamp(_cameraV2.fieldOfView,MIN_FOV,MAX_FOV+9);
            _tempCameraPos      = _cameraV2.transform.position;

            for (byte i = 0; i < Input.touchCount; i++)
            {
                _touchIDPos[i] = Input.GetTouch(i).position;
            }
        }
    }   //If changes touchCounts - update some data.
    private void    CameraFunctionPerMode   (in SettedMode setmode)
    {
        if (isUItouched || isWindowOpened)
        {
            CameraMode = SettedMode.Awaiting; 
            return; 
        }
        switch (setmode)
        {
            case SettedMode.None:
                break;

            case SettedMode.CameraMove:
                MovedValue();
                break;

            case SettedMode.ObjectMove: 
                try{
                    MovedObjectValue(SelectedObject);
                }
                catch{
                    CameraMode = SettedMode.Awaiting; 
                    IDontWannaSpawnObject = false;
                    return;
                }
                break;
            case SettedMode.CameraZoom:
                if (Input.touchCount < 2) { CameraMode = SettedMode.CameraMove; IDontWannaSpawnObject = true; }
                else CameraZoom();
                break;

            case SettedMode.ObjectSpawn:
                if (isWindowOpened) { CameraMode = SettedMode.Awaiting; return; }
                if (Vector3.Distance(_touchIDPos[0], Input.GetTouch(0).position) < 1f) SpawnSelectedObject();
                else CameraMode = SettedMode.CameraMove;
                break;

            case SettedMode.InteractiveTip:
                SelectedObject.GetComponent<IInteractiveInjector>().InteractiveRun();
                CameraMode = SettedMode.Awaiting;
                break;

            default:
                break;
        }
    }
    private void    CheckObjectToNavMesh    (in GameObject obj)
    {
        obj.GetComponent<NavAgentGroundCheck>()?.DisableNavmesh();
    }
    private void    SpawnSelectedObject     (){
        if (Input.touches[0].phase is TouchPhase.Ended){
            Ray _ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(_ray, out RaycastHit SpawnPoint)){
                if (SPAWN_lists.instance.SelectedInstance != null){
                    GameObject NewObject = Instantiate(
                        SPAWN_lists.instance.SelectedInstance,
                        SpawnPoint.point + SpawnPoint.normal * 2, 
                        Quaternion.identity);
                    GameObject spawnEffect = Instantiate(
                        SpawningEffect,
                        SpawnPoint.point + SpawnPoint.normal * 2,
                        Quaternion.identity);
                    NewObject.transform.rotation = Quaternion.Euler(
                        NewObject.transform.rotation.x, 
                        Random.Range(0, 360), 
                        NewObject.transform.rotation.z);
                }
            }
            //Debug.Log("Спавним объект " + Input.touches[0].fingerId);
        }
    }
    private bool    RunCheckFingerOnUI      (){
        //Check finger on UI elements. Lock moving if true.
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                //Debug.Log("Палец задел UI");
                CameraMode = SettedMode.Awaiting;
                isUItouched = true;
                return true;
            }
        }
        return false;
    }
    private void    MovedObjectValue        (GameObject selected){
        //Move object toward finger;     
        try{
            Ray posToTransform      = _cameraV2.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit output       = new RaycastHit();

            if (Physics.Raycast(posToTransform, out output, Mathf.Infinity, layersToIgnore)){
                Vector3 outputPoint = output.point + output.normal * 5;
                Rigidbody rigBod    = selected.GetComponent<Rigidbody>();
                rigBod.velocity     = ((selected.transform.position - outputPoint) * 5) * -1;
                CheckObjectToNavMesh(selected);
            }
        }
        catch{
            CameraMode = SettedMode.Awaiting;
            return;
        }
    }   
    private void    CameraZoom              ()
    {
        _spawnLock      = true;
        isTippedOnce    = true;

        float clampedZoomed = Mathf.Clamp(_tempFOV - MathUE.DistanceOf2points(Input.GetTouch(0).position.x, Input.GetTouch(1).position.x, _touchIDPos[0].x, _touchIDPos[1].x, 0.23f), MIN_FOV, MAX_FOV);

        _cameraV2.fieldOfView = clampedZoomed + _shakeValue;
    }
    private void    MovedValue              (){
        //Move camera toward finger;
        Ray ray             = _cameraV2.ScreenPointToRay(Input.GetTouch(0).position);
        Vector3 OriginalTransform;
        OriginalTransform   = _tempCameraPos - new Vector3(_tempDirectionRay.x - ray.direction.x, _tempDirectionRay.y - ray.direction.y, 0) * cameraMoveSpeed;
        transform.position  += (OriginalTransform - transform.position) * 0.1f;
        transform.position  = new Vector3(
            Mathf.Clamp(transform.position.x, minXdist, maxXdist), 
            Mathf.Clamp(transform.position.y, minYdist, maxYdist), 
            transform.position.z);
    }
    public  void    SetSpawnLock            (in bool value)
    {
        _spawnLock = value;
    }
    public  void    SetDarkScreen           ()
    {
        float r = faderImg.color.r;
        float g = faderImg.color.g;
        float b = faderImg.color.b;

        faderImg.color = new Color(r, g, b, 1);
    }
    public  void    SetShake                (in float value) => _shakeValue = value;
    public  void    ClearReservedObj        () => SavedGameObject = null;
    private void    MoveTowardsReservedObj  () => transform.position = Vector3.Lerp(transform.position, new Vector3(SavedGameObject.transform.position.x, SavedGameObject.transform.position.y + 5f, transform.position.z), 0.03f);

    #region setBounds
    public static void SetDefaultOptions()
    {
        instance.minXdist = DEFAULT_MIN_X_DIST;
        instance.maxXdist = DEFAULT_MAX_X_DIST;
        instance.minYdist = DEFAULT_MIN_Y_DIST;
        instance.maxYdist = DEFAULT_MAX_Y_DIST;
    }
    public static void SetOptions(in float minX, in float maxX, in float minY, in float maxY , in float speed)
    {
        instance.minXdist = minX;
        instance.maxXdist = maxX;
        instance.minYdist = minY;
        instance.maxYdist = maxY;

        instance.cameraMoveSpeed = speed;
    }
    #endregion
}

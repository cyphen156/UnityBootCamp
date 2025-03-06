using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerHead;
    public Transform playerLookObj;
    public CharacterController characterController;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float thirdPersonDistance;
    [SerializeField] private Vector3 thirdPersonOffset;
    [SerializeField] private float zoomDistance;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float defaultFov;
    [SerializeField] private float zoomFov;
    [SerializeField] private float currentDistance;
    [SerializeField] private float targetDistance;
    [SerializeField] private float targetFov;
    [SerializeField] private bool isZoomed;
    [SerializeField] private Coroutine zoomCoroutine;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float pitch;
    [SerializeField] private float yaw;
    [SerializeField] private bool isFirstPerson;
    [SerializeField] private bool isRotaterAroundPlayer;

    // Physics
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGround;

    private void Awake()
    {
        // initalize necessary Vars
        moveSpeed = 5.0f;
        mouseSensitivity = 100.0f;
        //cmaeraTransform;
        characterController = GetComponent<CharacterController>();
        //playerHead = 
        thirdPersonDistance = 3.0f;
        thirdPersonOffset = new Vector3(0f, 1.5f, 0f);
        //playerLookObj = 
        zoomDistance = 1.0f;
        zoomSpeed = 5.0f;
        defaultFov = 60.0f;
        zoomFov = 30.0f;

        isZoomed = false;

        pitch = 0.0f;
        yaw = 0.0f;
        isFirstPerson = false;
        isRotaterAroundPlayer = false;
        gravity = -9.81f;
        jumpHeight = 2.0f;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = thirdPersonDistance;
        targetDistance = zoomDistance;
        targetFov = defaultFov;
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.fieldOfView = defaultFov;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45f, 45f);

        isGround = characterController.isGrounded;

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            Debug.Log($"1인칭모드 : {isFirstPerson}");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isRotaterAroundPlayer = !isRotaterAroundPlayer;
            Debug.Log($"플레이어 주변 카메라 회전모드 {isRotaterAroundPlayer}"); ;
        }

        if (isFirstPerson)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
        }

        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isZoomed = false;
        }
    }

    void FirstPersonMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * v + cameraTransform.right * h;
        move.y = 0f;    // 점프 안뛰엇으니까
        characterController.Move(move * moveSpeed * Time.deltaTime);    // 이동도 잘 생각해보면 축을 기준으로 이동

        cameraTransform.position = playerHead.position;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0); // 벡터 합성으로 방향을 바꿔가면서 앞으로만 이동

        transform.rotation = Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0f); // 회전은 항상 축을 기준으로 돌려
    }

    void ThirdPersonMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        UpdateCameraPosition();
    }
    void UpdateCameraPosition()
    {
        if (isRotaterAroundPlayer)
        {
            //카메라가 플레이어 오르쪽에서 회전하도록 설정
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //카메라를 플레이어의 오른쪽에서 고정된 위치로 이동.
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;
            //카메라가 플레이어의 위치를 따라가도록 설정
            cameraTransform.LookAt(transform.position + new Vector3(0f, thirdPersonOffset.y, 0f));
        }
        else
        { //플레이어가 직접 돈다
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            cameraTransform.position = playerLookObj.position + thirdPersonOffset + Quaternion.Euler(pitch, yaw, 0) * direction;
            cameraTransform.LookAt(playerLookObj.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
    }
}
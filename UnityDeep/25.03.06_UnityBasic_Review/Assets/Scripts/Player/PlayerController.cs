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
            Debug.Log($"1��Ī��� : {isFirstPerson}");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isRotaterAroundPlayer = !isRotaterAroundPlayer;
            Debug.Log($"�÷��̾� �ֺ� ī�޶� ȸ����� {isRotaterAroundPlayer}"); ;
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
        move.y = 0f;    // ���� �ȶپ����ϱ�
        characterController.Move(move * moveSpeed * Time.deltaTime);    // �̵��� �� �����غ��� ���� �������� �̵�

        cameraTransform.position = playerHead.position;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0); // ���� �ռ����� ������ �ٲ㰡�鼭 �����θ� �̵�

        transform.rotation = Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0f); // ȸ���� �׻� ���� �������� ����
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
            //ī�޶� �÷��̾� �����ʿ��� ȸ���ϵ��� ����
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //ī�޶� �÷��̾��� �����ʿ��� ������ ��ġ�� �̵�.
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;
            //ī�޶� �÷��̾��� ��ġ�� ���󰡵��� ����
            cameraTransform.LookAt(transform.position + new Vector3(0f, thirdPersonOffset.y, 0f));
        }
        else
        { //�÷��̾ ���� ����
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            cameraTransform.position = playerLookObj.position + thirdPersonOffset + Quaternion.Euler(pitch, yaw, 0) * direction;
            cameraTransform.LookAt(playerLookObj.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
    }
}
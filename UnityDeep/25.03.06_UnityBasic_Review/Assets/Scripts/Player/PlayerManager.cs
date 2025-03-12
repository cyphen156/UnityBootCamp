using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;



public class PlayerManager : MonoBehaviour
{
    private float moveSpeed = 5.0f; //플레이어 이동 속도
    public float mouseSensitivity = 100.0f; // 마우스 감도
    public Transform cameraTransform; // 카메라의 Transform
    public CharacterController characterController;
    public Transform playerHead; //플레이어 머리 위치(1인칭 모드를 위해서)
    public float thirdPersonDistance = 3.0f; //3인칭 모드에서 플레이어와 카메라의 거리
    public Vector3 thirdPersonOffset = new Vector3(0f, 1.5f, 0f); //3인칭 모드에서 카메라 오프셋
    public Transform playerLookObj; //플레이어 시야 위치

    public float zoomeDistance = 1.0f; //카메라가 확대될 때의 거리(3인칭 모드에서 사용)
    public float zoomSpeed = 5.0f; // 확대축소가 되는 속도
    public float defaultFov = 60.0f; //기본 카메라 시야각
    public float zoomeFov = 30.0f; //확대 시 카메라 시야각(1인칭 모드에서 사용)

    private float currentDistance; //현재 카메라와의 거리(3인칭 모드)
    private float targetDistance; //목표 카메라 거리
    private float targetFov; //목표 FOV
    //private bool isZoomed = false; //확대 여부 확인
    private Coroutine zoomCoroutine; //코루틴을 사용하여 확대 축소 처리
    private Camera mainCamera; //카메라 컴포넌트

    private float pitch = 0.0f; //위아래 회전 값
    private float yaw = 0.0f; //좌우 회전 값
    private bool isFirstPerson = false; //1인칭 모드 여부
    private bool isRotaterAroundPlayer = false; //카메라가 플레이어 주위를 회전하는지 여부 

    //중력 관련 변수
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    private bool isGround;

    private Animator animator;
    private float horizontal;
    private float vertical;
    private bool IsRunning = false;
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    private bool IsAim = false;
    private bool IsFire = false;
    private bool IsRapidFire = false;


    private int animationSpeed = 1;
    private string currentAnimation;
    public AudioClip audioClipFire;
    public AudioClip audioClipRapidFire;

    private AudioSource audioSource;
    public AudioClip audioClipWalk;
    public AudioClip audioClipWeaponChange;
    public AudioClip resetAudio;
    public GameObject RifleM4Obj;

    // 충돌 처리시 사용할 변수
    public Vector3 startPosition;
    public Quaternion startRotation;

    public Transform aimTargetTransform;
    private float weaponMaxDistance = 100.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = thirdPersonDistance;
        targetDistance = thirdPersonDistance;
        targetFov = defaultFov;
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.fieldOfView = defaultFov;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        RifleM4Obj.SetActive(false);
        startPosition = transform.position;
        startRotation = transform.rotation;
        StartCoroutine(RapidFire());
    }

    void Update()
    {
        animator.speed = animationSpeed;
        
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animStateInfo.IsName(currentAnimation) && animStateInfo.normalizedTime >= 1.0f)
        // 현재 재생되고 있는 애니메이션의 이름
        {
            currentAnimation = "";
            animator.Play(currentAnimation);
        }
        MouseSet();

        CameraSet();

        PlayerMovement();

        AimSet();

        WeaponFire();

        Run();

        WeaponChange();

        AnimationSet();

        PickUp();
    }

    void MouseSet()
    {
        //마우스 입력을 받아 카메라와 플레이어 회전 처리
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
    }
    void CameraSet()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            Debug.Log(isFirstPerson ? "1인칭 모드" : "3인칭 모드");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isRotaterAroundPlayer = !isRotaterAroundPlayer;
            Debug.Log(isRotaterAroundPlayer ? "카메라가 주위를 회전합니다." : "플레이어가 시야에 따라서 회전합니다.");
        }
    }
    void PlayerMovement()
    {
        if (isFirstPerson)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
        }
    }
    void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(audioClipWeaponChange);
            animator.SetTrigger("IsWeaponChange");
            RifleM4Obj.SetActive(true);
        }
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
        }

        else
        {
            IsRunning = false;
        }
        moveSpeed = IsRunning ? runSpeed : walkSpeed;
    }

    void WeaponFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsAim)
            {
                weaponMaxDistance = 200.0f;
                IsFire = true;
                //animator.SetBool("IsFire", IsFire);
                animator.SetTrigger("Fire");

                Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, weaponMaxDistance))
                {
                    Debug.Log("Hit : " + hit.collider.gameObject.name);
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.6f);
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * weaponMaxDistance, Color.green, 0.6f);
                }
                audioSource.PlayOneShot(audioClipFire);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (IsAim)
            {
                IsRapidFire = true;                
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsFire = false;
            IsRapidFire = false;

            //animator.SetBool("IsFire", IsFire);
        }
    }
    void AimSet()
    {
        if (Input.GetMouseButtonDown(1))
        {
            IsAim = true;
            //animator.SetBool("IsAim", IsAim);
            animator.SetLayerWeight(1, 1);

            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }

            if (isFirstPerson)
            {
                SetTargetFOV(zoomeFov);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFov));
            }
            else
            {
                SetTargetDistance(zoomeDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            IsAim = false;
            //animator.SetBool("IsAim", IsAim);
            animator.SetLayerWeight(1, 0);
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }

            if (isFirstPerson)
            {
                SetTargetFOV(defaultFov);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFov));
            }
            else
            {
                SetTargetDistance(thirdPersonDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));
            }
        }

    }

    

    IEnumerator RapidFire()
    {
        while (true)
        {
            if (IsFire && IsRapidFire)
            {
                Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, weaponMaxDistance))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    Debug.Log("Hit : " + hitObject.name);
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.6f);
                    if (hitObject.layer == 6)
                    {
                        hitObject.GetComponent<ZombieManager>().OnHit();
                    }
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * weaponMaxDistance, Color.green, 0.6f);
                }
                audioSource.Stop();
                animator.SetTrigger("Fire");
                audioSource.PlayOneShot(audioClipRapidFire);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }

    
    
    void AnimationSet()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsRunning", IsRunning);
    }
    void FirstPersonMovement()
    {
        //if (!IsAim)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
            moveDirection.y = 0;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        cameraTransform.position = playerHead.position;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0);
    }

    void ThirdPersonMovement()
    {
        //if (!IsAim)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
        UpdateCameraPosition();
    }


    void UpdateCameraPosition()
    {
        if (isRotaterAroundPlayer)
        {
            //카메라가 플레이어 오른쪽에서 회전하도록 설정
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //카메라를 플레이어의 오른쪽에서 고정된 위치로 이동
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;

            //카메라가 플레이어의 위치를 따라가도록 설정
            cameraTransform.LookAt(transform.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
        else
        {
            //플레이어가 직접 회전하는 모드
            transform.rotation = Quaternion.Euler(0f, yaw, 0);
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            cameraTransform.position = playerLookObj.position + thirdPersonOffset + Quaternion.Euler(pitch, yaw, 0) * direction;
            cameraTransform.LookAt(playerLookObj.position + new Vector3(0, thirdPersonOffset.y, 0));
            // RayCasting
            UpdateAimTarget();
        }
    }


    public void SetTargetDistance(float distance)
    {
        targetDistance = distance;
    }

    public void SetTargetFOV(float fov)
    {
        targetFov = fov;
    }

    IEnumerator ZoomCamera(float targetDistance)
    {
        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f) //현재 거리에서 목표 거리로 부드럽게 이동
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        currentDistance = targetDistance; // 목표 거리에 도달한 후 값을 고정
    }

    IEnumerator ZoomFieldOfView(float targetFov)
    {
        while (Mathf.Abs(mainCamera.fieldOfView - targetFov) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.fieldOfView = targetFov;
    }

    void PlayWeaponChangeSound()
    {
        if (audioClipWeaponChange != null)
        {
            audioSource.PlayOneShot(audioClipWeaponChange);
        }
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetLayerWeight(1, 0);
            animator.SetTrigger("PickUp");
        }
    }
    public void ResetSequence()
    {
        // 집에 가라
        //StartCoroutine(PlayResetAudio());
        audioSource.PlayOneShot(resetAudio);
        characterController.Move(startPosition - transform.position);
        //transform.position = startPosition;
        transform.rotation = startRotation;
    }
    IEnumerator PlayResetAudio()
    {
        audioSource.PlayOneShot(resetAudio);
        yield return null;
    }

    /// <summary>
    /// RayCast 
    /// Camera To Target (Aimed Target)
    /// </summary>
    void UpdateAimTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        aimTargetTransform.position = ray.GetPoint(10.0f);
        //aimTargetTransform = ray.transform
        //Physics.Raycast(ray, aimTargetTransform, 1000f, 1111111);
    }
}
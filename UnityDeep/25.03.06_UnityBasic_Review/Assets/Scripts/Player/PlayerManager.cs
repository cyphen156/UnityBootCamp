using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Audio;



public class PlayerManager : MonoBehaviour
{
    private float moveSpeed = 5.0f; //�÷��̾� �̵� �ӵ�
    public float mouseSensitivity = 100.0f; // ���콺 ����
    public Transform cameraTransform; // ī�޶��� Transform
    public CharacterController characterController;
    public Transform playerHead; //�÷��̾� �Ӹ� ��ġ(1��Ī ��带 ���ؼ�)
    public float thirdPersonDistance = 3.0f; //3��Ī ��忡�� �÷��̾�� ī�޶��� �Ÿ�
    public Vector3 thirdPersonOffset = new Vector3(0f, 1.5f, 0f); //3��Ī ��忡�� ī�޶� ������
    public Transform playerLookObj; //�÷��̾� �þ� ��ġ

    public float zoomeDistance = 1.0f; //ī�޶� Ȯ��� ���� �Ÿ�(3��Ī ��忡�� ���)
    public float zoomSpeed = 5.0f; // Ȯ����Ұ� �Ǵ� �ӵ�
    public float defaultFov = 60.0f; //�⺻ ī�޶� �þ߰�
    public float zoomeFov = 30.0f; //Ȯ�� �� ī�޶� �þ߰�(1��Ī ��忡�� ���)

    private float currentDistance; //���� ī�޶���� �Ÿ�(3��Ī ���)
    private float targetDistance; //��ǥ ī�޶� �Ÿ�
    private float targetFov; //��ǥ FOV
    //private bool isZoomed = false; //Ȯ�� ���� Ȯ��
    private Coroutine zoomCoroutine; //�ڷ�ƾ�� ����Ͽ� Ȯ�� ��� ó��
    private Camera mainCamera; //ī�޶� ������Ʈ

    private float pitch = 0.0f; //���Ʒ� ȸ�� ��
    private float yaw = 0.0f; //�¿� ȸ�� ��
    private bool isFirstPerson = false; //1��Ī ��� ����
    private bool isRotaterAroundPlayer = false; //ī�޶� �÷��̾� ������ ȸ���ϴ��� ���� 

    //�߷� ���� ����
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

    //bool isGetM4Item = false;     // ����� �ڵ�
    public bool isUseWeapon = false;    // ����� �ڵ�
    public bool isGetWeaponItem = false;

    private int animationSpeed = 1;
    private string currentAnimation;
    public AudioClip audioClipFire;
    public AudioClip audioClipRapidFire;

    private AudioSource audioSource;
    public AudioClip audioClipWalk;
    public AudioClip audioClipWeaponChange;
    public AudioClip resetAudio;
    public GameObject RifleM4Obj;

    public MultiAimConstraint multiAimConstranint;
    public Vector3 boxSize = new Vector3(1.0f, 1.0f, 1.0f);
    public float castDistance = 5.0f;
    public LayerMask itemLayer;
    public Transform itemGetPos;


    // �浹 ó���� ����� ����
    public Vector3 startPosition;
    public Quaternion startRotation;

    public Transform aimTargetTransform;
    private float weaponMaxDistance = 100.0f;
    public GameObject crossHair;
    public HashSet<string> itemList;
    public LayerMask TargetLayerMask;
    public Transform EffectPos;
    public ParticleSystem gunFireEffect;    // m4Effect
    public ParticleSystem AttackParticle;    
    public AudioClip audioClipHit;

    private float rifleFireDelay = 0.5f;

    void Start()
    {
        itemList = new HashSet<string>();
        itemList.Add("assault1");
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
        crossHair.SetActive(false);
    }

    void Update()
    {
        animator.speed = animationSpeed;
        
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animStateInfo.IsName(currentAnimation) && animStateInfo.normalizedTime >= 1.0f)
        // ���� ����ǰ� �ִ� �ִϸ��̼��� �̸�
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
        Operate();
        //DubugBox();
    }

    void MouseSet()
    {
        //���콺 �Է��� �޾� ī�޶�� �÷��̾� ȸ�� ó��
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
            Debug.Log(isFirstPerson ? "1��Ī ���" : "3��Ī ���");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isRotaterAroundPlayer = !isRotaterAroundPlayer;
            Debug.Log(isRotaterAroundPlayer ? "ī�޶� ������ ȸ���մϴ�." : "�÷��̾ �þ߿� ���� ȸ���մϴ�.");
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
        // ���� ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(audioClipWeaponChange);
            animator.SetTrigger("IsWeaponChange");
            RifleM4Obj.SetActive(true);
            isUseWeapon = true;
        }
        // ���� ���� ����
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.PlayOneShot(audioClipWeaponChange);
            animator.SetTrigger("IsWeaponChange");
            RifleM4Obj.SetActive(false);
            isUseWeapon = false;
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
                gunFireEffect.Play();
                //StartCoroutine(SetFireDelay(false));
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
    
    IEnumerator SetFireDelay(bool v)
    {
        yield return new WaitForSeconds(rifleFireDelay);
        IsFire = v;
    }

    void AimSet()
    {
        if (Input.GetMouseButtonDown(1) && isGetWeaponItem && isUseWeapon)// isGetM4Item
        {
            crossHair.SetActive(true);
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
            crossHair.SetActive(false);
            IsAim = false;
            multiAimConstranint.data.offset = new Vector3(0, 0, 0);
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

                //RaycastHit hit;
                RaycastHit[] hits;
                TargetLayerMask = 1 << 6;
                //if (Physics.Raycast(ray, out hit, weaponMaxDistance, TargetLayerMask))
                //// single RayCasting
                //{
                //    GameObject hitObject = hit.collider.gameObject;
                //    Debug.Log("Hit : " + hitObject.name);
                //    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.6f);
                //    if (hitObject.layer == 6)
                //    {
                //        hitObject.GetComponent<ZombieManager>().OnHit();
                //    }
                //}
                // Multi RayCasting
                hits = Physics.RaycastAll(ray, weaponMaxDistance, TargetLayerMask);
                if (hits.Length > 0)
                {
                    int cnt = 0;
                    Vector3 endPoint = ray.origin + ray.direction * weaponMaxDistance;
                    Debug.DrawLine(ray.origin, endPoint, Color.blue, 0.6f);

                    foreach (RaycastHit hit1 in hits)
                    {
                        if (cnt < 2)
                        {
                            GameObject hitObject = hit1.collider.gameObject;
                            Debug.Log("Hit : " + hitObject.name);
                            Debug.DrawLine(ray.origin, hit1.point, Color.red, 0.6f);
                            if ((TargetLayerMask & (1 << hitObject.layer)) != 0)
                            {
                                Debug.Log("OnHit1 called");
                                ParticleSystem particle = Instantiate(AttackParticle, hit1.point, Quaternion.identity);
                                AttackParticle.Play();
                                audioSource.PlayOneShot(audioClipHit);
                                hitObject.GetComponent<HPController>().OnHit();
                            }
                        }
                        cnt++;
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
            //ī�޶� �÷��̾� �����ʿ��� ȸ���ϵ��� ����
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //ī�޶� �÷��̾��� �����ʿ��� ������ ��ġ�� �̵�
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;

            //ī�޶� �÷��̾��� ��ġ�� ���󰡵��� ����
            cameraTransform.LookAt(transform.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
        else
        {
            //�÷��̾ ���� ȸ���ϴ� ���
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
        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f) //���� �Ÿ����� ��ǥ �Ÿ��� �ε巴�� �̵�
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        currentDistance = targetDistance; // ��ǥ �Ÿ��� ������ �� ���� ����
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
        // ���� ����
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

    void Operate()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.E) && !stateInfo.IsName("PickUp"))
        {
            animator.SetTrigger("Operate");
            
            Vector3 origin = itemGetPos.position;
            Vector3 direction = itemGetPos.forward;
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(origin, boxSize / 2, direction, Quaternion.identity, castDistance, itemLayer);
            string name;
            foreach(RaycastHit hit in hits)
            {
                name = hit.collider.name;
                if (itemList.Contains(name))
                {
                    Debug.Log("�̹� �κ��丮�� ���� �Ѱ��� ������ �ٴϼ�");
                }
                else
                {
                    itemList.Add(name);
                    hit.collider.gameObject.SetActive(false);
                    isGetWeaponItem = true;
                }
                Debug.Log("Item : " + hit.collider.name);
            }
        }
    }
    void DubugBox(Vector3 origin, Vector3 direction)
    {
        Vector3 endPoint = origin + direction * castDistance;

        Vector3[] corners = new Vector3[8];
        corners[0] = origin + new Vector3(-boxSize.x, -boxSize.y, -boxSize.z) / 2;
        corners[1] = origin + new Vector3(boxSize.x, -boxSize.y, -boxSize.z) / 2;
        corners[2] = origin + new Vector3(-boxSize.x, boxSize.y, -boxSize.z) / 2;
        corners[3] = origin + new Vector3(boxSize.x, boxSize.y, -boxSize.z) / 2;
        corners[4] = origin + new Vector3(-boxSize.x, -boxSize.y, -boxSize.z) / 2;
        corners[5] = origin + new Vector3(boxSize.x, -boxSize.y, boxSize.z) / 2;
        corners[6] = origin + new Vector3(-boxSize.x, boxSize.y, boxSize.z) / 2;
        corners[7] = origin + new Vector3(boxSize.x, -boxSize.y, boxSize.z) / 2;

        Debug.DrawLine(corners[0], corners[1], Color.green, 3f);
        Debug.DrawLine(corners[1], corners[3], Color.green, 3f);
        Debug.DrawLine(corners[3], corners[2], Color.green, 3f);
        Debug.DrawLine(corners[2], corners[0], Color.green, 3f);
        Debug.DrawLine(corners[4], corners[5], Color.green, 3f);
        Debug.DrawLine(corners[5], corners[7], Color.green, 3f);
        Debug.DrawLine(corners[7], corners[6], Color.green, 3f);
        Debug.DrawLine(corners[6], corners[4], Color.green, 3f);
        Debug.DrawLine(corners[0], corners[4], Color.green, 3f);
        Debug.DrawLine(corners[1], corners[5], Color.green, 3f);
        Debug.DrawLine(corners[2], corners[6], Color.green, 3f);
        Debug.DrawLine(corners[3], corners[7], Color.green, 3f);
        Debug.DrawLine(origin, direction * castDistance, Color.green);


        Debug.DrawLine(corners[0], corners[1], Color.green, 3f);
        Debug.DrawLine(corners[0], corners[1], Color.green, 3f);
        Debug.DrawLine(corners[0], corners[1], Color.green, 3f);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Audio;
using UnityEngine.UI;

using static UnityEngine.Random;
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

    //bool isGetM4Item = false;     // 강사님 코드
    public bool isUseWeapon = false;    // 강사님 코드
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


    // 충돌 처리시 사용할 변수
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
    public GameObject flashLight;
    public bool isFlashLightOn = false;
    public AudioClip audioClipFlashLight;
    /// <summary>
    ///  UIControll
    /// </summary>
    public Text bulletText;
    private int maxBulletMagazine = 30;
    private int fireBulletCount = 30;
    private int saveBulletCount = 120;
    private bool isPaused = false;
    public GameObject pauseObj;


    // 총기반동
    private int shotGunRayCount = 5;
    private float shotGunSpreadAngle = 10.0f;
    private float recoilStrength = 2.0f;
    private float maxRecoilAngle = 10.0f;
    private float currentRecoil = 0.0f;
    private float shakeDuration = 0.1f;
    private float shakeMagnitude = 0.1f;
    private Vector3 originalCameraPosition;
    private Coroutine cameraShakeCoroutine;


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
        pauseObj.SetActive(false);
        bulletText.gameObject.SetActive(false);
    }

    void Update()
    {
        PauseMenu();
        if (!isPaused)
        {
            animator.speed = animationSpeed;
        
            AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (animStateInfo.IsName(currentAnimation) && animStateInfo.normalizedTime >= 1.0f)
            // 현재 재생되고 있는 애니메이션의 이름
            {
                currentAnimation = "";
                animator.Play(currentAnimation);
            }

            if (currentRecoil > 0)
            {
                currentRecoil -= recoilStrength * Time.deltaTime;
                currentRecoil = Mathf.Clamp(currentRecoil, 0.0f, maxRecoilAngle);

                Quaternion currentRotation = Camera.main.transform.rotation;
                Quaternion recoilRotation = Quaternion.Euler(-currentRecoil, 0, 0);
                Camera.main.transform.rotation = currentRotation * recoilRotation;
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
            FillAmmor();
            ActionFlashLight();

        }
        //DubugBox();
    }

    IEnumerator CameraShake(float shakeDuration, float shakeMagnitude)
    {
        float elapsed = 0.0f;
        Transform originTransform =  Camera.main.transform;
        while (elapsed < shakeDuration)
        {
            float offsetX = UnityEngine.Random.Range(-1.0f, 1.0f) * shakeMagnitude;
            float offsetY = UnityEngine.Random.Range(-1.0f, 1.0f) * shakeMagnitude;
            
            Camera.main.transform.position = originTransform.position + new Vector3(offsetX, offsetY, 0.0f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originTransform.position;
        Camera.main.transform.rotation = originTransform.rotation;

    }
    void StartCameraShake()
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
        }
        cameraShakeCoroutine = StartCoroutine(CameraShake(shakeDuration, shakeMagnitude));
    }
    void ApplyRecoil()
    {
        Quaternion currentRotation = Camera.main.transform.rotation;

        Quaternion recoilRotation = Quaternion.Euler(-currentRecoil, 0, 0);

        Camera.main.transform.rotation = currentRotation * recoilRotation;
        currentRecoil += recoilStrength;
        currentRecoil = Mathf.Clamp(currentRecoil, 0, maxRecoilAngle);
    }
    private void FireShotGun()
    {
        for (int i = 0; i < shotGunRayCount; i++)
        {
            RaycastHit hit;

            Vector3 origin = Camera.main.transform.position;
            Vector3 spreadDirection = GetSpreadDirection(Camera.main.transform.forward, shotGunSpreadAngle);

            Debug.DrawRay(origin, spreadDirection * castDistance, Color.green, 0.6f);

            if (Physics.Raycast(origin, spreadDirection, out hit, castDistance, TargetLayerMask))
            {
                Debug.Log("Hit");
            }

        }
    }

    private Vector3 GetSpreadDirection(Vector3 forwardDirection, float spreadAngle)
    {
        float spreadX = UnityEngine.Random.Range(-spreadAngle, spreadAngle);
        float spreadY = UnityEngine.Random.Range(-spreadAngle, spreadAngle);
        Vector3 spreadDirection = Quaternion.Euler(spreadX, spreadY, 0) * forwardDirection;
        return spreadDirection;
    }
    private void PauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                // true
                Pause();
            }
            else
            {
                // false
                Regame();
            }
        }
    }
    void Regame()
    {
        isPaused = false;
        pauseObj.SetActive(isPaused);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void Pause()
    {
        pauseObj.SetActive(isPaused);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    private void ActionFlashLight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SoundManager.Instance.PlaySfx("audioClipFlashLight", flashLight.transform.position);
            isFlashLightOn = !isFlashLightOn;
            flashLight.gameObject.SetActive(isFlashLightOn);
        }
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
        // 무기 장착
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlaySfx("audioClipWeaponChange", transform.position);
            //audioSource.PlayOneShot(audioClipWeaponChange);
            animator.SetTrigger("IsWeaponChange");
            RifleM4Obj.SetActive(true);
            isUseWeapon = true;
            bulletText.gameObject.SetActive(true);

        }
        // 무기 장착 해제
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlaySfx("audioClipWeaponChange", transform.position);
            //audioSource.PlayOneShot(audioClipWeaponChange);
            animator.SetTrigger("IsWeaponChange");
            RifleM4Obj.SetActive(false);
            isUseWeapon = false;
            bulletText.gameObject.SetActive(false);
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
            if (IsAim && BulletCount())
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
                SoundManager.Instance.PlaySfx("audioClipFire", gunFireEffect.transform.position);
                //audioSource.PlayOneShot(audioClipFire);
                ParticleManager.GetInstance().ParticlePlay(ParticleType.Fire, gunFireEffect.transform.position); 
                //gunFireEffect.Play();
                //StartCoroutine(SetFireDelay(false));
            }
        }
        else if (Input.GetMouseButton(0))
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

    private bool BulletCount()
    {
        if (fireBulletCount <= 0)
        {
            // 탄창에 총알이 없으면 못쏜다.
            // 사운드 효과 추가하기
            return false;
        }
        fireBulletCount--;
        SetAmmorText();
        return true;
    }

    private void SetAmmorText()
    {        
        bulletText.text = fireBulletCount.ToString() + " / " + saveBulletCount.ToString();
    }
    private void FillAmmor()
    {
        if (Input.GetKeyDown(KeyCode.R) && saveBulletCount > 0)
        {
            // 저장된 총알 수가 한 탄창보다 많니?
            if (saveBulletCount >= maxBulletMagazine)
            {
                saveBulletCount -= maxBulletMagazine;
                fireBulletCount = maxBulletMagazine;
            }
            // 저장된 총알 수가 한 탄창보다 적으면
            else
            {
                fireBulletCount = saveBulletCount;
                saveBulletCount = 0;
            }
            SetAmmorText();
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
            if (IsFire && IsRapidFire && BulletCount())
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
                                ParticleManager.GetInstance().ParticlePlay(ParticleType.Explosion, hit1.point);
                                //ParticleSystem particle = Instantiate(AttackParticle, hit1.point, Quaternion.identity);
                                //AttackParticle.Play();
                                SoundManager.Instance.PlaySfx("audioClipFire", gunFireEffect.transform.position);
                                if (hitObject.TryGetComponent(out HPController hpController))
                                {
                                    hpController.OnHit();
                                }
                            }
                        }
                        cnt++;
                    }
                }

                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * weaponMaxDistance, Color.green, 0.6f);
                }
                SoundManager.Instance.StopSFX();
                animator.SetTrigger("Fire");
                SoundManager.Instance.PlaySfx("audioClipFire", gunFireEffect.transform.position);
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
            SoundManager.Instance.PlaySfx("audioClipWeaponChange", transform.position);
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
        SoundManager.Instance.PlaySfx("resetAudio", transform.position);
        characterController.Move(startPosition - transform.position);
        //transform.position = startPosition;
        transform.rotation = startRotation;
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
                if (hit.collider.gameObject.tag == "Bullet")
                {// 총알 특수 처리
                    saveBulletCount += hit.collider.gameObject.GetComponent<BulletCount>().bulletCount;
                    SetAmmorText();
                }
                else if (hit.collider.gameObject.layer == 8)
                {
                    Debug.Log("플레이어 호출");
                    //hit.collider.gameObject.GetComponent<DoorCollisionController>().ForceDoorRotate();
                }
                else
                {// 탄창
                    name = hit.collider.name;
                    if (itemList.Contains(name))
                    {
                        Debug.Log("이미 인벤토리에 있음 한개만 가지고 다니센");
                    }
                    else
                    {
                        itemList.Add(name);
                        hit.collider.gameObject.SetActive(false);
                        isGetWeaponItem = true;
                    }
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

    
    public void Exit()
    {
        Debug.Log("Exit 눌림");
    }

    public void Return()
    {
        Regame();
    }
}
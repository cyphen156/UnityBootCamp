using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    /// <summary>
    /// Patrol,
    /// Chase,
    /// Attack,
    /// Evade,
    /// Damage,
    /// Die,
    /// Idle
    /// </summary>
    public enum  EZombieState
    {
        Patrol,     // 순찰
        Chase,      // 추적
        Attack,     // 공격
        Evade,      // 이런거 없음
        Damage,     // OnHit
        Die,        // 사망
        Goback,     // 놓치면

        Idle        // 가만히 서있기
    }

    // 이거 필요할까??

    // 애니메이터가 트리거를 통해 제어하고 있는데???
    public enum ZombieAnimation
    {
        ZombieWalk,
        ZombieRun,
        //ZombieScream,
        ZombieAttack,
        //ZombieBiting,
        ZombieEvade,
        ZombieDie,

        ZombieIdle
    }
    public EZombieState currentState = EZombieState.Idle;
    

    public float HP = 10f;
    public float currentTime = 0f;
    public GameObject hitEffect;
    public float attackRange = 1.0f;
    public Transform target;
    public float attackDelay = 2.0f;
    private float nextAttackTime = 0.0f;
    public Transform[] patrolPoints;    // 순찰 포인트
    private int currentPatrolPoints;  // 현재 패트롤 인덱스
    public float moveSpeed = 2.0f;
    public float trackingRange = 5.0f;
    private bool isAttack = false;
    private float evadeRange = 5.0f;
    private float distanceToTarget;
    private bool isWaiting = true;
    public float idleTime = 2.0f;
    public bool isPlayingAnimation = false;
    ZombieAnimation currentAnimation;
    Animator animator;
    public float rotationSpeed = 90f; // 회전 속도 (도/초)
    private Coroutine stateRoutine; // 코루틴 상태 저장용
    public GameObject handRight;
    private bool hasTurnedEnough = false;
    GameObject playerObject;
    public float zombieFov;

    private NavMeshAgent agent;
    void Start()
    {
        currentState = EZombieState.Idle;
        animator = GetComponent<Animator>();
        currentAnimation = ZombieAnimation.ZombieIdle;
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (HP <= 0)
        {
            ChangeCurrentState(EZombieState.Die);
            AIBehaviorControl();
            PlayAnimation();
            
            //Destroy(gameObject);
        }

        // 좀비가 죽지 않았다면 해야 할 일
        if (currentState != EZombieState.Die)
        {
            // 거리재기
            distanceToTarget = Vector3.Distance(transform.position, target.position);


            // 순찰 시작 루틴 선택활용
            // 
            // 조건
            // 1. 기본 상태에 진입 한 후 일정 시간이 지나거나
            if (currentTime >= idleTime)
            {
                ChangeCurrentState(EZombieState.Patrol);
            }
            if (distanceToTarget < trackingRange)
            // 플레이어가 추적 범위 안에 들어옴
            {
                ChangeCurrentState(EZombieState.Chase);

                if (distanceToTarget < attackRange && !isAttack)
                // 추적 범위 안에서도 공격 범위 안에 들어옴, 공격 할 때는 딜레이가 있음
                {
                    ChangeCurrentState(EZombieState.Attack);
                    // 공격
                    //StartCoroutine(Attack());
                }
                if (HP < 5)
                {
                    // 체력이 적으면 돔황챠!
                    ChangeCurrentState(EZombieState.Evade);
                }
            }
            else
            // 추적 범위 안에 플레이어가 없음
            // 자신의 원래 자리로 돌아간 뒤
            {
                ChangeCurrentState(EZombieState.Goback);
            }

            AIBehaviorControl();
            PlayAnimation();
        }
    }
    void ChangeCurrentState(EZombieState state)
    {
        if (currentState == state)
        {
            // 현재 상태 계속 유지하는 코드
            return;
        }

        // 상태가 바뀌면 해야 할 일을 작성하기
        currentState = state;
        
        //if (stateRoutine != null)
        //{
        //    StopCoroutine(stateRoutine);
        //}

        //AIBehaviorControl();
    }

    
    void AIBehaviorControl()
    {
        switch (currentState)
        {
            //default:
            case EZombieState.Patrol:
                //stateRoutine = StartCoroutine(Patrol());
                Patrol();
                break;
            case EZombieState.Chase:
                //stateRoutine = StartCoroutine(Patrol());
                Chase(target);
                break;
            case EZombieState.Evade:
                //stateRoutine = StartCoroutine(Patrol());
                Evade();
                break;
            case EZombieState.Die:
                //stateRoutine = StartCoroutine(Patrol());
                Die();
                break;
            case EZombieState.Attack:
                //stateRoutine = StartCoroutine(Patrol());
                Attack();
                break;

            case EZombieState.Goback:
                GoBack();
                break;

            default:    //  EzZombieState.Idle
                //stateRoutine = StartCoroutine(Patrol());
                Idle();
                break;
        }
    }
    void PlayAnimation()
    {
        //while (!isPlayingAnimation)
        //{
        //}
    }
    //IEnumerator Patrol()
    //{
    //    if (patrolPoints.Length > 0)
    //    {
    //        animator.Play("Idle");
    //        Debug.Log("순찰");
    //        Transform targetPoint = patrolPoints[currentPatrolPoints];

    //        while(currentState == EZombieState.Idle)
    //        {
    //            yield return null;
    //        }
    //    }
    //}
    void Patrol()
    {
        if (patrolPoints.Length > 0)
        {
            Debug.Log("순찰");
            Transform targetPoint = patrolPoints[currentPatrolPoints];
        }
    }

    void Chase(Transform target)
    {
        Debug.Log("추적");        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Evade()
    {
        Debug.Log("회피");
    }

    void Die()
    {
        Debug.Log("쥬그무ㅜ");
        animator.SetTrigger("Die");
        // 플레이어 그룹과의 충돌 무시
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), playerObject.GetComponent<Collider>(), true);
        // 몬스터 그룹과의 충돌 무시
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }
    void Attack()
    {
        Debug.Log("공격");
        //ActiveHand();
        animator.SetTrigger("Attack");
    }
    public void ActiveHand()
    {
        handRight.gameObject.SetActive(true);
    }
    public void DeActiveHand()
    {
        handRight.gameObject.SetActive(false);
    }
    void GoBack()
    {
        Debug.Log("집에 가자");
        Vector3 direction = (patrolPoints[currentPatrolPoints].position - transform.position).normalized;

        // 목표 지점과 현재 위치 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, patrolPoints[currentPatrolPoints].position);
        float rotationThreshold = distanceToTarget * 0.2f; // 목표 거리의 20%까지 회전 수행

        // 목표 방향을 바라보도록 회전 (목표 거리의 20% 내에 있을 때는 회전 X)
        if (distanceToTarget > rotationThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    void Idle()
    {
        Debug.Log("Default");

        // 이상 상태일 경우 일정 시간이 지나면 추적을 시작함
        if (currentTime >= idleTime)
        {
            currentTime = 0f;
            ChangeCurrentState(EZombieState.Chase);
        }
    }
    //IEnumerator Attack()
    //{
    //    yield return new WaitForSeconds(nextAttackTime);
    //    isAttack = false;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<MeshRenderer>().material.color = Color.red;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어랑 충돌함");
            GameObject player = other.gameObject;
            if (player)
            {
                //playerManager.Weapon
            }

            // 처음으로 돌아가라
            player.GetComponent<PlayerManager>().ResetSequence();
        }
    }
}

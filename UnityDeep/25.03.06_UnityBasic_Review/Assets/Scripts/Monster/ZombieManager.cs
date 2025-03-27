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
        Patrol,     // ����
        Chase,      // ����
        Attack,     // ����
        Evade,      // �̷��� ����
        Damage,     // OnHit
        Die,        // ���
        Goback,     // ��ġ��

        Idle        // ������ ���ֱ�
    }

    // �̰� �ʿ��ұ�??

    // �ִϸ����Ͱ� Ʈ���Ÿ� ���� �����ϰ� �ִµ�???
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
    public Transform[] patrolPoints;    // ���� ����Ʈ
    private int currentPatrolPoints;  // ���� ��Ʈ�� �ε���
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
    public float rotationSpeed = 90f; // ȸ�� �ӵ� (��/��)
    private Coroutine stateRoutine; // �ڷ�ƾ ���� �����
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

        // ���� ���� �ʾҴٸ� �ؾ� �� ��
        if (currentState != EZombieState.Die)
        {
            // �Ÿ����
            distanceToTarget = Vector3.Distance(transform.position, target.position);


            // ���� ���� ��ƾ ����Ȱ��
            // 
            // ����
            // 1. �⺻ ���¿� ���� �� �� ���� �ð��� �����ų�
            if (currentTime >= idleTime)
            {
                ChangeCurrentState(EZombieState.Patrol);
            }
            if (distanceToTarget < trackingRange)
            // �÷��̾ ���� ���� �ȿ� ����
            {
                ChangeCurrentState(EZombieState.Chase);

                if (distanceToTarget < attackRange && !isAttack)
                // ���� ���� �ȿ����� ���� ���� �ȿ� ����, ���� �� ���� �����̰� ����
                {
                    ChangeCurrentState(EZombieState.Attack);
                    // ����
                    //StartCoroutine(Attack());
                }
                if (HP < 5)
                {
                    // ü���� ������ ��Ȳí!
                    ChangeCurrentState(EZombieState.Evade);
                }
            }
            else
            // ���� ���� �ȿ� �÷��̾ ����
            // �ڽ��� ���� �ڸ��� ���ư� ��
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
            // ���� ���� ��� �����ϴ� �ڵ�
            return;
        }

        // ���°� �ٲ�� �ؾ� �� ���� �ۼ��ϱ�
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
    //        Debug.Log("����");
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
            Debug.Log("����");
            Transform targetPoint = patrolPoints[currentPatrolPoints];
        }
    }

    void Chase(Transform target)
    {
        Debug.Log("����");        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Evade()
    {
        Debug.Log("ȸ��");
    }

    void Die()
    {
        Debug.Log("��׹���");
        animator.SetTrigger("Die");
        // �÷��̾� �׷���� �浹 ����
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), playerObject.GetComponent<Collider>(), true);
        // ���� �׷���� �浹 ����
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }
    void Attack()
    {
        Debug.Log("����");
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
        Debug.Log("���� ����");
        Vector3 direction = (patrolPoints[currentPatrolPoints].position - transform.position).normalized;

        // ��ǥ ������ ���� ��ġ ������ �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, patrolPoints[currentPatrolPoints].position);
        float rotationThreshold = distanceToTarget * 0.2f; // ��ǥ �Ÿ��� 20%���� ȸ�� ����

        // ��ǥ ������ �ٶ󺸵��� ȸ�� (��ǥ �Ÿ��� 20% ���� ���� ���� ȸ�� X)
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

        // �̻� ������ ��� ���� �ð��� ������ ������ ������
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
            Debug.Log("�÷��̾�� �浹��");
            GameObject player = other.gameObject;
            if (player)
            {
                //playerManager.Weapon
            }

            // ó������ ���ư���
            player.GetComponent<PlayerManager>().ResetSequence();
        }
    }
}

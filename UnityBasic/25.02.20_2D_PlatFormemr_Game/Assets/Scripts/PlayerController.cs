using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 플레이어 애니메이션 
    /// 0 : Player_Idle,
    /// 1 : Player_Run,
    /// 2 : Player_Jump,
    /// 3 : Player_Clear,
    /// 4 : Player_GameOver
    /// </summary>
    public enum ANIME_STATE
    {
        Player_Idle,
        Player_Run,
        Player_Jump,
        Player_Clear,
        Player_GameOver
    }
    Animator animator;
    string currentAnim;
    string previousAnim;

    Rigidbody2D rb;

    float axisH;
    public float speed;
    public float jumpForce;
    public LayerMask layerMask;
    bool isJump;
    bool isGround;


    public static string state = "Playing";
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        axisH = 0.0f;
        speed = 3.0f;
        jumpForce = 5.0f;
        isJump = false;
        isGround = true;
        state = "Playing";
        animator = GetComponent<Animator>();
        currentAnim = Enum.GetName(typeof(ANIME_STATE), 0);
        previousAnim = currentAnim;
    }

    private void Update()
    {
        if (state != "Playing")
        {
            return;
        }
        axisH = Input.GetAxisRaw("Horizontal"); //  수평이동

        // 이미지 플립
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (state != "Playing")
        {
            return;
        }
        isGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), layerMask);
        // 지정한 두 점을 연결하는 가상의 선에 게임 오브젝트가 접촉하는지?
        if (isGround || (axisH != 0))
        {
            rb.linearVelocity = new Vector2(speed * axisH, rb.linearVelocityY);
        }

        if (isGround && isJump)
        {
            // 플레이어 점프 수치만큼 벡터 설계
            Vector2 jumpPW = new Vector2(0, jumpForce);
            rb.AddForce(jumpPW, ForceMode2D.Impulse);
            isJump = false;
        }

        if (isGround)
        {
            // 땅 위에 있고
            if (axisH == 0)
            {
                // 멈춰 있는 경우
                currentAnim = Enum.GetName(typeof(ANIME_STATE), 0);
            }
            else
            {
                // 움직이고 있는 경우
                currentAnim = Enum.GetName(typeof(ANIME_STATE), 1);
            }
        }
        else
        {
            // 공중에 있는 경우
            currentAnim = Enum.GetName(typeof(ANIME_STATE), 2);
        }

        if (currentAnim != previousAnim)
        {
            previousAnim = currentAnim;

            animator.Play(currentAnim);
        }
    }
    private void Jump()
    {
        isJump = true;

        if (isJump)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    //private void ChangeAnim(string AnimName)
    //{
    //    animator.Play(Enum.GetName(typeof(ANIME_STATE), AnimName));
    //}

    private void Goal()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 3));
        state = "GameClear";
        Debug.Log("1");
        GameStop();
    }
    private void GameOver()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 4));
        state = "GameOver";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    private void GameStop()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }
}

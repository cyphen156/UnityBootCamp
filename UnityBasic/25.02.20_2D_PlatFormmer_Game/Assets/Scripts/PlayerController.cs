using System;
using UnityEngine;
using UnityEngine.UI;

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

    public int HP;
    float axisH;
    public const float speed = 5.0f;
    public const float jumpForce = 5.0f;
    public float accelateForce;
    public LayerMask layerMask;
    bool isJump;
    bool isGround;
    bool isAccelerate;

    public Text HPText;
    public Text LevelText;
    public static string state = "Playing";
    private void Start()
    {
        HP = 5;
        rb = GetComponent<Rigidbody2D>();
        axisH = 0.0f;
        accelateForce = 1.5f;
        isJump = false;
        isGround = true;
        state = "Playing";
        animator = GetComponent<Animator>();
        currentAnim = Enum.GetName(typeof(ANIME_STATE), 0);
        previousAnim = currentAnim;
        isAccelerate = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isAccelerate = true;
        }
        else
        {
            isAccelerate = false;
        }
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

        HPText.text = "HP : " + HP.ToString();
        LevelText.text = "Stage Level : " + LevelManager.level.ToString();
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
            rb.linearVelocity = new Vector2(Speed() * axisH, rb.linearVelocityY);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damaged();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    private void Damaged()
    {
        Debug.Log("");
        HP--;
        if (HP <= 0)
        {
            GameOver();
        }
    }

    private void Goal()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 3));
        state = "GameClear";
        GameStop();
    }
    public void GameOver()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 4));
        state = "GameOver";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    public void GameStop()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

    public float Speed()
    {
        if (isAccelerate)
        {
            return speed * accelateForce;
        }
        return speed;
    }
}

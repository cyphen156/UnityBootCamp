using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
///  플레이어의 이동 구현
/// </summary>

// 해당 기능을 통해 이 스크립트를 컴포넌트로써 사용할 경우
// 적어놓은 컴포넌트가 필수적으로 요구됨
// if component already added == deny delete component
// if component added not yet == automatically add nessasary component

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private float x, y;
    public float speed;
    public float jumpVal;
    private Rigidbody2D rb;
    private bool isJump;
    //private bool isWall;
    private uint score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = 0f; // 초기값 설정
        y= 0f;
        speed = 3f;
        jumpVal = 8f;
        rb = GetComponent<Rigidbody2D>();
        //isWall = false;
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        
    }

    void Move()
    {
        x = Input.GetAxisRaw("Horizontal");

        Vector3 velocity = new Vector3(x * speed * Time.deltaTime, y, 0);

        transform.position += velocity;
    }

    void jump()
    {
        if (!isJump)
        {
            isJump = true;
            rb.AddForce(new Vector3(0, jumpVal, 0), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            score++;
            Debug.Log($"{score}");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (score <= 35)
            {
                Debug.Log("점수가 모자라요 코인을 더 모아오세요.");
            }
            else if (score < 40)
            {
                Debug.Log("잘했어요. 그런데 살짝 아쉽네요 (TT)");
            }
            else
            {
                Debug.Log("완벽해요! 집에 가세요.\n***********************\n");
                Destroy(collision.gameObject);
                
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    ActivateSpecificLayer(mainCamera, "end"); // "end" 레이어만 활성화
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
        }
    }

    void ActivateSpecificLayer(Camera camera, string layerName)
    {
        // 특정 레이어 이름으로 활성화
        int layer = LayerMask.NameToLayer(layerName);
        if (layer >= 0)
        {
            camera.cullingMask = 1 << layer; // 해당 레이어만 활성화
            Debug.Log($"Layer '{layerName}' 활성화 완료.");
        }
        else
        {
            Debug.LogError($"Layer '{layerName}'가 존재하지 않습니다.");
        }
    }
}

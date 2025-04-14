using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private PlayerMovement movement;
    private PlayerAttack attack;
    private PlayerHealth health;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isInvincible = false;
    public float invincibilityDuration = 1.0f;
    public float knockbackForce = 5.0f;
    private Rigidbody2D rb;
    public bool isKnockback = false;
    public float knockbackDuration = 0.2f;

    private Vector3 StartPlayerPos;
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartPlayerPos = transform.position;
    }

    void Update()
    {
        if (!isKnockback)
        {
            movement.HandleMovement();
        }

        if (Input.GetButtonDown("Fire1") && !isKnockback)
        {
            attack.PerformAttack();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ReGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SoundManager.Instance.PlaySFX(SFXType.ItemGet);
    }

    public void ReGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        SoundManager.Instance.PlaySFX(SFXType.ItemGet);
    }

    public void MenuOn()
    {
        SoundManager.Instance.PlaySFX(SFXType.ItemGet);
        SceneManager.LoadScene("Menu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoin(10);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("DeathZone"))
        {
            SoundManager.Instance.PlaySFX(SFXType.Hit);
            transform.position = StartPlayerPos;
        }
        else if (collision.CompareTag("Zombie"))
        {
            PlayerAttack playerAttack = GetComponent<PlayerAttack>();
            float shakeDuration = 0.1f;
            float shakeMagnitude = 0.3f;
            //StartCoroutine(playerAttack.Shake(shakeDuration, shakeMagnitude));

            if (!isInvincible)
            {
                SoundManager.Instance.PlaySFX(SFXType.Hit);
                StartCoroutine(Invincibility());
                Vector2 knockbackDirection = spriteRenderer.flipX ? Vector2.right : Vector2.left;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                animator.SetTrigger("Hit");
                StartCoroutine(KnockbackCoroutine());
            }
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        Time.timeScale = 0.8f;
        float elapsedTime = 0f;
        float blinkInterval = 0.2f;

        Color originalColor = spriteRenderer.color;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f);
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval * 2;
        }
        spriteRenderer.color = originalColor;
        isInvincible = false;
        Time.timeScale = 1.0f;
    }

    IEnumerator KnockbackCoroutine()
    {
        isKnockback = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockback = false;
    }
}
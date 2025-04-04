using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using Unity.Mathematics;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private Animator animator;
    public List<GameObject> attackObjList = new List<GameObject>();
    private bool isAttacking = false;

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    private Vector3 originalPos;

    [Header("애니메이션 상태 이름")]
    public string attackStateName = "Attack";

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();

        if (Camera.main != null)
        {
            originalPos = Camera.main.transform.localPosition;
        }
    }

    public void PerformAttack()
    {
        if (isAttacking)
        {
            return;
        }

        if (playerAnimation != null)
        {
            playerAnimation.TriggerAttack();
        }

        StartCoroutine(AttackCooldownByAimation());
    }

    private IEnumerator AttackCooldownByAimation()
    {
        isAttacking = true;
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
        SoundManager.Instance.PlaySFX(SFXType.PlayerAttack);
        ParticleManager.Instance.ParticlePlay(ParticleType.PlayerAttack, transform.position, new Vector3(5, 5, 5));
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(attackStateName))
        {
            float animationLength = stateInfo.length;
            yield return new WaitForSeconds(animationLength);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        isAttacking = false;
    }

    public void AttackStart()
    {
        bool isFacingLeft = GetComponent<SpriteRenderer>().flipX;

        if (isFacingLeft)
        {
            if (attackObjList.Count > 0)
            {
                attackObjList[0].SetActive(true);
            }
        }
        else
        {
            if (attackObjList.Count > 0)
            {
                attackObjList[1].SetActive(true);
            }
        }
    }

    public void AttackEnd()
    {
        bool isFacingLeft = GetComponent<SpriteRenderer>().flipX;

        if (isFacingLeft)
        {
            if (attackObjList.Count > 0)
            {
                attackObjList[0].SetActive(false);
            }
        }
        else
        {
            if (attackObjList.Count > 0)
            {
                attackObjList[1].SetActive(false);
            }
        }
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        if (Camera.main == null)
        {
            yield break;
        }

        Camera.main.GetComponent<CinemachineBrain>().enabled = false;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, originalPos.y + y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float playerHP;

    public List<Image> HPUIs;
    private int DamageLevel;

    public GameObject UIBackground;

    private void Awake()
    {
        playerHP = 100f;
        DamageLevel = 0;
        foreach (Image img in HPUIs)
        {
            Color c = img.color;
            c.a = 0f;
            img.color = c;
        }
    }

    private void Update()
    {
        SetPlayerHP(playerHP);
    }
    public void SetPlayerHP(float inPlayerHP)
    {
        playerHP = inPlayerHP;
        SetDamageLevel();
    }

    private void SetDamageLevel()
    {
        int newDamageLevel = Mathf.Clamp(4 - (int)(playerHP / 20f), 0, 5);

        if (newDamageLevel != DamageLevel)
        {
            DamageLevel = newDamageLevel;

            StopAllCoroutines();

            StartCoroutine(SetHPUI(2f));
        }
    }
    IEnumerator SetHPUI(float duration)
    {
        float currentTime = 0f;

        // 목표 Vignette 알파 
        float vignetteTargetAlpha = 0.2f * Mathf.Clamp(DamageLevel, 0, 4);
        float vignetteStartAlpha = HPUIs[0].color.a;

        // 시작 알파 저장
        float[] startAlphas = new float[HPUIs.Count];
        for (int i = 0; i < HPUIs.Count; i++)
        {
            startAlphas[i] = HPUIs[i].color.a;
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;

            for (int i = 0; i < HPUIs.Count; i++)
            {
                float target = 0f;

                if (i == 0)
                {
                    target = vignetteTargetAlpha;
                }
                else
                {
                    if (i <= DamageLevel)
                    {
                        target = 1f;
                    }
                }

                float newAlpha = Mathf.Lerp(startAlphas[i], target, t);
                Color c = HPUIs[i].color;
                c.a = newAlpha;
                HPUIs[i].color = c;
            }
            yield return null;
        }

        // 마지막 보정
        for (int i = 0; i < HPUIs.Count; i++)
        {
            float target = 0f;

            if (i == 0)
            {
                target = vignetteTargetAlpha;
            }
            else
            {
                if (i <= DamageLevel)
                {
                    target = 1f;
                }
            }

            Color c = HPUIs[i].color;
            c.a = target;
            HPUIs[i].color = c;
        }
    }
    private void OnEnable()
    {
        // 낮밤 이벤트 구독
        FindObjectOfType<DaySystem>().OnDayNightChanged += HandleDayNightChanged;
    }

    private void OnDisable()
    {
        FindObjectOfType<DaySystem>().OnDayNightChanged -= HandleDayNightChanged;
    }

    private void HandleDayNightChanged(bool isDay, float weight)
    {
        if (UIBackground != null)
        {
            UIBackground.SetActive(isDay);
        }
    }
}

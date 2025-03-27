using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float playerHP;

    public List<Image> HPUIs;
    public GameObject UIBackground;
    public List<Image> UIBackgrounds;
    private int DamageLevel;


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
        foreach (Image img in UIBackgrounds)
        {
            Color c = img.color;
            c.a = 0f;
            img.color = c;
        }
    }

    //private void Update()
    //{
    //    SetPlayerHP(playerHP);
    //}
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
        float vignetteTargetAlpha = 0.2f * Mathf.Clamp(DamageLevel, 0, 4);
        float vignetteStartAlpha = HPUIs[0].color.a;

        float[] startAlphas = new float[HPUIs.Count];
        for (int i = 0; i < HPUIs.Count; i++)
            startAlphas[i] = HPUIs[i].color.a;

        float[] bgStartAlphas = new float[UIBackgrounds.Count];
        for (int i = 0; i < UIBackgrounds.Count; i++)
            bgStartAlphas[i] = UIBackgrounds[i].color.a;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;

            for (int i = 0; i < HPUIs.Count; i++)
            {
                float target = 0f;
                if (i == 0)
                    target = vignetteTargetAlpha;
                else if (i <= DamageLevel)
                    target = 1f;

                Color c = HPUIs[i].color;
                c.a = Mathf.Lerp(startAlphas[i], target, t);
                HPUIs[i].color = c;
            }

            // 백그라운드는 최대 2단계까지 사용
            for (int i = 0; i < UIBackgrounds.Count && i < 2; i++)
            {
                float target = (DamageLevel >= i + 1) ? (0.3f + i * 0.2f) : 0f; // 예: 1단계=0.3, 2단계=0.5
                Color c = UIBackgrounds[i].color;
                c.a = Mathf.Lerp(bgStartAlphas[i], target, t);
                UIBackgrounds[i].color = c;
            }

            yield return null;
        }
    }

    private void OnEnable()
    {
        if (DaySystem.Instance != null)
            DaySystem.Instance.OnDayNightChanged += HandleDayNightChanged;
    }

    private void OnDisable()
    {
        if (DaySystem.Instance != null)
            DaySystem.Instance.OnDayNightChanged -= HandleDayNightChanged;
    }

    private void HandleDayNightChanged(bool isDay, float weight)
    {
        if (UIBackground != null)
        {
            UIBackground.SetActive(isDay);
        }
    }
}

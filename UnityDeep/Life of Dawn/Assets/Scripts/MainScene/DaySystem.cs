using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;



/// <summary>
/// 타임 브로드캐스트 이벤터
/// </summary>
public class DaySystem : MonoBehaviour
{
    public float timeSpeed;
    public float baseTime;
    public float setTime;
    public float weight;    // 가중치

    public float currentTime;
    public bool isDay;

    public GameObject sun;

    public event Action<bool, float> OnDayNightChanged;
    private void Awake()
    {
        currentTime = 0f;
        timeSpeed = 0.1f;
        baseTime = 60f; // 1분
        weight = 1.5f;
        isDay = true;
        setTime = baseTime;
    }

    private void Start()
    {
        StartCoroutine(DayCycle());
    }

    IEnumerator DayCycle()
    {
        while (true)
        {
            currentTime += timeSpeed;

            sun.transform.Rotate(new Vector3(0f, 360f / setTime * timeSpeed, 0f));

            if (currentTime >= setTime)
            {
                isDay = !isDay;
                currentTime = 0f;

                setTime = isDay ? baseTime : baseTime * weight;

                OnDayNightChanged?.Invoke(isDay, weight);
                SetFog();
                Debug.Log($"[DaySystem] {(isDay ? "낮" : "밤")}으로 전환됨");
            }
            yield return new WaitForSeconds(timeSpeed);
        }
    }

    void SetFog()
    {
        RenderSettings.fog = true;

        if (isDay)
        {
            RenderSettings.fogColor = new Color(0.8f, 0.8f, 0.9f);
            RenderSettings.fogDensity = 0.002f;
        }
        else
        {
            RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.1f);
            RenderSettings.fogDensity = 0.01f;
        }

        RenderSettings.fogStartDistance = 10f;
        RenderSettings.fogEndDistance = 100f;
        RenderSettings.fogMode = FogMode.Exponential;
    }

}
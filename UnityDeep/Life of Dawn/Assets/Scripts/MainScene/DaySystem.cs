using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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

    public event Action<bool, float> OnDayNightChanged;
    DirectionalLight dl;
    private void Awake()
    {
        timeSpeed = 0.05f;
        baseTime = 120f; // 2분
        weight = 1.5f;
        isDay = true;
        setTime = baseTime;
        dl = GetComponent<DirectionalLight>();
    }

    private void Start()
    {
        StartCoroutine(DayCycle());
    }

    IEnumerator DayCycle()
    {
        while (true)
        {
            float addTime = Time.deltaTime;
            // 밤에는 시간이 늦게옴
            if (isDay)
            {
                setTime = baseTime;
            }
            else
            { 
                setTime = baseTime * weight;
            }
            currentTime += addTime;
            
            if (currentTime >= setTime)
            {
                isDay = !isDay;
                currentTime = 0;
                OnDayNightChanged?.Invoke(isDay, weight);
                Debug.Log($"[DaySystem] {(isDay ? "낮" : "밤")}으로 전환됨");
            }

            // 시간계는 다른 상태와 상관 없이 항상 일정하게 흘러야 한다.
            yield return new WaitForSeconds(timeSpeed);
        }
    }
}

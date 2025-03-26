using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;



/// <summary>
/// 타임 브로드캐스트 이벤터, 스카이박스 제어
/// </summary>
public class DaySystem : MonoBehaviour
{
    public float timeSpeed;
    public float baseTime;
    public float setTime;
    public float dayCycle;
    public float baseBendingTime;
    public float bendingTime;
    public float weight;    // 가중치

    public float currentTime;
    public bool isDay;

    public GameObject sun;
    public GameObject sunObject;
    public GameObject moonObject;
    

    public static DaySystem Instance;


    public event Action<bool, float> OnDayNightChanged;
    private string[] skyPhases = { "Dawn", "Day", "Twilight", "Night" };
    private int currentPhaseIndex;
    private float[] phaseDurations = { 30f, 30f, 45f, 45f };
    private float[] phaseAngles = { 72f, 72f, 108f, 108f };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentTime = 0f;
        timeSpeed = 0.1f;
        baseTime = 30f;     // 30초
        dayCycle = 150f;    // 하루는 150초
        weight = 1.5f;
        baseBendingTime = 5f;
        bendingTime = baseBendingTime;
        isDay = true;
        setTime = baseTime;
        currentPhaseIndex = 0;
        //moonObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(DayCycle());
    }

    public DaySystem GetInstance()
    {
        return Instance;
    }

    IEnumerator DayCycle()
    {
        while (true)
        {
            currentTime += timeSpeed;

            // 조명, 광원 회전
            sun.transform.Rotate(new Vector3(0f, -phaseAngles[currentPhaseIndex] / phaseDurations[currentPhaseIndex] * timeSpeed, 0f));

            if (currentTime >= setTime)
            {
                currentTime = 0f;

                // 다음 단계로 이동
                currentPhaseIndex = (currentPhaseIndex + 1) % skyPhases.Length;
                bendingTime = baseBendingTime;
                if (isDay)
                {
                    bendingTime *= weight;
                }
                string currentPhase = skyPhases[currentPhaseIndex];

                if (currentPhase == "Twilight" || currentPhase == "Dawn")
                {
                    isDay = !isDay;
                    OnDayNightChanged?.Invoke(isDay, weight);
                    Debug.Log($"[DaySystem] {(isDay ? "주간" : "야간")}으로 전환됨");
                }
                EnviromentManager.Instance.BlendEnviroment(currentPhase, bendingTime);

                //sunObject.SetActive(isDay);
                //moonObject.SetActive(!isDay);
                setTime = isDay ? baseTime : baseTime * weight;

                //SetFog();
            }
            yield return new WaitForSeconds(timeSpeed);
        }
    }

    // Legacy --> EnvironmentManager에 위임
    //void SetFog()
    //{
    //    RenderSettings.fog = true;

    //    if (isDay)
    //    {
    //        RenderSettings.fogColor = new Color(0.8f, 0.8f, 0.9f);
    //        RenderSettings.fogDensity = 0.002f;
    //    }
    //    else
    //    {
    //        RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.1f);
    //        RenderSettings.fogDensity = 0.01f;
    //    }

    //    RenderSettings.fogStartDistance = 10f;
    //    RenderSettings.fogEndDistance = 100f;
    //    RenderSettings.fogMode = FogMode.Exponential;
    //}

}
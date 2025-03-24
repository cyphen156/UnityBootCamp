using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// Ÿ�� ��ε�ĳ��Ʈ �̺���
/// </summary>
public class DaySystem : MonoBehaviour
{
    public float timeSpeed;
    public float baseTime;
    public float setTime;
    public float weight;    // ����ġ

    public float currentTime;
    public bool isDay;

    public event Action<bool, float> OnDayNightChanged;
    DirectionalLight dl;
    private void Awake()
    {
        timeSpeed = 0.05f;
        baseTime = 120f; // 2��
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
            // �㿡�� �ð��� �ʰԿ�
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
                Debug.Log($"[DaySystem] {(isDay ? "��" : "��")}���� ��ȯ��");
            }

            // �ð���� �ٸ� ���¿� ��� ���� �׻� �����ϰ� �귯�� �Ѵ�.
            yield return new WaitForSeconds(timeSpeed);
        }
    }
}

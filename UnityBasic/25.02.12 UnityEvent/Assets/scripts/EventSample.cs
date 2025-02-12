using System;
using UnityEngine;

public class EventSample : MonoBehaviour
{
    public event EventHandler kill;

    int count = 0;

    public void OnKill()
    {
        CountPlus();
        if (count == 100)
        {
            count = 0;
            kill(this, EventArgs.Empty);
        }
    }

    public void CountPlus() => count++;

    public void CountPlus1() 
    {
        count++;
    }
}


public class UnityEventSample : MonoBehaviour
{

    // 1. 이벤트 정의
    EventSample sampleEvent = new EventSample();

    private void Start()
    {
            Debug.Log("포탈");
        // 2. 이벤트 등록
        sampleEvent.kill += new EventHandler(MonsterKill);

        for (int i = 0; i < 5; i++)
        {
            // 이벤트 진행을 위해 기능 진행
            sampleEvent.OnKill();
        }
    }

    // 이벤트가 발생했을 때 실행할 코드.
    public void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("포탈열엇다.");
    }
}
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

    // 1. �̺�Ʈ ����
    EventSample sampleEvent = new EventSample();

    private void Start()
    {
            Debug.Log("��Ż");
        // 2. �̺�Ʈ ���
        sampleEvent.kill += new EventHandler(MonsterKill);

        for (int i = 0; i < 5; i++)
        {
            // �̺�Ʈ ������ ���� ��� ����
            sampleEvent.OnKill();
        }
    }

    // �̺�Ʈ�� �߻����� �� ������ �ڵ�.
    public void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("��Ż������.");
    }
}
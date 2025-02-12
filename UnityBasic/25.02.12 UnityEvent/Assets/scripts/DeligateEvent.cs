using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static MeetEvent;

class MeetEvent
{
    public delegate void MeetEventHandler(string message);

    public event MeetEventHandler meethandelr;

    int count = 0;
    public void Meet()
    {
        meethandelr("³ª¶û ¹ä¸ÔÀ¸·¯ °¡Áö ¾ÊÀ»·¡?");
    }
}

public class DeligateEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Text messageUI;

    MeetEvent meetEvent = new MeetEvent();
    void Start()
    {

        meetEvent.meethandelr += EventMessage;
    }

    private void EventMessage(string message)
    {
        messageUI.text = message;
    }

    public void OnMeetButtenEnter()
    {
        meetEvent.Meet();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

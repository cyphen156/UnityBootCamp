using UnityEngine;

public class UsingObserver : MonoBehaviour
{
    delegate void NotifyHandler();
    NotifyHandler nH;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UObserver observer = new UObserver();
        UObserver2 observer2 = new UObserver2();

        nH += new NotifyHandler(observer.OnNotify);
        nH += observer2.OnNotify;
    }
}

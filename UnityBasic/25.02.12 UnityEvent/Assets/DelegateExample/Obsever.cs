using UnityEngine;

/// <summary>
/// 옵저버에 대한 관리, 활용을 진행하기 위한 인터페이스
/// </summary>
public interface ISubject
{
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);

    void Notify();
}

public abstract class Observer
{
    public abstract void OnNotify();
}


public class UObserver : Observer
{
    public override void OnNotify()
    {
        Debug.Log("Observer1");
    }
}

public class UObserver2 : Observer
{
    public override void OnNotify()
    {
        Debug.Log("Observer2");
    }
}

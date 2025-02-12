using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UsingObserver2 : MonoBehaviour, ISubject
{
    List<Observer> observers = new List<Observer>();
    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(Observer observer)
    {
        if (observers.Count > 0)
        { 
            observers.Remove(observer);
        }
    }
    public void Notify()
    {
        foreach(Observer observer in observers)
        {
            observer.OnNotify();
        }
    }


    private void Start()
    {
        UObserver observer1 = new UObserver();
        UObserver2 observer2 = new UObserver2();

        AddObserver(observer1);
        AddObserver(observer2);
    }

}

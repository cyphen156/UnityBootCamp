using UnityEngine;

public class Child : ParentClass
{
    void Start()
    {
        base.DoSomeThing();
        DoSomeThing();
    }

    void Update()
    {
        
    }

    protected override void DoSomeThing()
    {
        Debug.Log("Called By Child Class not using Parents DoSomething()");
    }
}

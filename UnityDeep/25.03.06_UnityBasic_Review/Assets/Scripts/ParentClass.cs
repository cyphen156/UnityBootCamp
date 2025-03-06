using UnityEngine;

public class ParentClass : MonoBehaviour
{
    
    void Start()
    {
        DoSomeThing();
    }

    void Update()
    {
        
    }
    
    protected virtual void DoSomeThing()
    {
        Debug.Log("Called By Parent Class");

    }
}

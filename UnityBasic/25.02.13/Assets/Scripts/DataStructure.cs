using System.Collections.Generic;
using UnityEngine;

public class DataStructure : MonoBehaviour
{

    [SerializeField]public Queue<string> stringQue = new Queue<string> ();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stringQue.Clear ();
        stringQue.Enqueue ("���� ���� �ʹ�");
        stringQue.Enqueue("��� �԰� �ʹ�");
        stringQue.Enqueue("� �ϱ� �ȴ�");
        stringQue.Enqueue("Ŀ�� ���� ����");

        foreach (string s in stringQue)
        {
            //Debug.Log(stringQue.Peek());
        }
        while (stringQue.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log(stringQue.Dequeue());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

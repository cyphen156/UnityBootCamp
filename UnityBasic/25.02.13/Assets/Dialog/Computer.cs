using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public TMP_InputField input;
    public TextScripts text_Scripts;
    public string playerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_Scripts.AddBehavior("����");
        text_Scripts.AddBehavior("����"); 
        text_Scripts.AddBehavior("��");
        string initscript = "���͸� �̰ܶ�!";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerText = input.text;
            string computer = text_Scripts.GetBehavior(Random.Range(0, 3));
            if ()
            {

            }   
            else if ()
            {

            }

            //Debug.Log("����");
            //���� ������ �÷��̽� Ȧ������ �Է¹��� �ؽ�Ʈ �������� 
            string inputText;
            string rtnText;
        }
    }
}

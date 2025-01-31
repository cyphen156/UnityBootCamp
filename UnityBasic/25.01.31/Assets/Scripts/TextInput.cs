using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public Text message;    // 타이핑할 텍스트
    public Text message2;    // 타이핑할 텍스트

    [SerializeField] [TextArea] private string content; // 출력할 내용
    [SerializeField] private float delay = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMessageButtonClick()
    {
        StartCoroutine(TypeingText());
    }

    IEnumerator TypeingText()
    {
        message.text = "";

        int typing_cnt = 0;

        while (typing_cnt != content.Length)
        {
            if (typing_cnt < content.Length)
            {
                message.text += content[typing_cnt].ToString();
                typing_cnt++;
            }
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delay);
    }

    public void ByTwo()
    {
        if (delay == 0.2f)
        {
            message.color = Color.blue;
            message2.text = "x5";
            delay = 0.5f;
        }
        else
        {
            message2.color = Color.red;
            message2.text = "x2 ";
            delay = 0.2f;
        }
    }
}

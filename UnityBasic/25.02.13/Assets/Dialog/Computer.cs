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
        text_Scripts.AddBehavior("가위");
        text_Scripts.AddBehavior("바위"); 
        text_Scripts.AddBehavior("보");
        string initscript = "몬스터를 이겨라!";
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

            //Debug.Log("엔터");
            //엔터 눌리면 플레이스 홀더에서 입력받은 텍스트 가져오센 
            string inputText;
            string rtnText;
        }
    }
}

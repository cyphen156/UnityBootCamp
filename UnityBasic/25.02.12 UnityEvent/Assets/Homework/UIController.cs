using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject SystemUI;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI SystemText;
    public TMP_InputField PlayerInput;
    public GameStartConfig gameStartConfig;
    public Queue<string> dialog = new Queue<string>();

    void Start()
    {
        string hp = HP.text;
        string coin = Coin.text;
        // 한번 스크립터블 써보기
        gameStartConfig.AddData("당신의 목표는 ");
        gameStartConfig.AddData("다가오는 몬스터들을 피해서 ");
        gameStartConfig.AddData("최대한 많은 코인을 수집하는것입니다.");
        gameStartConfig.AddData("ㅎㅇㅌ!");

        for (int i = 0; i < gameStartConfig.startDialog.Count; ++i)
        {
            dialog.Enqueue(gameStartConfig.startDialog[i]);
        }
        StartCoroutine(Print());

        // 실행시마다 누적되는 문제를 해결함 10초 뒤에 리스트 비움

    }

    private void Update()
    {
        //StartCoroutine(Print());
    }
    public void SetUIText(string text, string text2)
    {
        HP.text = text;
        Coin.text = text2;
    }

    IEnumerator Print()
    {
        StartCoroutine(ClearList());
        while (dialog.Count > 0)
        {
            string data = dialog.Dequeue();
            // 한문장 초기화
            SystemText.text = "";

            foreach (char text in data)
            {
                SystemText.text += text; 
                yield return new WaitForSeconds(0.15f); 
            }

            yield return new WaitForSeconds(1f);

        }
        SystemText.text = "";
        SystemUI.SetActive(false);
    }
    IEnumerator ClearList()
    {
        yield return new WaitForSeconds(10f);
        gameStartConfig.clearData();
    }
}

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
        // �ѹ� ��ũ���ͺ� �Ẹ��
        gameStartConfig.AddData("����� ��ǥ�� ");
        gameStartConfig.AddData("�ٰ����� ���͵��� ���ؼ� ");
        gameStartConfig.AddData("�ִ��� ���� ������ �����ϴ°��Դϴ�.");
        gameStartConfig.AddData("������!");

        for (int i = 0; i < gameStartConfig.startDialog.Count; ++i)
        {
            dialog.Enqueue(gameStartConfig.startDialog[i]);
        }
        StartCoroutine(Print());

        // ����ø��� �����Ǵ� ������ �ذ��� 10�� �ڿ� ����Ʈ ���

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
            // �ѹ��� �ʱ�ȭ
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

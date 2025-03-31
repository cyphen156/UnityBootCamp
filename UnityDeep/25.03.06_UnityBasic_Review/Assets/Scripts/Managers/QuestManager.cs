using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public bool isTutorial = false;
    public TMP_Text Q1;
    public float currentTime = 20f;
    public int RemainTargets = 14;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 현재 씬 이름 확인
        string currentScene = SceneManager.GetActiveScene().name;
        isTutorial = currentScene.ToLower().Contains("tutorial");
    }

    private void Update()
    {
        if (isTutorial)
        {
            if (RemainTargets <= 0)
            {
                currentTime -= Time.deltaTime;
                Q1.text = "메인 스테이지 진입까지" + (int)currentTime + "초";
                if (currentTime <= 0f)
                {
                    SystemUIManager.instance.LoadScene("MainScene");
                }
            }
        }
    }

    public void DeleteTarget()
    {
        RemainTargets--;
        Q1.text = "남은 타겟 수: " + RemainTargets + "개";
    }
}

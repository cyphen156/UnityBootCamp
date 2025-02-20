using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImg;
    public Sprite gameOverSprite;
    public Sprite gameClearSprite;

    public GameObject reStartButton;
    public GameObject nextButton;

    Image img;
    public GameObject panel;

    //TimeMGR
    public GameObject timeBar;
    public Text timeText;
    TimeController timeController;

    GameObject player;
    PlayerController playerController;
    Vector3 firstPlayerPosition;
    LevelManager levelManager;
    public void Start()
    {
        timeController = GetComponent<TimeController>();
        levelManager = GetComponent<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        firstPlayerPosition = player.transform.position;

        if (timeController != null)
        {
            if (timeController.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }
    }


    private void Update()
    {
        if (PlayerController.state == "GameClear")
        {
            mainImg.SetActive(true);
            panel.SetActive(true);

            reStartButton.GetComponent<Button>().interactable = true;
            mainImg.GetComponent<Image>().sprite = gameClearSprite;

            PlayerController.state = "End";

            if (timeController != null)
            {
                timeController.isTimeOver = true;
            }
            player.transform.position = firstPlayerPosition;

        }
        else if (PlayerController.state == "GameOver")
        {
            mainImg.SetActive(true);
            panel.SetActive(true);
            nextButton.GetComponent<Button>().interactable = false;
            mainImg.GetComponent<Image>().sprite = gameOverSprite;
            PlayerController.state = "End";

            if (timeController != null)
            {
                timeController.isTimeOver = true;
            }
            // 버튼 연결로
            Debug.Log("집에 가라");
        }
        else if (PlayerController.state == "Playing")
        {
            if (timeController != null)
            {
                if (timeController.gameTime >= 0.0f)
                {
                    int time = (int)timeController.displayTime;
                    string toString = time.ToString();
                    timeText.text = toString;

                    if (time <= 0)
                    {
                        playerController.GameOver();
                    }
                }

            }

            //게임 진행중일시 추가처리 구현
            panel.SetActive(false);
            mainImg.SetActive(false);

        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        Debug.Log("🔹 NextLevel() 실행됨");

        // 1️⃣ 레벨 증가
        if (levelManager == null)
        {
            Debug.LogError("❌ LevelManager가 존재하지 않습니다!");
            return;
        }

        levelManager.StageUP();
        Debug.Log("✅ 레벨 증가 완료. 현재 레벨: " + LevelManager.level);

        // 2️⃣ 플레이어 상태 초기화
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("❌ 플레이어를 찾을 수 없습니다!");
            return;
        }

        playerController = player.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("❌ playerController를 찾을 수 없습니다!");
            return;
        }

        PlayerController.state = "Playing";
        player.transform.position = firstPlayerPosition;
        Debug.Log("✅ 플레이어 상태 및 위치 초기화 완료");

        // 3️⃣ 플레이어 HP 초기화
        Debug.Log("before HP 설정");
        playerController.HP = 5;
        Debug.Log("after HP 설정");

        // 4️⃣ UI 초기화
        if (panel != null) panel.SetActive(false);
        if (mainImg != null) mainImg.SetActive(false);
        if (reStartButton != null) reStartButton.GetComponent<Button>().interactable = true;
        if (nextButton != null) nextButton.GetComponent<Button>().interactable = true;

        Debug.Log("✅ UI 초기화 완료");

        // 5️⃣ 타이머 초기화
        if (timeController != null)
        {
            timeController.gameTime = 60;
            timeController.displayTime = 60;
            timeController.isTimeOver = false;
            Debug.Log("✅ 타이머 초기화 완료");
        }

        // 6️⃣ 몬스터 및 장애물 초기화
        if (levelManager != null)
        {
            levelManager.LevelReset();
            levelManager.GenerateEnemy();
            Debug.Log("✅ 몬스터 및 장애물 초기화 완료");
        }
        else
        {
            Debug.LogError("❌ LevelManager가 존재하지 않음!");
        }
    }

}


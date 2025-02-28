using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI bestScoreUI;
    public TextMeshProUGUI currentScoreUI;
    public int bestScore;
    private int currentScore = 0;
    public float currentTime; 

    public void Start()
    {
        currentTime = 0;
        bestScore = PlayerPrefs.GetInt("Best Score");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 1)
        {
            currentTime--;
            AddScore(LevelManager.Instance.GetLevel());
        }

        currentScoreUI.text = $"현재점수 : {currentScore}";

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("Best Score", bestScore);
        }
        bestScoreUI.text = $"최고점수 : {bestScore}";
    }
    
    public void AddScore(int input)
    {
        currentScore += input;
    }
}

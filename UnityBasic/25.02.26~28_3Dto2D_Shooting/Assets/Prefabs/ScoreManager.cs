using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public TextMeshProUGUI bestScoreUI;
    public TextMeshProUGUI currentScoreUI;
    private int bestScore;
    private int currentScore;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best Score");
    }

    private void Update()
    {
        Score++;
    }
    public int Score
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreUI.text = $"�������� : {currentScore}";
            if(currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = $"�ְ����� : {bestScore}";
                PlayerPrefs.SetInt("Best Score", bestScore);
            }
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// 레벨 매니저가 해야 할 일
    /// 시간에 따른 레벨 관리
    /// 게임 템포가 느려지면 안된다 --> 레벨업 주기는 15초당 1업
    /// </summary>

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
            }
            return instance;
        }
    }

    [SerializeField] private int level;
    [SerializeField] private float currentTime;
    [SerializeField] private float levelIncreaseTime;

    public TextMeshProUGUI levelText;
    GameObject player;
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
        level = 1;
        currentTime = 0;
        levelIncreaseTime = 15.0f;
    }

    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForFixedUpdate();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player != null)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= levelIncreaseTime)
            {
                currentTime = 0;
                level++;
                levelText.text = $"Level {level}";
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }
}

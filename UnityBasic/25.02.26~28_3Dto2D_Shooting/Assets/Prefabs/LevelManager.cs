using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// ���� �Ŵ����� �ؾ� �� ��
    /// �ð��� ���� ���� ����
    /// ���� ������ �������� �ȵȴ� --> ������ �ֱ�� 15�ʴ� 1��
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

using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class ObjectGenerator : MonoBehaviour
{
    static int score;
    /// <summary>
    /// �� Ŭ������ ������Ʈ�� �����Ѵ�.
    /// ���콺 ��ư Ŭ���� ī�޶��� ��ũ�� ������ ���� ������Ʈ ���� ����
    /// ���⿡ ���� �߻��ϴ� ��� ȣ��.
    /// </summary>

    public GameObject gameObject;
    private GameObject target;
    private GameObject scoreText;
    private float speed;
    private string baseText;
    private TextMeshProUGUI tmpGUI;
    private Material material;
    private float degree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //objectGenerator = GameObject.Find("ObjectGenerator");
        // ���� �ִ� ������Ʈ�� ���� ������ ��� // FindWithTag
        // Ÿ�Կ� �´� ������Ʈ Ž�� = FindObjectwithType(typeOf(ObjevtGenartor)) 
        speed = 3000.0f;
        score = 0;
        scoreText = GameObject.Find("Score");
        target = GameObject.Find("target");
        tmpGUI = scoreText.GetComponent<TextMeshProUGUI>();
        degree = 0.0f;
        baseText = tmpGUI.text;
    }

    // Update is called once per frame
    void Update()
    {
        // 0�� ��ư�� ������ ���
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 objGeneratorPos = transform.position;
            GameObject instant = Instantiate(gameObject, objGeneratorPos, Quaternion.identity);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 cameraDir = ray.direction;

            //Debug.Log(cameraDir + " " + ray);

            instant.GetComponent<ObjectShooter>().Shoot(cameraDir.normalized * speed);
        }
    }
    private void ChangeMaterial()
    {
        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }
    private void SetScoreText(string baseText, int score)
    {
        tmpGUI.text = $"{baseText}{score}��";
    }
    public int AddScore()
    {
        score++;
        SetScoreText(baseText, score);

        if ((score % 10 == 0) && (score != 0))
        {
            degree += 90;
            if (degree >= 360)
            {
                degree = 0;
            }
            ChangeMaterial();
            //Debug.Log("before");
            target.GetComponent<TargetMover>().AddSpeed(10f);
            //target.GetComponent<TargetMover>().Move();
        }

        return score;
    }
}

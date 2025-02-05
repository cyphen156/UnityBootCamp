using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class ObjectGenerator : MonoBehaviour
{
    static int score;
    /// <summary>
    /// 이 클래스는 오브젝트를 생성한다.
    /// 마우스 버튼 클릭시 카메라의 스크린 지점을 통해 오브젝트 방향 설정
    /// 방향에 맞춰 발사하는 기능 호출.
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
        // 씬에 있는 오브젝트의 값을 얻어오는 기능 // FindWithTag
        // 타입에 맞는 오브젝트 탐색 = FindObjectwithType(typeOf(ObjevtGenartor)) 
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
        // 0번 버튼을 눌렀을 경우
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
        tmpGUI.text = $"{baseText}{score}점";
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

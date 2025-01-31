using UnityEngine;

public class CreateObject3 : MonoBehaviour
{
    [SerializeField]private GameObject prefeb;
    // [] :: 加己 // 瘤沥等 公攫啊
    private GameObject sample;

    void Start()
    {
        prefeb = Resources.Load<GameObject>("Prefebs/MYPrffeb002");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sample = Instantiate(prefeb);
            sample.AddComponent<VectorSample>();

            //Sprite sprite = Resources.Load<Sprite>("Sprites/sprite01");
        }
    }
}

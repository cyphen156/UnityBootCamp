using UnityEngine;

public class CreateObject3 : MonoBehaviour
{
    [SerializeField]private GameObject prefeb;
    // [] :: �Ӽ� // ������ ����
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

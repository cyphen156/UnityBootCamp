using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject gobj_prefeb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 인스턴스화
        Instantiate(gobj_prefeb);

        Instantiate(gobj_prefeb, new Vector3(5, -3, 0), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float randomPos;
    private float min = -8f, max = 8.1f;
    private float x;
    public float delay;
    public GameObject meteor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = transform.position.x;

        meteor = Resources.Load<GameObject>("Prefebs/meteor");
        delay = 0.8f;
        StartCoroutine(CreateMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

    }

    IEnumerator CreateMeteor()
    {
        while (true) 
        {
            randomPos = Random.Range(min, max);
            Instantiate(meteor, new Vector3(randomPos, 4, 0), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
}

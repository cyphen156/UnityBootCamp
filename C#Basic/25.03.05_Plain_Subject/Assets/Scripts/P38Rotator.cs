using UnityEngine;

public class P38Rotator : MonoBehaviour
{
    public float rotaionSpeed = 60.0f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(v, 0, -h).normalized * rotaionSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class CameraMove2 : MonoBehaviour
{
    Transform pleyerTransform;

    float positionLag = 3.0f;
    float rotationLag = 3.0f;

    void Awake()
    {
        pleyerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, pleyerTransform.position, positionLag * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, pleyerTransform.rotation, rotationLag * Time.deltaTime);
    }
}

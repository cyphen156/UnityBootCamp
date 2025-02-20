using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    public GameObject subScreeen;

    /// <summary>
    /// 강제 스크롤
    /// </summary>

    public bool isForceScrollX;
    public bool isForceScrollY;
    public float forceScrollSpeedX;
    public float forceScrollSpeedY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftLimit = 0f;
        rightLimit = 20.6f;
        topLimit = 0f;
        bottomLimit = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        float z = transform.position.z;


        if (x < leftLimit)
        {
            x = leftLimit;
        }
        else if (x > rightLimit)
        {
            x = rightLimit;
        }


        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if (y > topLimit)
        {
            y = topLimit;
        }

        Vector3 vec3 = new Vector3(x, y, z);
        Vector3 subVec3 = new Vector3(x * 0.8f, 0, 0);
        transform.position = vec3;
        subScreeen.transform.position = subVec3;
    }
}

using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    
    /// <summary>
    /// 발사 기능을 제공해주는 클래스
    /// 충돌시 오브젝트를 고정시키는 역할
    /// <param name="direction"물체의 발사 방향</param>>
    /// </summary>

    public void Shoot(Vector3 direction)
    {
        //Debug.Log("나가는 방향" + direction);

        //Debug.Log("회전 방향" + transform.rotation);
        GetComponent<Rigidbody>().AddForce(direction);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 발생시 호출되는 함수
        GetComponent<ParticleSystem>().Play();
        if (collision.gameObject.tag == "target")
        {
            //Debug.Log(collision.gameObject.tag);
            GameObject objectGenerator = GameObject.Find("objectGenerator");
            objectGenerator.GetComponent<ObjectGenerator>().AddScore();
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 0.5f);
        }
        if (collision.gameObject.tag == "terrain")
        {
            Destroy(gameObject, 1.0f);
        }
    }
}



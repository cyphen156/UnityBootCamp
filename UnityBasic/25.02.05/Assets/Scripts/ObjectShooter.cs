using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    
    /// <summary>
    /// �߻� ����� �������ִ� Ŭ����
    /// �浹�� ������Ʈ�� ������Ű�� ����
    /// <param name="direction"��ü�� �߻� ����</param>>
    /// </summary>

    public void Shoot(Vector3 direction)
    {
        //Debug.Log("������ ����" + direction);

        //Debug.Log("ȸ�� ����" + transform.rotation);
        GetComponent<Rigidbody>().AddForce(direction);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹 �߻��� ȣ��Ǵ� �Լ�
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



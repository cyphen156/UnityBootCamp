using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Transform A;
    public Transform B;
    float elapsedTime = 0.0f;
    float sign = 1f;

    public Color AColor;
    public Color BColor;
    public AnimationCurve curve;
    private void Awake()
    {
    }
    void Update()
    {
        elapsedTime += sign * Time.deltaTime;

        if (elapsedTime > 1 || elapsedTime < 0)
        {
            sign = -sign;
        }
        transform.position = Vector3.Lerp(A.position, B.position, curve.Evaluate(elapsedTime));
        transform.rotation = Quaternion.Slerp(A.rotation, B.rotation, curve.Evaluate(elapsedTime));
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.Lerp(AColor, BColor, curve.Evaluate(elapsedTime)));
    }
}

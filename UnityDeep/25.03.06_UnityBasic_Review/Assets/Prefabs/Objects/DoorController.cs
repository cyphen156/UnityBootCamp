using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float duration = 5f;
    public bool isOpen = false;
    private Coroutine doorCoroutine;

    public void RotateDoor()
    {
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        doorCoroutine = StartCoroutine(RotationDoor());
    }

    IEnumerator RotationDoor()
    {
        float currentTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -90f, 0);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;

            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation; // 정확히 최종값으로 설정
        isOpen = true;
    }
}

using Unity.Burst;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewInterface : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ŭ������");
    }

    // �̺�Ʈ �ý��� �ν��Ͻ��� �־�� �����ϴ� �̺�Ʈ�ý���
    /**
     * ���� �̺�Ʈ �ý��� ������Ʈ ��ġ
     * ������Ʈ�� �ݶ��̴�
     * ī�޶� ������ ����ĳ����
     */

}

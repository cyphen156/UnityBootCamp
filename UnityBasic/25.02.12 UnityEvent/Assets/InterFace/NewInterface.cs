using Unity.Burst;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewInterface : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭진행");
    }

    // 이벤트 시스템 인스턴스가 있어야 동작하는 이벤트시스템
    /**
     * 씬에 이벤트 시스템 오브젝트 배치
     * 오브젝트에 콜라이더
     * 카메라에 피직스 레이캐스터
     */

}

using UnityEngine;
// 마우스 버튼을 Unity애플리케이션에 전달한다.
public class GridPosition : MonoBehaviour
{
    [SerializeField]
    public int position_x;
    public int position_y;
    private void OnMouseDown()
    {
        //Debug.Log($"{transform.name} is Clicked");
        //Debug.Log($"current MouseX : {transform.position.x}\ncurrent MouseY : {transform.position.y}");
        //Debug.Log("info");
        //Logger.Info("Info");
        //Debug.LogWarning("warnning");
        //Logger.Warnning("warnning");
        //Debug.LogError("error");
        //Logger.Error("error");
        //Debug.LogAssertion("logAssert");
        GameManager.instance.ProcessInput(position_x, position_y);
    }
}

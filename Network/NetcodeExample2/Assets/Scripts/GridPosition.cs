using UnityEngine;
// ���콺 ��ư�� Unity���ø����̼ǿ� �����Ѵ�.
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

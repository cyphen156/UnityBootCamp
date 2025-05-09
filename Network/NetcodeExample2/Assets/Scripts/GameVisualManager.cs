using UnityEditor.Search;
using UnityEngine;

/// <summary>
/// 보드의 상태를 앱에 출력한다.
/// </summary>
public class GameVisualManager : MonoBehaviour
{
    [SerializeField]
    private GameObject crossPrefab;
    [SerializeField]
    private GameObject circlePrefab;
    
    private GameObject go;

    private void Start()
    {
        GameManager.instance.OnBoardChanged += GenerateObject;
    }
    public void GenerateObject(BoardState boardState, int y, int x)
    {
        switch(boardState)
        {
            case BoardState.None:
                go = null;
                break;

            case BoardState.Cross:
                go = crossPrefab;
                break;

            case BoardState.Circle:
                go = circlePrefab;
                break;

            default:
                Logger.Error("그런 상태는 없단다.");
                break;
        }
        Vector2 wldPosition = GetPosition(x, y);

        GameObject newgo = Instantiate(go, wldPosition, Quaternion.identity);
        GameManager.instance.AddGameObject(newgo);

        go = null;
    }

    private Vector2 GetPosition(int x, int y)
    {
        // 0 ~ 2 까지의 인덱싱에서 
        // -1 ~ 1까지로 변환하기
        y--;
        x--;

        // 실제 좌표로 변환 
        y *= -3;
        x *= 3;

        Vector2 position = new Vector2(x, y);

        return position;
    }
}

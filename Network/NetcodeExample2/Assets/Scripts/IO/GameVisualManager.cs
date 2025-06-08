using UnityEditor.Search;
using UnityEngine;

/// <summary>
/// ������ ���¸� �ۿ� ����Ѵ�.
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
                Logger.Error("�׷� ���´� ���ܴ�.");
                break;
        }
        Vector2 wldPosition = GetPosition(x, y);

        GameObject newgo = Instantiate(go, wldPosition, Quaternion.identity);
        GameManager.instance.AddGameObject(newgo);

        go = null;
    }

    private Vector2 GetPosition(int x, int y)
    {
        // 0 ~ 2 ������ �ε��̿��� 
        // -1 ~ 1������ ��ȯ�ϱ�
        y--;
        x--;

        // ���� ��ǥ�� ��ȯ 
        y *= -3;
        x *= 3;

        Vector2 position = new Vector2(x, y);

        return position;
    }
}

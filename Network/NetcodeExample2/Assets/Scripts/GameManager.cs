using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum BoardState
{
    Cross,
    Circle,

    None
}

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    
    [SerializeField]
    private BoardState[, ] board;
    private BoardState currentTurn;
    private int inputCount;
    public event Action<BoardState, int, int> OnBoardChanged;
    private List<GameObject> gameObjects;

    private bool isReset;
    private bool isGameEnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        board = new BoardState[3, 3];
        gameObjects = new List<GameObject>();
        StartCoroutine(ResetGame());
    }
    
    public GameManager GetInstance()
    {
        return instance;
    }

    public void ProcessInput(int x, int y)
    {
        if (isReset)
        {
            return;
        }

        if (board[y, x] != BoardState.None)
        {
            Logger.Warning(" 거기 이미 체크 되어있으니가 다른데 눌러라");
            return;
        }

        inputCount++;

        // 진짜로 보드 스테이트를 바꿔줌
        board[y, x] = currentTurn;
        Logger.Info($"x : {x}, y : {y} is marked {currentTurn}");

        OnBoardChanged?.Invoke(currentTurn, y, x);

        if (inputCount >= 5)
        {
            CheckVictory();
        }

        // 다음 입력을 위해 CurrentTurn 바꿔주기
        if (currentTurn == BoardState.Cross)
        {
            currentTurn = BoardState.Circle;
        }
        else // circle
        {
            currentTurn = BoardState.Cross;
        }
    }

    private void CheckVictory()
    {
        BoardState winner = BoardState.None;
        bool isGameOver = false;

        int[] dx = { -1, 1, 0, 0, -1, 1, -1, 1 };
        int[] dy = { 0, 0, -1, 1, -1, 1, 1, -1 };

        for (int y = 0; y < board.GetLength(0) && !isGameOver; ++y)
        {
            for (int x = 0; x < board.GetLength(1) && !isGameOver; ++x)
            {
                BoardState current = board[y, x];
                if (current == BoardState.None) continue;

                for (int dir = 0; dir < 8; dir += 2)
                {
                    int count = 1;

                    // 정방향
                    int nx = x + dx[dir];
                    int ny = y + dy[dir];
                    if (InBoard(ny, nx) && board[ny, nx] == current)
                    {
                        count++;
                    }

                    // 역방향
                    int opp = dir + 1;
                    nx = x + dx[opp];
                    ny = y + dy[opp];
                    if (InBoard(ny, nx) && board[ny, nx] == current)
                    {
                        count++;
                    }

                    if (count >= 3)
                    {
                        winner = current;
                        isGameOver = true;
                        break;
                    }
                }
            }
        }

        if (!isGameOver && inputCount < 9)
        {
            return; // 게임 지속
        }

        if (winner == BoardState.None)
        {
            Logger.Info("Game Result :: Tie...\nReset Game!");
        }
        else
        {
            Logger.Info($"Game Result :: Winner is {winner}");
        }

        StartCoroutine(ResetGame());
    }

    private bool InBoard(int y, int x)
    {
        return y >= 0 && y < 3 && x >= 0 && x < 3;
    }

    public void AddGameObject(GameObject go)
    {
        gameObjects.Add(go);
    }

    IEnumerator ResetGame()
    {
        isReset = true;
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < board.GetLength(0); ++i)
        {
            for (int j = 0; j < board.GetLength(1); ++j)
            {
                board[i, j] = BoardState.None;
            }
        }
        if (gameObjects.Count != 0)
        {
            foreach (GameObject go in gameObjects)
            {
                Destroy(go);
            }
        }

        gameObjects.Clear();
        currentTurn = BoardState.Cross;
        inputCount = 0;

        isReset = false;
    }
}

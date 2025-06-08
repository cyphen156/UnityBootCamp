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
    private NetworkVariable<BoardState> currentTurn = new NetworkVariable<BoardState>();

    [SerializeField]
    private BoardState localState;
    private int inputCount;
    public event Action<BoardState, int, int> OnBoardChanged;
    public event Action<BoardState> OnTurnChanged;
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
        localState = BoardState.None;
        StartCoroutine(ResetGame());
    }

    private void Start()
    {
        NetworkManager.Singleton.OnConnectionEvent += (networkManager, ConnectionEventData) =>
        {
            Logger.Info("\n" + networkManager + "Client Connected : " + $"{ConnectionEventData.ClientId} {ConnectionEventData.EventType}");

            if (NetworkManager.ConnectedClients.Count == 2)
            {
                //접속한 애가 2명이면 게임시작했다
                if (IsHost)
                {
                    localState = BoardState.Cross;
                    currentTurn.Value = BoardState.Cross;
                }
                else
                {
                    localState = BoardState.Circle;
                }

                Logger.Info("\n\n" + localState.ToString() + "\n\n");
            }
        };
    }
    public GameManager GetInstance()
    {
        return instance;
    }

    public void ProcessInput(int x, int y)
    {
        // 요청 유효 처리받기
        RequestValidateRpc(x, y, localState);
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
        currentTurn.Value = BoardState.Cross;
        inputCount = 0;

        isReset = false;
    }

    [Rpc(SendTo.Server)]
    public void RequestValidateRpc(int x, int y, BoardState localState)
    {
        if (currentTurn.Value == localState)
        {
            Logger.Info("Input is valid");
            ChangedBoardStateRpc(x, y, localState);
        }

        else
        {
            Logger.Warning("Invalid Input");
        }
    }

    [Rpc(SendTo.Everyone)]
    public void ChangedBoardStateRpc(int x, int y, BoardState localState)
    {
        board[x, y] = localState;
        OnBoardChanged?.Invoke(localState, y, x);
        
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
        board[y, x] = currentTurn.Value;
        Logger.Info($"x : {x}, y : {y} is marked {currentTurn}");

        if (inputCount >= 5)
        {
            CheckVictory();
        }

        //****Local Play Only State Change    ****//
        //// 다음 입력을 위해 CurrentTurn 바꿔주기
        //if (currentTurn == BoardState.Cross)
        //{
        //    currentTurn = BoardState.Circle;
        //}
        //else // circle
        //{
        //    currentTurn = BoardState.Cross;
        //}

        //****     Multi Play Only State Change    ****//
        // 턴바꾸기

        if (localState == BoardState.Cross)
        {
            currentTurn.Value = BoardState.Circle;
        }
        else
        {
            currentTurn.Value = BoardState.Cross;
        }
    }
}

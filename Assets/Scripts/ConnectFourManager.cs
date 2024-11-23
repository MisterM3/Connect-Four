using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFourManager : Singleton<ConnectFourManager>
{

    [SerializeField] [Range(1, 15)] private int _rows = 6;
    [SerializeField] [Range(1, 15)] private int _columns = 7;
    [SerializeField] [Range(3, 10)] private int amountToWin = 4;

    private Vector2Int _boardDimensions;
    public Vector2Int BoardDimensions
    {
        get 
        {
            if (_boardDimensions == null)
                _boardDimensions = new Vector2Int(_rows, _columns);

            return _boardDimensions;
        }
    }

    public ConnectFourBoard ConnectFourBoard { get; private set; }

    public EventHandler<Player> onPlayerWon;
    public EventHandler<Vector2Int> onBoardReset;
    public Action<Vector2Int, Player> onDiskAdded;

    Player playerTurn;

    [SerializeField] private ConnectFourSolver _solver;

    private void Awake()
    {
        Debug.Log(1);
        InitializeSingleton(this);
        
        if (_solver == null)
        {
            Debug.LogWarning("Solver is not set for connectFourManager, add a solver!");
            return;
        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartGame();
    }

    public void PlayTurn(int rowIndex)
    {
        if (!ConnectFourBoard.TryAddDisk(rowIndex, playerTurn, out int column))
        {
            //Even though no disk is added, the turn is still over for the player
            NextTurn();
            return;
        }

        onDiskAdded?.Invoke(new Vector2Int(rowIndex, column), playerTurn);

        if (_solver.IsWinningMove(rowIndex, column, playerTurn))
        {
            onPlayerWon?.Invoke(this, playerTurn);
            Debug.Log($"{playerTurn} Won!");
            return;
        }

        NextTurn();
    }


    private void NextTurn()
    {
        if (playerTurn == Player.PlayerOne)
        {
            playerTurn = Player.PlayerTwo;
        }
        else if (playerTurn == Player.PlayerTwo)
        {
            playerTurn = Player.PlayerOne;
        }
    }

    private void EndGame(Player winner)
    {
        onPlayerWon?.Invoke(this, winner);
    }
    private void StartGame()
    {
        Debug.Log("Started");
        ResetGame();
        _solver.SetupSolver(ConnectFourBoard, amountToWin);
    }

    private void ResetGame()
    {
        if (ConnectFourBoard == null)
        {
            ConnectFourBoard = new(_rows, _columns);
        }
        else
            ConnectFourBoard.ResetBoard();

        onBoardReset?.Invoke(this, new Vector2Int(_rows, _columns));
        playerTurn = Player.PlayerOne;

    }
}

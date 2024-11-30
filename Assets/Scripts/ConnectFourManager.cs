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
    public EventHandler onTurnStart;
    public Action<Vector2Int, Player> onDiskAdded;
    public Action<int> onDiskMissed;

    private Player playerTurn;
    public Player PlayerTurn => playerTurn;

    [SerializeField] private ConnectFourSolver _solver;

    private bool _hasEnded = false;

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

    private void Start()
    {
        onBoardReset?.Invoke(this, new Vector2Int(_rows, _columns));
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
            //Even though no disk is added, the turn is still over for the player (make anim for this)
            onDiskMissed?.Invoke(rowIndex);
            return;
        }

        onDiskAdded?.Invoke(new Vector2Int(rowIndex, column), playerTurn);

        if (_solver.IsWinningMove(rowIndex, column, playerTurn))
        {
            EndGame(playerTurn);
            Debug.Log($"{playerTurn} Won!");
            return;
        }

        if (ConnectFourBoard.IsBoardFull())
        {
            EndGame(Player.None);
            Debug.Log("Draw");
        }
    }


    public void NextTurn()
    {
        if (playerTurn == Player.PlayerOne)
        {
            playerTurn = Player.PlayerTwo;
        }
        else if (playerTurn == Player.PlayerTwo)
        {
            playerTurn = Player.PlayerOne;
        }

        onTurnStart?.Invoke(this, EventArgs.Empty);
    }

    private void EndGame(Player winner)
    {
        onPlayerWon?.Invoke(this, winner);
    }
    public void StartGame()
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

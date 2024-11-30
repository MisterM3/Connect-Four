using System;
using UnityEngine;

/// <summary>
/// Keeps track of the connectFour Board and is the centeralized spot for all the logic
/// </summary>
/// Needs to be run before everything else, and thus has been placed in the Script Execution Order
public class ConnectFourManager : Singleton<ConnectFourManager>
{
    /// While I did not make the player be able to change the board dimensions or amount to win during the game, 
    /// I still kept it as variables instead of constants for if that would be added which you will see if various scripts
    [SerializeField] [Range(1, 15)] private int _rows = 6;
    [SerializeField] [Range(1, 15)] private int _columns = 7;
    [SerializeField] [Range(3, 10)] private int _amountToWin = 4;

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

    //I use events to connect things together as the manager doesn't need to know everything about the visuals
    public EventHandler<Player> onPlayerWon;
    public EventHandler<Vector2Int> onBoardReset;
    public EventHandler onTurnStart;
    public Action<Vector2Int, Player> onDiskAdded;
    public Action<int> onDiskMissed;

    private Player _playerTurn;
    public Player PlayerTurn => _playerTurn;

    [SerializeField] private ConnectFourSolver _solver;

    private bool _hasEnded = false;
    public bool HasEnded => _hasEnded;

    private void Awake()
    {
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
        if (!ConnectFourBoard.TryAddDisk(rowIndex, _playerTurn, out int column))
        {
            //Even though no disk is added, the turn is still over for the player (make anim for this)
            onDiskMissed?.Invoke(rowIndex);
            return;
        }

        onDiskAdded?.Invoke(new Vector2Int(rowIndex, column), _playerTurn);

        if (_solver.IsWinningMove(rowIndex, column, _playerTurn))
        {
            EndGame(_playerTurn);
            return;
        }

        if (ConnectFourBoard.IsBoardFull())
        {
            EndGame(Player.None);
        }
    }


    /// Next turn is called on the visuals using a delegate, as we have to wait untill the visuals are done before allowing the player to throw the ball aggain.
    public void NextTurn()
    {
        if (_hasEnded)
            return;

        if (_playerTurn == Player.PlayerOne)
        {
            _playerTurn = Player.PlayerTwo;
        }
        else if (_playerTurn == Player.PlayerTwo)
        {
            _playerTurn = Player.PlayerOne;
        }

        onTurnStart?.Invoke(this, EventArgs.Empty);
    }

    private void EndGame(Player winner)
    {
        _hasEnded = true;
        onPlayerWon?.Invoke(this, winner);
    }
    public void StartGame()
    {
        ResetGame();
        _hasEnded = false;
        _solver.SetupSolver(ConnectFourBoard, _amountToWin);
    }

    private void ResetGame()
    {
        if (ConnectFourBoard == null)
        {
            ConnectFourBoard = new(_rows, _columns);
        }
        else
            ConnectFourBoard.ResetBoard();

        _playerTurn = Player.PlayerOne;
        onBoardReset?.Invoke(this, new Vector2Int(_rows, _columns));

    }
}

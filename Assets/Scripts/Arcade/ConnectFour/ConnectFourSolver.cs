using UnityEngine;

/// <summary>
/// Abstract class to solve a connect four board
/// </summary>
public abstract class ConnectFourSolver : MonoBehaviour
{
    protected ConnectFourBoard currentBoard;
    protected int amountToWin = 4;

    public void SetupSolver(ConnectFourBoard board, int amountToWin)
    {
        currentBoard = board;
        this.amountToWin = amountToWin;
    }

    public bool IsWinningMove(Vector2Int startingPosition, Player playerToCheck) => IsWinningMove(startingPosition.x, startingPosition.y, playerToCheck);
    abstract public bool IsWinningMove(int rowIndex, int columnIndex, Player playerToCheck);
}

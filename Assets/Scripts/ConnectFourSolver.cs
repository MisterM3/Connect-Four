using UnityEngine;

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

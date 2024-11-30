using UnityEngine;

/// <summary>
/// Generic board that for playing connect four
/// </summary>
/// Making the board with little connection to Unity makes it easy to use in different projects, or to use in base C#
public class ConnectFourBoard
{
    /// The board consist of which player holds which square
    private Player[,] _connectFourBoard;

    public ConnectFourBoard(int rows, int columns)
    {
        if (rows <= 0)
        {
            Debug.LogWarning($"Amount of rows invalid, needs to have minimal of 1. Amount: {rows}");
            return;
        }
        if (columns <= 0)
        {
            Debug.LogWarning($"Amount of columns invalid, needs to have minimal of 1. Amount: {columns}");
            return;
        }

        _connectFourBoard = new Player[rows, columns];
    }

    public int GetLenght(int dimension) => _connectFourBoard.GetLength(dimension);

    public void ResetBoard()
    {
        int rows = _connectFourBoard.GetLength(0);
        int columns = _connectFourBoard.GetLength(1);
        _connectFourBoard = new Player[rows, columns];
    }

    public Player GetPlayerAtLocation(Vector2Int location) => GetPlayerAtLocation(location.x, location.y);
    public Player GetPlayerAtLocation(int rowIndex, int columnIndex)
    {
        if (!IsValidPosition(rowIndex, columnIndex))
            return Player.None;

        return _connectFourBoard[rowIndex, columnIndex];
    }

    public bool IsBoardFull()
    {
        //-1 as I'm using the value to access an array
        int topColumn = _connectFourBoard.GetLength(1) - 1;

        for (int i = 0; i < _connectFourBoard.GetLength(0); i++)
        {
            if (_connectFourBoard[i, topColumn] == Player.None)
                return false;
        }

        //The top row is full, so the board is full
        return true;
    }

    public bool IsValidPosition(Vector2Int location) => IsValidPosition(location.x, location.y);
    public bool IsValidPosition(int rowIndex, int columnIndex)
    {

        int boardRowCount = _connectFourBoard.GetLength(0);
        if (rowIndex < 0 || rowIndex >= boardRowCount)
        {
            Debug.Log($"Row Position is outside of the board. Keep the value between 0 and {boardRowCount - 1}, current rowIndex is {rowIndex}");
            return false;
        }

        int columnRowCount = _connectFourBoard.GetLength(1);
        if (columnIndex < 0 || columnIndex >= columnRowCount)
        {
            Debug.Log($"Column Position is outside of the board. Keep the value between 0 and {columnRowCount - 1}, current columnIndex is {columnIndex}");
            return false;
        }

        return true;
    }



    /// For functions like these I try to keep only one with the logic, and the rest using that function so they are easily changed
    public bool TryAddDisk(int rowIndex, Player player)
    {
        //Don't need to use column so just discard the outcome by using _
        return TryAddDisk(rowIndex, player, out _);
    }

    public bool TryAddDisk(int rowIndex, Player player, out int columnIndexNewDisk)
    {
        //Makes sure index is not valid if disk is not added
        columnIndexNewDisk = -1;
        if (!IsValidPosition(rowIndex, 0))
            return false;
        
        int columns = _connectFourBoard.GetLength(1);
        for (int columnIndex = 0; columnIndex < columns; columnIndex++)
        {
            bool isEmptySpace = _connectFourBoard[rowIndex, columnIndex] == Player.None;

            if (!isEmptySpace)
                continue;

            _connectFourBoard[rowIndex, columnIndex] = player;
            columnIndexNewDisk = columnIndex;
            return true;
        }

        //If code got here, then the row is full, so disk was not added
        return false;
    }

    private void AddDisk(int rowIndex, Player player)
    {
        TryAddDisk(rowIndex, player);
    }

    private void AddDisk(int rowIndex, Player player, out int columnIndexNewDisk)
    {
        TryAddDisk(rowIndex, player, out columnIndexNewDisk);
    }
}

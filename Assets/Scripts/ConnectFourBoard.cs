using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFourBoard
{
    private Player[,] _connectFourBoard;
    public Player[,] Board
    {
        get => _connectFourBoard;
    }

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

    public void ResetBoard()
    {
        int rows = _connectFourBoard.GetLength(0);
        int columns = _connectFourBoard.GetLength(1);
        _connectFourBoard = new Player[rows, columns];
    }

    public bool TryAddDisk(int rowIndex, Player player)
    {
        //Don't need to use column so just discard the outcome by using _
        return TryAddDisk(rowIndex, player, out _);
    }

    public bool TryAddDisk(int rowIndex, Player player, out int columnIndexNewDisk)
    {
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
        columnIndexNewDisk = -1;
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

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Solver that checkes every direction around the just placed piece to see if the player won this turn
/// </summary>
/// I made the solver only check the disk that was just placed to make the process go quicker
public class SolverDirections : ConnectFourSolver
{
    public override bool IsWinningMove(int rowIndex, int columnIndex, Player playerToCheck)
    {
        if (currentBoard == null || amountToWin == 0)
        {
            Debug.LogWarning($"SolverDirections is not setup! Use the SetupSolver method before checking for a winning move!");
            return false;
        }

        if (!currentBoard.IsValidPosition(rowIndex, columnIndex))
            return false;

        List<Vector2Int> directionsToCheck = GetDirectionsToCheck(rowIndex, columnIndex);

        foreach (Vector2Int direction in directionsToCheck)
        {
            bool hasWon = IsDirectionWinning(direction, rowIndex, columnIndex, playerToCheck);
            //We don't have to continue looking if we found the winning move
            if (hasWon)
                return true;
        }

        return false;
    }

    protected bool IsDirectionWinning(Vector2Int direction, Vector2Int startingPosition, Player playerToCheck) => IsDirectionWinning(direction, startingPosition.x, startingPosition.y, playerToCheck);
    protected bool IsDirectionWinning(Vector2Int direction, int rowIndex, int columnIndex, Player playerToCheck)
    {
        //Starting at 1 instead of 0, as the just placed disk is always correct for winning
        for (int i = 1; i < amountToWin; i++)
        {
            int rowIndexToCheck = rowIndex + direction.x * i;
            int columnIndexToCheck = columnIndex + direction.y * i;

            //If at any point in the checking anything else than the player disk is found, than the player hasn't won with this line
            if (currentBoard.GetPlayerAtLocation(rowIndexToCheck, columnIndexToCheck) != playerToCheck)
                return false;
        }

        //If the direction is fully for the player, the player has won
        return true;
    }

    protected List<Vector2Int> GetDirectionsToCheck(Vector2Int startingPosition) => GetDirectionsToCheck(startingPosition.x, startingPosition.y);
    protected List<Vector2Int> GetDirectionsToCheck(int rowIndex, int columnIndex)
    {
        //Going straight up is never an option as the space above the currently placed disk is empty
        List<Vector2Int> directionsToCheck = new();


        bool canWinLeft = (rowIndex >= amountToWin - 1);
        bool canWinRight = (rowIndex + amountToWin <= currentBoard.GetLenght(0));
        bool canWinDown = (columnIndex >= amountToWin - 1);
        bool canWinUp = (columnIndex + amountToWin <= currentBoard.GetLenght(1));


        if (canWinLeft)
        {
            directionsToCheck.Add(new Vector2Int(-1, 0));

            if (canWinDown)
                directionsToCheck.Add(new Vector2Int(-1, -1));

            if (canWinUp)
                directionsToCheck.Add(new Vector2Int(-1, 1));
        }

        if (canWinRight)
        {
            directionsToCheck.Add(new Vector2Int(1, 0));

            if (canWinDown)
                directionsToCheck.Add(new Vector2Int(1, -1));

            if (canWinUp)
                directionsToCheck.Add(new Vector2Int(1, 1));
        }

        if (canWinDown)
            directionsToCheck.Add(new Vector2Int(0, -1));

        return directionsToCheck;
    }
}



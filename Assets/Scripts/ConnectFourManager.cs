using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFourManager : Singleton<ConnectFourManager>
{

    [SerializeField] [Range(1, 15)] private int _rows = 6;
    [SerializeField] [Range(1, 15)] private int _columns = 7;

    private ConnectFourBoard _connectFourBoard;

    EventHandler<Player> onPlayerWon;

    Player playerTurn;

    [SerializeField] private ConnectFourSolver _solver;

    private void Awake()
    {
        InitializeSingleton(this);
    }

    public void PlayTurn(int rowIndex)
    {
        if (!_connectFourBoard.TryAddDisk(rowIndex, playerTurn, out int column))
            return;

        if (_solver.CheckIfWin(rowIndex, column, playerTurn))
        {

            
        }
        else
        {

        }
    }


    private void NextTurn()
    {

    }

    private void EndGame(Player winner)
    {
        onPlayerWon?.Invoke(this, winner);
    }
    private void StartGame()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        if (_connectFourBoard == null)
        {
            _connectFourBoard = new(_rows, _columns);
        }
        else
            _connectFourBoard.ResetBoard();
    }
}

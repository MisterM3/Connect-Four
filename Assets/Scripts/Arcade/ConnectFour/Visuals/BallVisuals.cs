using System;
using UnityEngine;

/// <summary>
/// Keeps track on which ball is currently waiting to be thrown
/// </summary>
public class BallVisuals : MonoBehaviour
{
    [SerializeField] private GameObject _playerOneBall;
    [SerializeField] private GameObject _playerTwoBall;

    private void Start()
    {
        ConnectFourManager.Instance.onTurnStart += ConnectFourManager_OnTurnStart;
        ConnectFourManager.Instance.onBoardReset += OnBoardReset;
    }

    private void OnDestroy()
    {
        ConnectFourManager.Instance.onTurnStart -= ConnectFourManager_OnTurnStart;
    }

    private void OnBoardReset(object sender, Vector2Int e) => SetBall();

    private void ConnectFourManager_OnTurnStart(object sender, EventArgs e) => SetBall();

    private void SetBall()
    {
        Player playerTurn = ConnectFourManager.Instance.PlayerTurn;
        switch (playerTurn)
        {
            case Player.PlayerOne:
                _playerOneBall.SetActive(true);
                _playerTwoBall.SetActive(false);
                break;
            case Player.PlayerTwo:
                _playerOneBall.SetActive(false);
                _playerTwoBall.SetActive(true);
                break;
            default:
                _playerOneBall.SetActive(false);
                _playerTwoBall.SetActive(false);
                break;
        }
    }
}

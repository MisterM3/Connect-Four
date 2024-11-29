using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisuals : MonoBehaviour
{

    [SerializeField] private GameObject playerOneBall;
    [SerializeField] private GameObject playerTwoBall;


    private void Start()
    {
        ConnectFourManager.Instance.onTurnStart += ConnectFourManager_OnTurnStart;
    }

    private void ConnectFourManager_OnTurnStart(object sender, EventArgs e)
    {
        Player playerTurn = ConnectFourManager.Instance.PlayerTurn;
        switch(playerTurn)
        {
            case Player.PlayerOne:
                playerOneBall.SetActive(true);
                playerTwoBall.SetActive(false);
                break;
            case Player.PlayerTwo:
                playerOneBall.SetActive(false);
                playerTwoBall.SetActive(true);
                break;
            default:
                playerOneBall.SetActive(false);
                playerTwoBall.SetActive(false);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] Transform midPointTransform;
    [SerializeField] GameObject playerOneBall;
    [SerializeField] GameObject playerTwoBall;

    public Vector3 WorldPosition => midPointTransform.position;

    public void SetDisk(Player player)
    {
        if (player == Player.PlayerOne)
        {
            playerOneBall.SetActive(true);
            playerTwoBall.SetActive(false);
        } 
        else if (player == Player.PlayerTwo)
        {
            playerOneBall.SetActive(false);
            playerTwoBall.SetActive(true);
        }
    }

    public void ResetHoop()
    {
        playerOneBall.SetActive(false);
        playerTwoBall.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowWinner : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textField;

    public void SetWinner(Player winner)
    {
        string winnerText = "";

        switch (winner)
        {
            case Player.None:
                winnerText = "Draw!";
                break;
            case Player.PlayerOne:
                winnerText = "Yellow Wins!";
                break;
            case Player.PlayerTwo:
                winnerText = "Red Wins!";
                break;
        }

        textField.text = winnerText;
    }
}

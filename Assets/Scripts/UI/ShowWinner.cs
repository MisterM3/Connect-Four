using UnityEngine;
using TMPro;

public class ShowWinner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    //Using start and ondestroy as these events should be received even if the gameobject is disabled
    public void Start()
    {
        ConnectFourManager.Instance.onPlayerWon += SetWinner;
    }

    public void OnDestroy()
    {
        ConnectFourManager.Instance.onPlayerWon -= SetWinner;
    }

    public void SetWinner(object o, Player winner) => SetWinner(winner);

    //I use a switch as it looks cleaner and more readable in this scenario
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

        _textField.text = winnerText;
    }
}

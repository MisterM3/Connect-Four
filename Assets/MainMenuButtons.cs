using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    
    public void PlayGame()
    {
        ConnectFourManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

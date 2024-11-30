using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField] UIStateMachine uiStateMachine;

    private void Awake()
    {
        if (uiStateMachine == null)
        {
            Debug.LogError("No UIStateMachine on MainMenuButtons, add one! " + gameObject.name);
        }
    }

    public void PlayGame()
    {
        uiStateMachine.SwitchUI(UIStates.None);
        ConnectFourManager.Instance.StartGame();
    }

    public void Info()
    {
        uiStateMachine.SwitchUI(UIStates.Info);
    }

    public void Options()
    {
        uiStateMachine.SwitchUI(UIStates.Options);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

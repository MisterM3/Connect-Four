using UnityEngine;

/// <summary>
/// Functionality for Buttons on the main menu (can be called with UnityEvents on Buttons)
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] UIStateMachine _uiStateMachine;

    private void Awake()
    {
        if (_uiStateMachine == null)
        {
            Debug.LogError("No UIStateMachine on MainMenuButtons, add one! " + gameObject.name);
        }
    }

    public void PlayGame()
    {
        _uiStateMachine.SwitchUI(UIStates.None);
        ConnectFourManager.Instance.StartGame();
    }

    //When functioned can be put together and have easy to understand functionality (with one liners), I like to order it this way for readability
    public void Info() => _uiStateMachine.SwitchUI(UIStates.Info);
    public void Options() => _uiStateMachine.SwitchUI(UIStates.Options);
    public void Quit() => Application.Quit();
}

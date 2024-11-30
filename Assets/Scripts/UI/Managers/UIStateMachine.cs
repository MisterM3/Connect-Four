using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIStateMachine : Singleton<UIStateMachine>
{
    //Using this system as dictionary are easier to use for getting items, but I can't serialize them
    [SerializeField] private List<UIObject> _uiStates;
    private Dictionary<UIStates, IStateUI> _uiStatesDict = new();

    private UIStates currentState = UIStates.None;



    [Serializable]
    private struct UIObject
    {
        //Can't serialize interfaces, so this needs to be done like this
        public StateUISimple stateUI;
        public UIStates uiState;
    }

    private void Awake()
    {

        InitializeSingleton(this);

        foreach(UIObject uiObject in _uiStates)
        {
            _uiStatesDict.Add(uiObject.uiState, uiObject.stateUI);
        }

        DisableAllStates();
        SwitchUI(UIStates.MainMenu);
    }

    public void BackToMainMenu()
    {
        SwitchUI(UIStates.MainMenu);
    }

    public void SwitchUI(UIStates state)
    {
        if (currentState != UIStates.None)
        {
            DisableState(currentState);
        }

        currentState = state;

        if (currentState != UIStates.None)
        {
            EnableState(currentState);
        }
    }

    private void DisableState(UIStates state)
    {
        if (!_uiStatesDict.TryGetValue(state, out IStateUI stateScript))
        {
            Debug.LogWarning($"No GameObject for UIState {state}, add one to UIStateMachine");
            return;
        }
        stateScript.DisableState();

    }

    private void EnableState(UIStates state)
    {
        if (!_uiStatesDict.TryGetValue(state, out IStateUI stateScript))
        {
            Debug.LogWarning($"No GameObject for UIState {state}, add one to UIStateMachine");
            return;
        }
        stateScript.EnableState();
    }

    private void DisableAllStates()
    {
        foreach(IStateUI stateUI in _uiStatesDict.Values)
        {
            stateUI.DisableState();
        }
    }
}

[Serializable]
public enum UIStates
{
    None,
    MainMenu,
    Options,
    Info,
    EndingGame,
}
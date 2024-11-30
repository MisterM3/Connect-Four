using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Simplistic State Machine for Enabling and Disabling UI
/// </summary>
public class UIStateMachine : Singleton<UIStateMachine>
{
    //Using this system as dictionary are easier to use for getting items, but I can't serialize them
    [SerializeField] private List<UIObject> _uiStates;
    private Dictionary<UIStates, IStateUI> _uiStatesDict = new();

    private UIStates _currentState = UIStates.None;

    //As I will never be calling this from outside this class and I'm only using it to make SerializedFields, I keep it at the top of the class with the variables
    [Serializable]
    private struct UIObject
    {
        //Can't serialize interfaces, so this needs to be done like this, I don't have time to make a good serializable system for all classes
        public StateUISimple stateUI;
        public UIStates uiState;
    }

    private void Awake()
    {
        InitializeSingleton(this);
    }
    protected override void OnSingletonSucceedInitialize()
    {
        foreach (UIObject uiObject in _uiStates)
        {
            _uiStatesDict.Add(uiObject.uiState, uiObject.stateUI);
        }

        DisableAllStates();
        SwitchUI(UIStates.MainMenu);
    }

    //Used a lot, and has to be used in UnityEvents so I can't use a enum parameter
    public void BackToMainMenu() => SwitchUI(UIStates.MainMenu);

    public void SwitchUI(UIStates state)
    {
        if (_currentState != UIStates.None)
        {
            DisableState(_currentState);
        }

        _currentState = state;

        if (_currentState != UIStates.None)
        {
            EnableState(_currentState);
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
    private void DisableAllStates()
    {
        foreach (IStateUI stateUI in _uiStatesDict.Values)
        {
            stateUI.DisableState();
        }
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
}


using UnityEngine;
using System.Collections.Generic;
using System;

public class StateManager : MonoBehaviour
{
    [SerializeField] Digit[] digits;
    
    private Stack<StateSnapshot> stateSnapshots;

    private string[] prevDigitPatterns;
    private ActionCard lastUsedAction;

    public static StateManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        stateSnapshots = new Stack<StateSnapshot>();
    }

    public void addSnapshot(StateSnapshot snapshot)
    {
        stateSnapshots.Push(snapshot);
    }
    
    public StateSnapshot popSnapshot()
    {
        return stateSnapshots.Pop();
    }

    public void clearStates()
    {
        stateSnapshots.Clear();
    }

    public void TakeSnapshot()
    {
        SavePrevDigitPatterns();
        stateSnapshots.Push(new StateSnapshot(prevDigitPatterns, lastUsedAction));
    }

    public void SetLastUsedAction (ActionCard lastUsedAction)
    {
        this.lastUsedAction = lastUsedAction;
    }

    public void SavePrevDigitPatterns()
    {
        string[] digitPatterns = new string[digits.Length];
        for (int i = 0; i < digits.Length; i++)
        {
            digitPatterns[i] = digits[i].GetPattern();
        }

        prevDigitPatterns = digitPatterns;
    }
}

public class StateSnapshot
{
    string[] digitPatterns;
    ActionCard actionUsed;

    public StateSnapshot(string[] digitPatterns, ActionCard actionUsed)
    {
        this.digitPatterns = digitPatterns;
        this.actionUsed = actionUsed;
    }

    public string[] DigitPatterns => digitPatterns;

    public ActionCard ActionUsed => actionUsed;
}
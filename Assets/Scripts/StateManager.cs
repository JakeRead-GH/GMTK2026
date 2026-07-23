using UnityEngine;
using System.Collections.Generic;
using System;

public class StateManager : MonoBehaviour
{
    [SerializeField] Digit[] digits;
    
    private Stack<StateSnapshot> stateSnapshots;

    private Dictionary<Digit, string> prevDigitPatterns;
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

    public void AddSnapshot(StateSnapshot snapshot)
    {
        stateSnapshots.Push(snapshot);
    }
    
    public StateSnapshot PopSnapshot()
    {
        return stateSnapshots.Pop();
    }

    public void ClearStates()
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
        Dictionary<Digit, string> digitPatterns = new Dictionary<Digit, string>();
        foreach (Digit digit in digits) {
            digitPatterns.Add(digit, digit.GetPattern());
        }

        prevDigitPatterns = digitPatterns;
    }
}

public class StateSnapshot
{
    Dictionary<Digit, string> digitPatterns;
    ActionCard actionUsed;

    public StateSnapshot(Dictionary<Digit, string> digitPatterns, ActionCard actionUsed)
    {
        this.digitPatterns = digitPatterns;
        this.actionUsed = actionUsed;
    }

    public Dictionary<Digit, string> DigitPatterns => digitPatterns;

    public ActionCard ActionUsed => actionUsed;
}
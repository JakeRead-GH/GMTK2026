using UnityEngine;
using System.Collections.Generic;
using System;

public class StateManager : MonoBehaviour
{
    [SerializeField] Digit[] digits;
    
    private Stack<StateSnapshot> stateSnapshots;

    private Dictionary<Digit, string> prevDigitPatterns;
    private Dictionary<Digit, string> prevLockedPatterns;
    private ActionCard lastUsedAction;

    public static StateManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        stateSnapshots = new Stack<StateSnapshot>();
    }

    public void AddSnapshot(StateSnapshot snapshot)
    {
        stateSnapshots.Push(snapshot);
    }
    
    public StateSnapshot PopSnapshot()
    {
        if(stateSnapshots.Count == 0)
        {
            return null;
        }
        return stateSnapshots.Pop();
    }

    public void ClearStates()
    {
        stateSnapshots.Clear();
    }

    public void TakeSnapshot()
    {
        SavePrevDigitPatterns();
        SavePrevLockedPatterns();
        stateSnapshots.Push(new StateSnapshot(prevDigitPatterns, prevLockedPatterns, lastUsedAction));
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

    public void SavePrevLockedPatterns()
    {
        Dictionary<Digit, string> lockedPatterns = new Dictionary<Digit, string>();
        foreach (Digit digit in digits)
        {
            lockedPatterns.Add(digit, digit.GetLockedPattern());
        }

        prevLockedPatterns = lockedPatterns;
    }
}

public class StateSnapshot
{
    Dictionary<Digit, string> digitPatterns;
    Dictionary<Digit, string> lockedPatterns;
    ActionCard actionUsed;

    public StateSnapshot(Dictionary<Digit, string> digitPatterns, Dictionary<Digit, string> lockedPatterns, ActionCard actionUsed)
    {
        this.digitPatterns = digitPatterns;
        this.lockedPatterns = lockedPatterns;
        this.actionUsed = actionUsed;
    }

    public Dictionary<Digit, string> DigitPatterns => digitPatterns;
    public Dictionary<Digit, string> LockedPatterns => lockedPatterns;
    public ActionCard ActionUsed => actionUsed;
}
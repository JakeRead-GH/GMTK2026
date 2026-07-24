using UnityEngine;
using System.Collections.Generic;

public class LevelInfoStore : MonoBehaviour
{
    public static LevelInfoStore Instance { get; private set; }

    [Tooltip("Action cards present in the level")]
    [SerializeField] ActionCard[] actionCards;

    [Tooltip("Number of uses available for each action. Must be ordered in the order of the action cards")]
    [SerializeField] int[] remainingUses;

    [Tooltip("Initial numbers/letters that will appear on the display")]
    [SerializeField] string initialDisplayPattern;

    [Tooltip("Pattern the display must reach to succeed")]
    [SerializeField] string winningDisplayPattern;

    private Dictionary<ActionCard, int> actionCardUsesMapping;
    
    public ActionCard[] ActionCards => actionCards;
    public Dictionary<ActionCard, int> ActionCardUsesMapping => actionCardUsesMapping;
    public string InitialDisplayPattern => initialDisplayPattern;
    public string WinningDisplayPattern => winningDisplayPattern;

    void Awake()
    {
        if (actionCards.Length != remainingUses.Length) 
        { 
            Debug.Log("Action cards and remaining uses are not the same length");
            return;
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        actionCardUsesMapping = new Dictionary<ActionCard, int>();
        for (int i = 0; i < actionCards.Length; i++) 
        {
            actionCardUsesMapping.Add(actionCards[i], remainingUses[i]);
        }
    }

}

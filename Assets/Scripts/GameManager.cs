using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    [SerializeField] Display display;
    [SerializeField] ActionSelectionManager actionSelectionManager;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip successClip;

    SceneLoader sceneLoader;

    //private static Dictionary<string, string> levelDisplayInitialStates = new Dictionary<string, string>
    //{
    //    { "MainStage", "-120" },
    //    { "Level_01", " 0NE" },
    //    { "Level_02", "22" },
    //    { "Level_03", "333" },
    //    { "Level_04", "4444444" },
    //    { "Level_05", "HELP" },
    //    { "Level_06", "0000" },
    //    { "Level_07", "0000" },
    //    { "Level_08", "0000" },
    //    { "Level_09", "0000" },
    //    { "Level_10", "0000" }
    //    //{ "CarcorSceneNoTouchy", "0000" }
    //};

    //private static Dictionary<string, string> levelWinConditions = new Dictionary<string, string>
    //{
    //    { "MainStage", "1200" },
    //    { "Level_01", "NE--" },
    //    { "Level_07", "-00-" }
    //};



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        sceneLoader = SceneLoader.instance;
        SetLevelInitialState();
    }

    public void HandleSucess()
    {
        if (!display.CheckSuccess())
        {
            return;
        }
        SoundFXManager.instance.PlaySoundFXClip(successClip, transform, 1f);
        sceneLoader.LoadNextLevel();
    }

    public void SetLevelInitialState()
    {
        display.SetDisplayPattern(LevelInfoStore.instance.InitialDisplayPattern);
        ActionCard[] actionCards = LevelInfoStore.instance.ActionCards;
        Dictionary<ActionCard, int> actionCardUsesMapping = LevelInfoStore.instance.ActionCardUsesMapping;
        foreach (ActionCard actionCard in actionCards) 
        { 
            actionCard.SetRemainingUses(actionCardUsesMapping[actionCard]);
            actionCard.RefreshVisuals();
        }
        actionSelectionManager.ClearSelection();
        StateManager.instance.ClearStates();
    }
}

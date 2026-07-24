using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    [SerializeField] Display display;
    
    SceneLoader sceneLoader;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        sceneLoader = SceneLoader.instance;
    }

    public void HandleSucess()
    {
        if (!display.CheckSuccess())
        {
            return;
        }
        sceneLoader.LoadNextLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //make the game manager a singleton
    public static GameManager Instance { get; private set; }
    public Stage currentStage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GetCurrentStage();
    }

    private void GetCurrentStage()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentStageIndex = currentScene.buildIndex;
        //current stage is going to be the equivalent index in Stage
        currentStage = StageHelper.GetStageByIndex(currentStageIndex);
    }

}

using System;
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
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GetCurrentStage();
        if (currentStage == Stage.Ocean_1)
        {
            //use the AudioManager to play the background music
            AudioManager.Instance.PlayMusic(SoundType.Music_Loop);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetMusicAccordingToScene(scene);
    }

    private void GetMusicAccordingToScene(Scene scene)
    {
        if (scene.buildIndex == (int)Stage.Ocean_1)
        {
            AudioManager.Instance.PlayMusic(SoundType.Music_Loop);
        }
        else if (scene.buildIndex == (int)Stage.Ocean_2)
        {
            AudioManager.Instance.PlayMusic(SoundType.Music_Loop);
        }
        else if (scene.buildIndex == (int)Stage.Ocean_3)
        {
            AudioManager.Instance.PlayMusic(SoundType.Music_Loop);
        }
    }


    void Update()
    {

    }


    private void GetCurrentStage()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentStageIndex = currentScene.buildIndex;
        //current stage is going to be the equivalent index in Stage
        currentStage = StageHelper.GetStageByIndex(currentStageIndex);
    }

    internal void StartGame()
    {
        int stageIndex = (int)Stage.Ocean_1;
        SceneManager.LoadScene(stageIndex);
    }

    public void StartNextStage()
    {
        int nextStageIndex = (int)currentStage + 1;
        SceneManager.LoadScene(nextStageIndex);
    }

    public void RestartStage()
    {
        SceneManager.LoadScene((int)currentStage);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class MainMenu : MonoBehaviour
{

    public event System.Action OnStart;
    Button startBtn;
    Button quitBtn;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startBtn = root.Q<Button>("StartBtn");
        quitBtn = root.Q<Button>("QuitBtn");


        quitBtn.RegisterCallback<NavigationSubmitEvent>(ev => Application.Quit());
        startBtn.RegisterCallback<NavigationSubmitEvent>(OnStartGameSubmitted);
        quitBtn.RegisterCallback<ClickEvent>(ev => Application.Quit());
        startBtn.RegisterCallback<ClickEvent>(OnStartGameClicked);
    }

    private void OnStartGameSubmitted(NavigationSubmitEvent evt)
    {
        StartGame();
    }

    private void OnStartGameClicked(ClickEvent evt)
    {
        StartGame();
    }

    private void StartGame()
    {
        OnStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }



}

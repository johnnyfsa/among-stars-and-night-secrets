using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    [SerializeField]
    MainMenu mainMenu;
    private Animator bgAnimator;
    private GameObject logo;
    // Start is called before the first frame update

    void Awake()
    {
        mainMenu.OnStart += StartGame;
    }

    void OnDestroy()
    {
        mainMenu.OnStart -= StartGame;
    }

    private void StartGame()
    {
        bgAnimator.SetBool(AnimationStrings.startGame, true);
        logo.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(SoundType.Shooting_Star_1);
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.StartGame();
    }

    void Start()
    {
        bgAnimator = GetComponentInChildren<Animator>();
        logo = GameObject.Find("Logo");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

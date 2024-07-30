using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Canvas fadeOutCanvas;

    void Start()
    {
        fadeOutCanvas.gameObject.GetComponent<Animator>().Play("IrisFadeIn");
        StartCoroutine(DisableCanvas());
    }
    void Update()
    {
        if (player.HasGoalStar)
        {
            AudioManager.Instance.PlaySFX(SoundType.Win_Fanfare);
            fadeOutCanvas.gameObject.SetActive(true);
            StartCoroutine(StartNewStage());
        }
    }

    private IEnumerator DisableCanvas()
    {
        yield return new WaitForSeconds(2f);
        fadeOutCanvas.gameObject.SetActive(false);
    }

    private IEnumerator StartNewStage()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.StartNextStage();
    }
}

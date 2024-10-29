using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public GameObject Logo;
    public Animator bacnGroundAnimator;

    float animationDuration;
    // Start is called before the first frame update
    void Start()
    {
        //get the animation duration
        animationDuration = bacnGroundAnimator.GetCurrentAnimatorStateInfo(0).length;
        AudioManager.Instance.PlaySFX(SoundType.Shooting_Star_2);
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(animationDuration);
        Logo.SetActive(true);
        StartCoroutine(BackToMainMenu());
    }

    private IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        //fade the music volume
        GameManager.Instance.LoadMainMenu();
    }

}

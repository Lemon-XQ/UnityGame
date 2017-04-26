using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIStart : UIBase {


    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        SoundManager.Instance.PlayBGM("Audio_bgm_startup_cg");
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.interactable = true;
        gameObject.SetActive(true);

    }

    public override void DoOnPausing()
    {
        canvasGroup.interactable = false;
        gameObject.SetActive(false);

    }

    public override void DoOnResuming()
    {
        SoundManager.Instance.PlayBGM("Audio_bgm_startup_cg");
        canvasGroup.interactable = true;
        gameObject.SetActive(true);

    }

    public override void DoOnExit()
    {
        canvasGroup.DOFade(0f, 0.5f);
        canvasGroup.interactable = false;
        gameObject.SetActive(false);

    }

    public void GoToOption()
    {
        UIManager.Instance.PushUIPanel("UIOption");
    }

    public void GoToLevel()
    {
        UIManager.Instance.PushUIPanel("UILevel");
    }

    public void Exit()
    {
        Application.Quit();
    }

}

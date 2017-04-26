using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UILevel : UIBase {

    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
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
        canvasGroup.interactable = true;
        gameObject.SetActive(true);

    }

    public override void DoOnExit()
    {
        canvasGroup.DOFade(0f, 0.5f);
        canvasGroup.interactable = false;
        gameObject.SetActive(false);

    }

    public void GoToPlay(string LevelName)
    {
        UIManager.Instance.PushUIPanel(LevelName);
    }

    public void GoToStart()
    {
        UIManager.Instance.PopUIPanel();
    }
}

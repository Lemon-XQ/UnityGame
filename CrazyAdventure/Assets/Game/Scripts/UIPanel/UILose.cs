using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UILose : UIBase
{
    public Text text_IQ;
    private string text;

    void Awake()
    {
        text = text_IQ.text;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        transform.GetChild(0).DOLocalMoveY(11,1f);
        canvasGroup.interactable = true;
        int IQ = Random.Range(-500,100);
        text_IQ.text = text + IQ.ToString();
        gameObject.SetActive(true);

    }

    public override void DoOnPausing()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
    }

    public override void DoOnResuming()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
        int IQ = Random.Range(-500, 100);
        text_IQ.text = text + IQ.ToString();
    }

    public override void DoOnExit()
    {
        canvasGroup.interactable = false;
        transform.GetChild(0).DOLocalMoveY(452, 1f);
        gameObject.SetActive(false);

    }

    public void OnReplayClick()
    {
        HeroController.Instance.ResetGame();
    }

}

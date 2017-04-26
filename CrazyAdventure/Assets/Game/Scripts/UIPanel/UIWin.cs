using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIWin : UIBase
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
        transform.GetChild(0).DOLocalMoveY(11, 1f);
        canvasGroup.interactable = true;
        int IQ = Random.Range(200, 400);
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
        int IQ = Random.Range(200, 400);
        text_IQ.text = text + IQ.ToString();
    }

    public override void DoOnExit()
    {
        canvasGroup.interactable = false;
        transform.GetChild(0).DOLocalMoveY(452, 1f);
        gameObject.SetActive(false);

    }

    public void OnHomeClick()
    {
        //当栈中有超过一个面板时弹出，栈底为Home面板
        //因为reset中也要弹出一个面板，所以是>2
        while (UIManager.Instance.UIStack.Count>2)
        {
            UIManager.Instance.PopUIPanel();
        }
        HeroController.Instance.ResetGame();
        HeroController.Instance.IsWin = false;
    }

    public void OnNextClick()
    {
        UIManager.Instance.PopUIPanel();
        //UIManager.Instance.PushUIPanel("TMX_2_2");
        HeroController.Instance.IsWin = false;
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIControl : UIBase {

    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
    }

    public override void DoOnPausing()
    {

    }

    public override void DoOnResuming()
    {
  
    }

    public override void DoOnExit()
    {
        gameObject.SetActive(false);
    }

    public void GoToStart()
    {
        //当栈中有超过一个面板时弹出，栈底为Home面板
        //因为reset中也要弹出一个面板，所以是>2
        while (UIManager.Instance.UIStack.Count > 2)
        {
            UIManager.Instance.PopUIPanel();
        }
        HeroController.Instance.ResetGame();
    }

    public void SetMute(bool value)
    {
        SoundManager.Instance.Mute= value;
    }

}

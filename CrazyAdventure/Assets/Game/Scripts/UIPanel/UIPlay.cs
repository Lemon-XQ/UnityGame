using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIPlay : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        UIManager.Instance.PushUIPanel("UIControl");
        gameObject.SetActive(true);
        SoundManager.Instance.PlayBGM("Audio_bgm_3");
    }

    public override void DoOnPausing()
    {
        
    }

    public override void DoOnResuming()
    {
        //复原场景
        ResetObject[] ros = GameObject.FindObjectsOfType<ResetObject>();
        for (int i = 0; i < ros.Length; i++)
        {
            ros[i].Reset();
        }
        SoundManager.Instance.PlayBGM("Audio_bgm_3");
    }

    public override void DoOnExit()
    {
        gameObject.SetActive(false);
    }

   
}

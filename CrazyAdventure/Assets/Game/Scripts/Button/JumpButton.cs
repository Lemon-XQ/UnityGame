using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// 脚本位置：UGUI按钮组件身上
/// 脚本功能：实现按钮长按状态的判断
/// </summary>

// 继承：按下，抬起和离开的三个接口
public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    // 延迟时间
    private float delay = 0.1f;

    // 按钮是否是按下状态
    private bool isDown = false;

    // 按钮最后一次是被按住状态时候的时间
    private float lastIsDownTime;

    void FixedUpdate()
    {
        // 如果按钮是被按下状态
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间
            if (Time.time - lastIsDownTime > delay)
            {
                // 触发长按方法              
                HeroController.Instance.JumpButtonClick = false;
                // 记录按钮最后一次被按下的时间
                lastIsDownTime = Time.time;
            }
        }
    }

    // 当按钮被按下后系统自动调用此方法
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        HeroController.Instance.JumpButtonClick = true;
        lastIsDownTime = Time.time;
    }

    // 当按钮抬起的时候自动调用此方法
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        HeroController.Instance.JumpButtonClick = false;
    }

    // 当鼠标从按钮上离开的时候自动调用此方法
    public void OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
        HeroController.Instance.JumpButtonClick = false;
    }

}


using UnityEngine;
using System.Collections;

public enum Grade
{
    EASY,
    NORMAL,
    HARD
}
public class GameSetting : MonoBehaviour {
    public float volume=1;
    public Grade difficulty=Grade.NORMAL;
    public bool isFullScreen=true;
    public TweenPosition startPanelTween;
    public TweenPosition optionPanelTween;

    public void OnVolumeChanged()
    {
        volume = UIProgressBar.current.value;
    }

    public void OnGradeChanged()
    {
        switch (UIPopupList.current.value.Trim())
        {
            case "EASY":
                difficulty = Grade.EASY;
                break;
            case "NORMAL":
                difficulty = Grade.NORMAL;
                break;
            case "HARD":
                difficulty = Grade.HARD;
                break;
            default:
                break;
        }
    }

    public void OnIsFullScreenChanged()
    {
        isFullScreen = UIToggle.current.value;
    }

    public void OnOptionButtonClick()
    {
        startPanelTween.PlayForward();
        optionPanelTween.PlayForward();
    }

    public void OnCompleteOptionClick()
    {
        optionPanelTween.PlayReverse();
        startPanelTween.PlayReverse();
    }
}

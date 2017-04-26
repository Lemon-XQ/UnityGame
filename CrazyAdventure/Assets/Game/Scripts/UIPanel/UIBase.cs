using UnityEngine;
using System.Collections;

public class UIBase : MonoBehaviour {

    protected CanvasGroup canvasGroup;
    protected virtual void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void DoOnEntering() { }
    public virtual void DoOnPausing() { }
    public virtual void DoOnResuming() { }
    public virtual void DoOnExit() { }
}

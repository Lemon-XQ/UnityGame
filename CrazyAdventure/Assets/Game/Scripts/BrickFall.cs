using UnityEngine;
using System.Collections;

public class BrickFall : MonoBehaviour {

    public static Vector3 DefaultPos;

    void Awake()
    {
        DefaultPos = transform.localPosition;
    }
    public void Fall()
    {
        //当主角下落超过画面最低处时砖块不必再下落，避免多次碰撞
        if (HeroController.Instance.transform.localPosition.y >= -500)
        {
            float y = Mathf.Lerp(transform.localPosition.y, transform.localPosition.y - 200, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            SoundManager.Instance.PlayAudio("Audio_brick_broken");
        }
        
    }
	
}

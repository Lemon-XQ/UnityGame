using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    public GameObject ball;
    public int number = 10;
    public GameObject[] balls;
    public Text[] texts;
    private bool NeedWin = true;

	void Update () {
        if (number > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject.Instantiate(ball);
                number--;
            }
        }
        else //球已发射完，一秒后进入下一关
        {
            if (NeedWin)
            {
                SoundManager._instance.PlayAudio("dieOrWin");
                Invoke("OnWin",1.5f);
                NeedWin = false;
            }          
        }
        for (int i = 0; i < 3; ++i)
        {
            texts[i].text = (number-i).ToString();
        }
        if (number <= 2)
        {
            balls[number].SetActive(false);       
        }
	}

    void OnWin()
    {
        gameObject.GetComponent<LevelControl>().OnWin();
    }

}

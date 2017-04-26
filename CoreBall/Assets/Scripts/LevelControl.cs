using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {
  
    public GameObject[] levelObject;
    private int level = 1;
    private Text centerText;
    private bool[] rotateDir = { true, false };

    void Awake()
    {
        centerText = GameObject.Find("centerText").GetComponent<Text>();
    }

	void Start () {
        //第一次进入游戏时存储信息
        if(PlayerPrefs.HasKey("Level")==false)
            PlayerPrefs.SetInt("Level",1);

        level = PlayerPrefs.GetInt("Level"); //读取上次的存档

        GameObject l = GameObject.Instantiate(levelObject[level-1]);
        l.transform.parent = this.transform;
        centerText.text = level.ToString();

        //控制关卡信息，如顺逆时针，旋转角度等
        GameObject.Find("CenterSphere").GetComponent<Controller>().rotate_angle += level * 3;
        GameObject.Find("CenterSphere").GetComponent<Controller>().clockwise=rotateDir[Random.Range(0,2)];
	}

    public void OnWin()
    {
        level++;
        if (level > 4)
            level = 1;       
        PlayerPrefs.SetInt("Level",level);
        SceneManager.LoadScene(0);

    }

}

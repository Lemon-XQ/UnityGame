using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {

    public float OnInterval = 3; //开启时间间隔
    public float OffInterval = 3; //关闭时间间隔
    public bool twinkle = false;//是否需要闪烁
    private Renderer laserRender;
    private float timer=0;//计时器


    void Awake()
    {
        laserRender = GetComponent<Renderer>();
        timer = Time.time;
    }

    void Update()
    {
        if (twinkle)
        {
            if (laserRender.enabled == true)
            {
                if (Mathf.Abs(Time.time - timer - OffInterval) < 0.1f)
                {
                    laserRender.enabled = false;
                    timer = Time.time;
                }
            }
            else
            {
                if (Mathf.Abs(Time.time - timer - OnInterval) < 0.1f)
                {
                    laserRender.enabled = true;
                    timer = Time.time;
                }
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (laserRender.enabled==true && other.tag == "Player")
        {
            GameController._instance.alarmOn = true;
            GameController._instance.lastPlayerPosition = other.transform.position;
        }
    }
}

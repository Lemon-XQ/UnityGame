using UnityEngine;
using System.Collections;

public class car : MonoBehaviour {

    public GameObject player;

    public Vector3 targetpos,endpos;
    private int smoothing = 2;
    public float speed = 180;
    public AudioSource audio;
    public GameObject[] wheels;
    public GameObject damper;
    private bool isRotate = true;
    private bool isReach = false;
    private bool isOut = false;
    private bool isJump = false;
   
    private float angle = 0;

	// Use this for initialization
	void Start () {
        Invoke("PlaySound",0.4f);
        Invoke("StopRotate", 1.6f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, targetpos) < 0.1f)//车到达
        {
            isReach = true;
            //playerJumpCar();//车到后玩家自动跳车
            if (isOut)
                Destroy(this.gameObject);
        }
        if (!isReach)
        {
            Vector3 cartemppos = transform.position;
            transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * smoothing);
            if (!isJump)//还没跳车则跟车一起运动
            {
                Vector3 postemp=player.transform.position;
                player.transform.position = new Vector3(postemp.x+transform.position.x-cartemppos.x,postemp.y,postemp.z);
            }
                
            if (isRotate)
             {
                 foreach (GameObject wheel in wheels)
                {
                    wheel.transform.Rotate(-Vector3.forward, speed * Time.deltaTime);
                }
             }
        }
        else//车已到达指定位置，放下挡板
        {
            if (angle < 135)//挡板旋转至左下方135度处
            {
                damper.transform.Rotate(Vector3.forward,speed*Time.deltaTime);
                angle += speed * Time.deltaTime;
            }
            //等待玩家跳车
            if(player.transform.position.y < -0.8f)//与地面距离小于0.1则判定其已落地
            {
                Invoke("DriveAway", 0.6f);
                isJump = true;
            }
                  
        }
        
            
	}

    void PlaySound()
    {
        audio.Play();
    }

    void StopRotate()
    {
        isRotate = false;
    }

    void DriveAway()
    {
        targetpos = endpos;
        isReach = false;
        isRotate = true;
        isOut = true;
    }

}

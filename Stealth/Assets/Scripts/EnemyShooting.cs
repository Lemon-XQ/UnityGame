using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    public float minDamage=40;//伤害值随距离的缩短而递增
    public Transform laserStartPos;
    
    private LineRenderer laser;
    private Animator anim;
    private bool hasShoot = false;
    private PlayerHealth health;
    private AudioSource shootAudiosource;
    private Transform playerPos;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        shootAudiosource = GetComponent<AudioSource>();
        laser = GetComponentInChildren<LineRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        //大于0.5f说明正在射击
        if (anim.GetFloat("Shot") > 0.5f && health.hp>0)
        {
            Shooting();
        }
        else
        {
            hasShoot = false;
            if (health.hp < 0)
            {
                //人物死亡后取消射击动作
                GetComponent<PlayerInSight>().isPlayerInSight = false;
                //激光复位
                laser.SetPosition(0, laserStartPos.position);
                laser.SetPosition(1, laserStartPos.position);
            }
        }
    }

    private void Shooting()
    {
        if (hasShoot == false)
        {
            //播放射击音效
            if (!shootAudiosource.isPlaying)
                shootAudiosource.Play();
            //发射激光
            Vector3 endPos = playerPos.position;
            endPos.y += 1;
            laser.SetPosition(0,laserStartPos.position);
            laser.SetPosition(1,endPos);

            //伤害值随距离的缩短而递增
            health.TakeDamage( minDamage+100-20*(transform.position-health.transform.position).magnitude );
            hasShoot = true;//防止射击一次却调用该函数多次
        }
        else
        {
            //激光复位
            laser.SetPosition(0, laserStartPos.position);
            laser.SetPosition(1,laserStartPos.position);
        }
    }

}

using UnityEngine;
using System.Collections;


public class EnemyMove : MonoBehaviour
{

    public float speed = 1;
    public float jumpSpeed = 3;
    public float walkRadius = 1f;//默认敌人可行走范围

    public PlayerState state = PlayerState.PlayerGround;
    private bool isGround = true;
    private int groundLayerMask;

    public playerGround playerGround;
    public playerDown playerDown;
    public playerJump playerJump;

    private int speedDir = -1;
    private Vector3 startPos;//敌人初始点

    void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //控制敌人只在某个范围内行走
        //if (Mathf.Abs(transform.position.x - startPos.x) > walkRadius)
        //{
        //    speedDir = -speedDir;
        //}

        

        Vector3 v = this.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().velocity = new Vector3(speedDir * speed, v.y, v.z);
        v = this.GetComponent<Rigidbody>().velocity;//暂存刚体的当前速度

        //判断是否在地面上
        RaycastHit hitinfo;
        isGround = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hitinfo, 0.2f, groundLayerMask);

        //判断当前主角的状态 跳起 正常
        if (!isGround)
        {
            state = PlayerState.PlayerJump;
        }
        else
        {          
            state = PlayerState.PlayerGround;
        }

        //控制主角的跳跃
        if (!isGround)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(v.x, jumpSpeed, v.z);
        }

        //根据状态来判定启用哪一个游戏状态
        switch (state)
        {
            case PlayerState.PlayerDown:
                playerDown.gameObject.SetActive(true);
                playerJump.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(false);
                break;
            case PlayerState.PlayerGround:
                playerDown.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(true);
                break;
            case PlayerState.PlayerJump:
                playerDown.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(true);
                playerGround.gameObject.SetActive(false);
                break;
        }

        //控制敌人的朝向
        float x = 1;
        if (this.GetComponent<Rigidbody>().velocity.x > 0.05f)//向右运动
        {
            x = -1;
        }
        else if (this.GetComponent<Rigidbody>().velocity.x < -0.05f)//向左运动（默认向左）
        {
            x = 1;
        }
        else//保持原朝向不变
        {
            x = 0;
        }
        if (x != 0)
        {
            playerGround.transform.localScale = new Vector3(x, 1, 1);
            playerJump.transform.localScale = new Vector3(x, 1, 1);
            playerDown.transform.localScale = new Vector3(x, 1, 1);
        }

        //控制敌人在idle和walk状态的切换
        if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x) > 0.05f)//运动中
        {
            playerGround.status = AnimStatus.Walk;
            //playerDown.status = AnimStatus.Walk;
        }
        else
        {
            playerGround.status = AnimStatus.Idle;
            //playerDown.status = AnimStatus.Idle;
        }
    }
}

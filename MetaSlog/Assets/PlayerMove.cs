using UnityEngine;
using System.Collections;

public enum PlayerState
{
    PlayerGround,
    PlayerDown,
    PlayerJump
}

public class PlayerMove : MonoBehaviour
{

    public float speed = 3;
    public float jumpSpeed = 3;


    public PlayerState state = PlayerState.PlayerJump;
    private bool isGround = false;
    private int groundLayerMask;

    private bool isBottomKeyClick = false;

    public playerGround playerGround;
    public playerDown playerDown;
    public playerJump playerJump;

    bool isStarted = false;
    public int killCount = 0;
    public int playBlood = 100;

    //
    public AudioClip soundKnife;
    public AudioClip soundHeroDie;
    public AudioClip soundEnemyDie;
    public AudioClip soundThrow;
    public AudioClip soundShoot;
    public AudioClip soundBoom;
    public AudioClip soundBoomMoving;
    public AudioClip jetOverBoom;
    //


    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))//按住S键不松开下蹲
        {
            isBottomKeyClick = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBottomKeyClick = false;
        }

        float h = Input.GetAxis("Horizontal");
        Vector3 v = this.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().velocity = new Vector3(h * speed, v.y, v.z);
        v = this.GetComponent<Rigidbody>().velocity;//暂存刚体的当前速度

        //判断是否在地面上
        RaycastHit hitinfo;
        isGround = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hitinfo, 0.3f, groundLayerMask);

        //判断当前主角的状态 跳起 蹲下 正常
        if (isGround == false)
        {
            state = PlayerState.PlayerJump;
        }
        else
        {
            if (isBottomKeyClick)
            {
                state = PlayerState.PlayerDown;
            }
            else
            {
                state = PlayerState.PlayerGround;
            }
        }

        //控制主角的跳跃
        if (isGround && Input.GetKeyDown(KeyCode.K))
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

        //控制主角的朝向
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

        //控制主角在idle和walk状态的切换
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

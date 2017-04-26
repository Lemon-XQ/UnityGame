using UnityEngine;
using System.Collections;

public enum ShootDir
{
    Left,
    Right,
    Top,
    //Down
}

public class playerShoot : MonoBehaviour
{

    public int shootRate = 7;//代表每秒可以射击的次数

    public playerGround playerGround;
    public playerDown playerDown;
    public playerJump playerJump;

    private float shootTimeInterval = 0;
    private float timer = 0;
    private bool canShoot = true;

    private PlayerMove playerMove;

    private bool isTopKeyDown = false;
   // private bool isBottomKeyDown = false;

    void Start()
    {
        shootTimeInterval = 1f / shootRate;//射击一次的时间间隔
        playerMove = this.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot == false)
        {
            timer += Time.deltaTime;
            if (timer >= shootTimeInterval)
            {
                canShoot = true;
                timer -= shootTimeInterval;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isTopKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isTopKeyDown = false;
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    isBottomKeyDown = true;
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    isBottomKeyDown = false;
        //}

        if (canShoot && Input.GetKeyDown(KeyCode.J))
        {
            //进行射击的操作
            this.GetComponent<AudioSource>().Play();
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            switch (playerMove.state)
            {
                case PlayerState.PlayerGround:
                    playerGround.Shoot(rigidbody.velocity.x, isTopKeyDown);
                    break;
                case PlayerState.PlayerDown:
                    playerDown.Shoot(rigidbody.velocity.x, isTopKeyDown);
                    break;
                case PlayerState.PlayerJump:
                    playerJump.Shoot(rigidbody.velocity.x, isTopKeyDown);
                    break;
            }
        }
    }
}

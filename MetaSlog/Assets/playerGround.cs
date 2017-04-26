using UnityEngine;
using System.Collections;

public enum AnimStatus
{
    Idle,
    Walk
};

public class playerGround : MonoBehaviour {

    public AnimStatus status = AnimStatus.Idle;

    public float animSpeed = 10;//默认一秒播放10帧图片
    private float animTimeInterval;//播放一帧需要的时间

    public SpriteRenderer UpRenderer;//上半身的渲染器
    public SpriteRenderer DownRenderer;//下半身的渲染器

    public Sprite[] idleUpSpriteArray;
    private int idleUpIndex = 0;
    private int idleUpLength;//数组长度
    private float Timer = 0;//计时器

    public Sprite idleDownSprite;

    public Sprite[] WalkUpSpriteArray;
    private int WalkUpIndex = 0;
    private int WalkUpLength;//数组长度

    public Sprite[] WalkDownSpriteArray;
    private int WalkDownIndex = 0;
    private int WalkDownLength;//数组长度


    public Sprite shootUpSprite;
    public Sprite shootHorizontalSprite;

    private bool shoot = false;
    private ShootDir shootDir;

    public GameObject projectilePrefab;
    public Transform shootupPos;
    public Transform shoothorizontalPos;

	// Use this for initialization
	void Start () {
        idleUpLength = idleUpSpriteArray.Length;
        animTimeInterval = 1 / animSpeed;
        WalkUpLength = WalkUpSpriteArray.Length;
        WalkDownLength = WalkDownSpriteArray.Length;
	}
	
	// Update is called once per frame
	void Update () {
        switch (status)
        {
            case AnimStatus.Idle:
                Timer += Time.deltaTime;
                if (Timer > animTimeInterval)//播放下一帧
                {
                    Timer -= animTimeInterval;
                    idleUpIndex++;//帧数自增，播放下一帧
                    idleUpIndex %= idleUpLength;//判断是否达到最大帧数
                    UpRenderer.sprite = idleUpSpriteArray[idleUpIndex];
                }
                DownRenderer.sprite = idleDownSprite;
                break;

            case AnimStatus.Walk:
                Timer += Time.deltaTime;
                if (Timer > animTimeInterval)//播放下一帧
                {
                    Timer -= animTimeInterval;
                    WalkUpIndex++;//帧数自增，播放下一帧
                    WalkDownIndex++;
                    WalkUpIndex %= WalkUpLength;//判断是否达到最大帧数
                    WalkDownIndex %= WalkDownLength;
                    UpRenderer.sprite = WalkUpSpriteArray[WalkUpIndex];
                    DownRenderer.sprite = WalkDownSpriteArray[WalkDownIndex];
                }
                break;

            default:
                break;
        }
	}

    void LateUpdate()
    {
        if (shoot)
        {
            shoot = false;
            //进行射击
            Vector3 pos = Vector3.zero;//pos表示子弹发出点
            if (shootDir == ShootDir.Top)
            {
                pos = shootupPos.position;
            }
            else if (shootDir == ShootDir.Left || shootDir == ShootDir.Right)
            {
                pos = shoothorizontalPos.position;
            }
            int z_rotation = 0;//默认向右发射子弹
            switch (shootDir)
            {
                case ShootDir.Left:
                    UpRenderer.sprite = shootHorizontalSprite;
                    z_rotation = 180;
                    break;
                case ShootDir.Right:
                    UpRenderer.sprite = shootHorizontalSprite;
                    z_rotation = 0;
                    break;
                case ShootDir.Top:
                    UpRenderer.sprite = shootUpSprite;
                    z_rotation = 90;
                    break;
                //case ShootDir.Down:
                //    z_rotation = 270;
                //    break;
                default:
                    break;
            }
            //实例化子弹
            GameObject.Instantiate(projectilePrefab, pos, Quaternion.Euler(0, 0, z_rotation));

        }
    }

    public void Shoot(float v_h, bool isTopKeyDown)
    {
        shoot = true;

        //得到射击的方向
        if (isTopKeyDown == false)
        {//不选择射击方向时默认与人物朝向一致
            if (transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                shootDir = ShootDir.Right;
            }
        }
        else
        {
                shootDir = ShootDir.Top;
            //else if (isBottomKeyDown)
            //{
            //    shootDir = ShootDir.Down;
            //}
        }
    }


}

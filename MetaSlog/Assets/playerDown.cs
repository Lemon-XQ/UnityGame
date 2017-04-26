using UnityEngine;
using System.Collections;

public class playerDown : MonoBehaviour
{
    public float animSpeed = 10;//默认一秒播放10帧图片
    private float animTimeInterval;//播放一帧需要的时间

    public SpriteRenderer UpRenderer;//上半身的渲染器
    public SpriteRenderer DownRenderer;//下半身的渲染器

    public Sprite[] UpSpriteArray;
    private int UpIndex = 0;
    private int UpLength;//数组长度
    private float Timer = 0;//计时器

    public Sprite[] DownSpriteArray;
    private int DownIndex = 0;
    private int DownLength;//数组长度


    public Sprite shootUpSprite;
    public Sprite shootHorizontalSprite;

    private bool shoot = false;
    private ShootDir shootDir;

    public GameObject projectilePrefab;
    public Transform shootupPos;
    public Transform shoothorizontalPos;

    // Use this for initialization
    void Start()
    {
        animTimeInterval = 1 / animSpeed;
        UpLength = UpSpriteArray.Length;
        DownLength = DownSpriteArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > animTimeInterval)//播放下一帧
        {
            Timer -= animTimeInterval;
            UpIndex++;//帧数自增，播放下一帧
            DownIndex++;
            UpIndex %= UpLength;//判断是否达到最大帧数
            DownIndex %= DownLength;
            UpRenderer.sprite = UpSpriteArray[UpIndex];
            DownRenderer.sprite = DownSpriteArray[DownIndex];
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
        else//同时按下W S 键时按优先级选择射击方向
        {
               shootDir = ShootDir.Top;        
        }
    }

}

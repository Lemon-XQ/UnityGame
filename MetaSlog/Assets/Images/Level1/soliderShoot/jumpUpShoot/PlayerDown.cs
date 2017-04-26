using UnityEngine;
using System.Collections;

public class PlayerDown : MonoBehaviour {
    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;
    public AnimStatus status = AnimStatus.Idle;//表示主角当前的状态

    public SpriteRenderer upRenderer;//上半身的渲染器
    public SpriteRenderer downRenderer;//下半身的渲染器

    public Sprite[] idleUpSpriteArray;
    private int idleUpIndex = 0;
    private int idleUpLength = 0;
    private float idleUpTimer = 0;

    public Sprite idleDownSprite;

    public Sprite[] walkUpSpriteArray;
    private int walkUpIndex = 0;
    private int walkUpLength = 0;
    private float walkUpTimer = 0;

    public Sprite[] walkDownSpriteArray;
    private int walkDownIndex = 0;
    private int walkDownLength = 0;
    private float walkDownTimer = 0;


    // Use this for initialization
    void Start() {
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        idleUpLength = idleUpSpriteArray.Length;
        walkUpLength = walkUpSpriteArray.Length;
        walkDownLength = walkDownSpriteArray.Length;
    }

    // Update is called once per frame
    void Update() {
        switch (status) {
            case AnimStatus.Idle:
                idleUpTimer += Time.deltaTime;
                if (idleUpTimer > animTimeInterval) {//播放下一帧
                    idleUpTimer -= animTimeInterval;//当计时器减去一个周期的时间
                    idleUpIndex++;//当帧数自增（播放下一帧）
                    idleUpIndex %= idleUpLength;//(判断是否到达最大帧数)
                    upRenderer.sprite = idleUpSpriteArray[idleUpIndex];
                }
                downRenderer.sprite = idleDownSprite;
                break;
            case AnimStatus.Walk:
                walkUpTimer += Time.deltaTime;
                if (walkUpTimer > animTimeInterval) {
                    walkUpTimer -= animTimeInterval;
                    walkUpIndex++;
                    walkUpIndex %= walkUpLength;
                    upRenderer.sprite = walkUpSpriteArray[walkUpIndex];
                }

                walkDownTimer += Time.deltaTime;
                if (walkDownTimer > animTimeInterval) {
                    walkDownTimer -= animTimeInterval;
                    walkDownIndex++;
                    walkDownIndex %= walkDownLength;
                    downRenderer.sprite = walkDownSpriteArray[walkDownIndex];
                }

                break;
        }
    }

    public void Shoot(float v_h, bool isTopKeyDown, bool isBottomKeyDown) {
    }
}

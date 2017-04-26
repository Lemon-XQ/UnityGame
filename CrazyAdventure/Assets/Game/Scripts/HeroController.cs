using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

    public float MoveSpeed = 2;
    public float JumpPower = 300;
    public Vector2 StartPos;

    private Rigidbody2D rigidBody;
    private HeroAnimation heroAnim;
    private float UpForce = 0;

    private static HeroController instance;
    public static HeroController Instance
    {
        get { return instance; }
    }

    public bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        set
        {
            if (isDead != value)//如果死亡状态与当前一致则不用修改
            {
                isDead = value;
                if (value)
                {
                    UIManager.Instance.PushUIPanel("UILose");
                    SoundManager.Instance.PlayAudio("Audio_ao");
                }
            }
        }
    }

    public bool isWin = false;
    public bool IsWin
    {
        get { return isWin; }
        set
        {
            if (isWin != value)
            {
                isWin = value;
                if (value)
                {
                    UIManager.Instance.PushUIPanel("UIWin");
                    SoundManager.Instance.PlayAudio("Audio_congratulations");
                }
            }
        }
    }

    //UI按钮是否按下
    public bool LeftButtonClick = false;

    public bool RightButtonClick = false;

    public bool JumpButtonClick = false;

	void Awake () {
        instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        heroAnim = GetComponent<HeroAnimation>();
        StartPos = transform.localPosition;
	}

    void Update()
    {
        if (IsDead)
        {
            heroAnim.DieState();
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            return;
        }
        if (transform.localPosition.y < -500)
        {
            IsDead = true;
            return;
        }

        if (IsWin)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            return;
        }

        float h = Input.GetAxis("Horizontal");

        if (LeftButtonClick)
            h = -1.0f;
        else if (RightButtonClick)
            h = 1.0f;
        if (rigidBody.velocity.y == 0) //Jump
        {
            if (Input.GetKeyDown(KeyCode.Space) || JumpButtonClick)
            {
                heroAnim.JumpState(true, h < 0);
                if (UpForce == 0)
                {
                    rigidBody.AddForce(Vector2.up * JumpPower);
                    UpForce = JumpPower;
                }
            }
            else
            {
                heroAnim.JumpState(false, false);
                UpForce = 0;
            }
        }

        if (Mathf.Abs(h - 0) > 0.01f)
        {
            rigidBody.velocity = new Vector2(h * MoveSpeed, rigidBody.velocity.y);
            heroAnim.RunState(h < 0);
        }
        else 
        {
            heroAnim.IdleState();
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }                  

    }


    public void ResetGame()
    {
        UIManager.Instance.PopUIPanel();
        transform.localPosition = StartPos;
        heroAnim.IdleState(); 
        IsDead = false;
             
    }
}

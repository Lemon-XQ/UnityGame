using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float walk_speed = 20;
    public float jump_speed = 10;
    private Rigidbody2D rigidBody;
    private Animator anim;
    public bool isSlide = false;
    public bool isGround = false;
    public bool isWall = false;

    Transform wall;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	void Update () 
    {
        float h = Input.GetAxis("Horizontal");

        if (!isSlide)
        {
            rigidBody.velocity = new Vector2( h * walk_speed ,rigidBody.velocity.y);

            //控制朝向
            if (h > 0.1f)
                transform.localScale = new Vector3(1,1,1);
            else if(h < -0.1f)
                transform.localScale = new Vector3(-1, 1, 1);

            anim.SetFloat("horizontal",Mathf.Abs(h)); //播放idle或run动画

            //控制跳跃
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.playAudio("Sounds/Jump");
                Vector2 velocity = rigidBody.velocity;
                velocity.y = jump_speed;
                rigidBody.velocity = velocity;
                //rigidBody.AddForce(Vector2.up * jumpPower);
                if (isWall)
                    rigidBody.gravityScale = 0.5f;
                
            }

            anim.SetFloat("vertical",rigidBody.velocity.y);//控制播放jump动画
        }


        if (isWall == false || isGround == true)
            isSlide = false;

	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
           // rigidBody.gravityScale = 15;
        }
              
        //待解决bug:人物在墙角起跳时由于gravityscale改变导致起跳速度很大
        if (collision.collider.tag == "Wall")
        {
            wall = collision.collider.transform;
            isWall = true;
            rigidBody.velocity = Vector2.zero;

            rigidBody.gravityScale = 0.5f;
                
        }

        anim.SetBool("isGround",isGround);
        anim.SetBool("isWall",isWall);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            isGround = false;
        if (collision.collider.tag == "Wall")
        {
            isWall = false;
            rigidBody.gravityScale = 1;
        }

        anim.SetBool("isGround", isGround);
        anim.SetBool("isWall", isWall);
    }

    void ChangeDir() 
    {
        isSlide = true;
        //控制在墙上滑行时朝向
        if (wall.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

}

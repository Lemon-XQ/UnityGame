using UnityEngine;
using System.Collections;

public enum EnemyType{
    Enemy0,
    Enemy1
}


public class EnemyAI : MonoBehaviour {

    //敌人类型枚举 有策划人员选择
	public EnemyType enemyType = EnemyType.Enemy1;
 
	//主角游戏对象
	public GameObject player;
 
    //代码控制动画
    private PlayerMove playMove;
    private int groundLayMask;
    public enum EnemyState { IDLE, WALK, KILL, DEAD }

    public EnemyState state = EnemyState.WALK;

    public SpriteRenderer enemy1SpriteRenderer;
    public Sprite[] enemy1WalkSprites;
    private int spriteWalkCount;
    private int walkIndex = 0;
    private float walkTimer = 0;
    private float animWalkSpeed = 10f;
    private float animWalkInterval = 0;

    public Sprite[] enemy1KillSprites;
    private int spriteKillCount;
    private float animKillSpeed = 6;
    private float animKillInterval = 0;
    private float killTimer = 0;
    private int killIndex;

    public Sprite[] enemy1IdleSprites;
    private int spriteIdleCount;
    private float animIdleSpeed = 7f;
    private float animIdleInterval = 0;
    private float idleTimer = 0;
    private int idleIndex;

    public Sprite[] enemy1PartrolSprites;
    private int spritePartrolCount;
    private float animPartrolSpeed = 10f;
    private float animPartrolInterval = 0;
    private float partrolTimer = 0;
    private int partrolIndex;

    public Sprite[] enemy1DeadSprites;
    private int spriteDeadCount;
    private float animDeadSpeed = 10f;
    private float animDeadInterval = 0;
    private float deadTimer = 0;
    private int deadIndex;

    private Vector3 localScale;

	void Start ()
	{
        groundLayMask = LayerMask.GetMask("Ground");
        player = GameObject.Find("Player");
        playMove = player.GetComponent<PlayerMove>();
        animWalkInterval = 1f / animWalkSpeed;
        animKillInterval = 1f / animKillSpeed;
        animIdleInterval = 1f / animIdleSpeed;
        animDeadInterval = 1f / animDeadSpeed;
        animPartrolInterval = 1f / animPartrolSpeed;
        spriteWalkCount = enemy1WalkSprites.Length;
        spriteKillCount = enemy1KillSprites.Length;
        spriteIdleCount = enemy1IdleSprites.Length;
        spriteDeadCount = enemy1DeadSprites.Length;
        spritePartrolCount = enemy1PartrolSprites.Length;
	}
 
	void Update ()
	{
		//根据策划选择的敌人类型 这里面会进行不同的敌人AI
		switch(enemyType)
		{
		    case EnemyType.Enemy0:
			    updateEnemyType0();
			    break;
		    case EnemyType.Enemy1:
                UpdateEnemyState();
                //updateEnemyType1();
			    break;
		}
	}
 
	//更新第一种敌人的AI
	void updateEnemyType0()
	{
	
	}

    //在这里更新敌人状态
    void UpdateEnemyState()
    {
        if (player.transform.position.x > transform.position.x + 0.05f)
        {
            localScale = new Vector3(-1, 1, 1);
        }
        if (player.transform.position.x < transform.position.x - 0.05f)
        {
            localScale = new Vector3(1, 1, 1);
        }
        transform.localScale = localScale;
        if (playMove.playBlood <= 0)
        {
            state = EnemyState.IDLE;
            Idle();
            return;
        }
        if (state == EnemyState.DEAD)
        {
            Dead();
            return;
        }
        //判断敌人与主角之间的距离
        float distance = Vector3.Distance(player.transform.position, transform.position);
        //当敌人与主角的距离小于3 敌人将开始面朝主角追击
        if (distance <= 3)
        {
            //当敌人与主角的距离小于1 敌人将开始面朝主角攻击
            if (distance <= 1)
            {
                setEnemyState(EnemyState.KILL);
            }
            else
            {
                //否则敌人将开始面朝主角追击
                setEnemyState(EnemyState.WALK);
            }
        }
        else
        {
            //敌人攻击主角时 主角迅速奔跑 当它们之间的距离再次大于3的时候 敌人将再次进入正常状态 开始思考
            if (state == EnemyState.WALK || state == EnemyState.KILL)
            {
                setEnemyState(EnemyState.IDLE);
            }
        }

    }

	void setEnemyState(EnemyState newState)
	{
        //if(state == newState)
        //    return;
		state = newState;

        //if (player.transform.position.x > transform.position.x + 0.05f)
        //{
        //    localScale = new Vector3(-1, 1, 1);
        //}
        //if (player.transform.position.x < transform.position.x - 0.05f)
        //{
        //    localScale = new Vector3(1, 1, 1);
        //}
        //transform.localScale = localScale;
        //if (playMove.playBlood <= 0)
        //{
        //    state = EnemyState.IDLE;
        //    Idle();
        //    return;
        //}
        //if (state == EnemyState.DEAD)
        //{
        //    Dead();
        //    return;
        //}
        //float dis = Vector3.Distance(transform.position, player.transform.position);
        //if (dis > 3f)
        //{
        //    state = EnemyState.IDLE;
        //}
        //else if (dis < 0.5f)
        //{
        //    state = EnemyState.KILL;
        //}
        //else
        //{
        //    state = EnemyState.WALK;
        //}

		switch(state)
		{
		    case EnemyState.IDLE:
                Idle();
                Debug.Log("Idle...");
			    break;
		    case EnemyState.KILL:
                Kill();
                Debug.Log("Attacking...");
			    break;
		    case EnemyState.WALK:
                Walk();
                Debug.Log("Walking...");
			    break;
		}
 
	}

    /// <summary>
    /// 站岗巡逻状态标识
    /// </summary>
    private bool isPartrol = false;
    private float idleKindTimer = 0;
    private bool partrolLeft = false;
    private void Idle()
    {
        idleKindTimer += Time.deltaTime;

        if (idleKindTimer > 5)
        {
            idleKindTimer -= 5;
            if (Random.Range(0, 2) == 1)
            {
                partrolLeft = !partrolLeft;
            }
            if (Random.Range(0, 2) == 1)
            {
                isPartrol = !isPartrol;
            }
        }
        //站岗or巡逻
        if (!isPartrol)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > animIdleInterval)
            {
                idleTimer -= animIdleInterval;
                idleIndex++;
                idleIndex %= spriteIdleCount;
                enemy1SpriteRenderer.sprite = enemy1IdleSprites[idleIndex];
            }
        }
        else
        {
            partrolTimer += Time.deltaTime;

            if (partrolTimer > animPartrolInterval)
            {
                partrolTimer -= animPartrolInterval;
                partrolIndex++;
                partrolIndex %= spritePartrolCount;
                enemy1SpriteRenderer.sprite = enemy1PartrolSprites[partrolIndex];
            }
            if (partrolLeft)
            {
                transform.localScale = new Vector3(1, 1, 1);
                RaycastHit hitLeft;
                if (!Physics.Raycast(transform.position, Vector3.left, out hitLeft, 0.2f, groundLayMask))
                    transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                RaycastHit hitRight;
                if (!Physics.Raycast(transform.position, Vector3.right, out hitRight, 0.2f, groundLayMask))
                    transform.Translate(Vector3.right * 1 * Time.deltaTime);
            }
        }
    }

    private void Dead()
    {
        deadTimer += Time.deltaTime;
        if (deadTimer > animDeadInterval)
        {
            deadTimer -= animDeadInterval;
            deadIndex++;
            deadIndex %= spriteDeadCount;
            if (deadIndex == 1)
            {
                AudioSource.PlayClipAtPoint(playMove.soundEnemyDie, new Vector3(0, 0, 0));
            }
            if (deadIndex == 0)
            {
                Destroy(gameObject);
            }
            enemy1SpriteRenderer.sprite = enemy1DeadSprites[deadIndex];
        }
        Debug.Log("Dying...");
    }

    private void Kill()
    {
        killTimer += Time.deltaTime;

        if (killTimer > animKillInterval)
        {
            killTimer -= animKillInterval;
            killIndex++;
            killIndex %= spriteKillCount;
            enemy1SpriteRenderer.sprite = enemy1KillSprites[killIndex];

            if (killIndex == 2)
            {
                playMove.playBlood -= 5;
                AudioSource.PlayClipAtPoint(playMove.soundKnife, new Vector3(0, 0, 0));
            }
        }
        if (partrolLeft)
        {
            transform.Translate(Vector3.left * 0.1f * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * 0.1f * Time.deltaTime);
        }
    }

    private void Walk()
    {
        walkTimer += Time.deltaTime;

        if (walkTimer > animWalkInterval)
        {
            walkTimer -= animWalkInterval;
            walkIndex++;
            walkIndex %= spriteWalkCount;
            enemy1SpriteRenderer.sprite = enemy1WalkSprites[walkIndex];
        }
        if (transform.position.x >= player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            RaycastHit hitLeft;
            if (!Physics.Raycast(transform.position, Vector3.left, out hitLeft, 0.2f, groundLayMask))
                transform.Translate(Vector3.left * 1 * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            RaycastHit hitRight;
            if (!Physics.Raycast(transform.position, Vector3.right, out hitRight, 0.2f, groundLayMask))
                transform.Translate(Vector3.right * 1 * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "projectile(Clone)" && state != EnemyState.DEAD)
        {
            state = EnemyState.DEAD;
            playMove.killCount++;
            Debug.Log("Dead...");
        }
    }
}


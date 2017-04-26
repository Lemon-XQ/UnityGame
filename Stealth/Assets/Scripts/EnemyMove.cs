using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Transform[] waypoints;
    public float patrolTime = 3f;//每次巡逻停留时间
    public float chaseTime = 3f;//放弃追逐时间

    private float patrolTimer = 0;//巡逻计时器
    private float chaseTimer = 0;//追逐计时器
    private NavMeshAgent navAgent;
    private int index = 0;
    private PlayerInSight sight;
    private PlayerHealth health;


	void Awake () {
	    navAgent=this.GetComponent<NavMeshAgent>();
        navAgent.destination = waypoints[index].position;
        sight = this.GetComponent<PlayerInSight>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

	void Update () {
        if (sight.isPlayerInSight && health.hp > 0)
        {
            //print("shoot");
            shoot();
        }
        else if (sight.alarmPos != Vector3.zero && health.hp > 0)
        {
            //print("chase");
            chase();
        }
        else
        {
            patrol();
        }
        
	}

    private void patrol()
    {
        navAgent.speed = 1.5f;
        navAgent.destination = waypoints[index].position;
        navAgent.updateRotation = false;

        //判断敌人是否已到下一个巡逻点，是则进入休息
        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolTime)
            {
                index++;
                index %= waypoints.Length;//防止数组溢出，实现往复巡逻
                navAgent.destination = waypoints[index].position;
                patrolTimer = 0;
            }
            
        }
    }

    private void chase()
    {
        navAgent.speed = 5;
        navAgent.destination = sight.alarmPos;
        navAgent.updateRotation = false;

        if (navAgent.remainingDistance < 1f)//离目标足够近时开始计时
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseTime)//放弃追逐，警报停止
            {
                //print("giveup");
                sight.alarmPos = Vector3.zero;
                GameController._instance.alarmOn = false;
                GameController._instance.lastPlayerPosition = Vector3.zero;
            }
        }
    }

    public void shoot()
    {
        //不能停掉navAgent...要不然敌人就再也不会追主角了，也不会自己回去巡逻了。。设置destination也没用
       // navAgent.Stop();
    }

}

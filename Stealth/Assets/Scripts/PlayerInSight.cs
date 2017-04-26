using UnityEngine;
using System.Collections;

public class PlayerInSight : MonoBehaviour {

    public bool isPlayerInSight = false;
    //单独存储每个敌人最后发现玩家的位置，当玩家触发警报系统或其他敌人看到时，
    //这个独立变量将等于全局变量，敌人会全部向这个点集合。
    //如果敌人只是听见脚步声/喊叫声，玩家位置会单独储存在这个变量中
    //并不会改变全局变量，而惊动其他敌人
    public Vector3 alarmPos = Vector3.zero;//单独存储的玩家位置，局部变量

    public float viewField = 120;//视野范围

    private Animator playerAnim;
    private SphereCollider sphereCollider;
    private NavMeshAgent navAgent;
    private Vector3 preLastPlayerPosition;//上一帧玩家被发现的位置

    void Awake()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        sphereCollider = this.GetComponentInChildren<SphereCollider>();
        navAgent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        preLastPlayerPosition = GameController._instance.lastPlayerPosition;
    }

    void Update()
    {
        //首先检测玩家位置全局变量是否改变，如果改变，那么独立变量也随之改变
        //实时更新，保证alarmpos始终与gamecontroller里的一致
        if (GameController._instance.lastPlayerPosition != preLastPlayerPosition)
        {
            alarmPos = GameController._instance.lastPlayerPosition;
            preLastPlayerPosition = GameController._instance.lastPlayerPosition;
        }
    }

    //如果playerInSight为true必须满足三个条件
    //1、玩家在触发范围内2、玩家在敌人面前，并且在敌人视线延长范围内
    //3、敌人与玩家之间没有障碍物遮挡视线
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //判断主角是否在敌人视野范围内
            Vector3 dir = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward,dir);
            //射线检测
            RaycastHit hitInfo;
            bool res = Physics.Raycast(transform.position + Vector3.up, 
                                       other.transform.position - transform.position, 
                                       out hitInfo);
            if (angle < 0.5f * viewField && (res == false || hitInfo.collider.tag == "Player"))
            //在视野范围内且敌人与主角之间无其他遮挡物
            {
                isPlayerInSight = true;
                alarmPos = other.transform.position;
                GameController._instance.alarmOn = true;
            }
            else
            {
                isPlayerInSight = false;
            }

            //判断是否听到主角脚步声
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("LocoMotion"))//主角发出脚步声
            {
                NavMeshPath path = new NavMeshPath();
                //计算敌人所处位置到主角位置的最短路径
                if (navAgent.CalculatePath(other.transform.position, path))
                {
                    Vector3[] wayPoints = new Vector3[path.corners.Length+2];//加上起点、终点
                    wayPoints[0] = transform.position;
                    wayPoints[wayPoints.Length - 1] = other.transform.position;
                    for (int i = 1; i < wayPoints.Length-1; i++)
                    {
                        wayPoints[i] = path.corners[i - 1];
                    }

                    //计算最短路径长度是否已超过trigger范围
                    float length = 0;
                    for (int i = 1; i < wayPoints.Length; i++)
                    {
                        length += (wayPoints[i] - wayPoints[i - 1]).magnitude;
                    }
                    if (length <= sphereCollider.radius)
                    {
                        alarmPos = other.transform.position;
                    }
                }
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInSight = false;
        }
    }

}

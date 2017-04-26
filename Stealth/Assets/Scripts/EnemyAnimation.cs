using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

    public float angularDampTime = 0.4f;
    public float speedDampTime = 0.4f;
    public float deadZone = 5f;

    private Animator anim;
    private NavMeshAgent nav;
    private PlayerInSight sight;
    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = this.GetComponent<Animator>();
        nav=this.GetComponent<NavMeshAgent>();
        sight = this.GetComponent<PlayerInSight>();
        nav.updateRotation = false;
        deadZone *= Mathf.Deg2Rad;
    }

    //解决敌人有时候飘着走的bug（受NavMeshAgent控制）
    void OnAnimatorMove()
    {
        // 设置NavMeshAgent的速度 等于上一帧的移动速度
        nav.velocity = anim.deltaPosition / Time.deltaTime;

        // 敌人转向由动画控制
        transform.rotation = anim.rootRotation;
    }



    void Update()
    {
         NavAnimSetup ();

    //    //初始时以及巡逻停留时velocity为0
    //    if (nav.desiredVelocity == Vector3.zero )
    //    {
    //        anim.SetFloat("Speed", 0, speedDampTime, Time.deltaTime);
    //        anim.SetFloat("AngularSpeed",0,angularDampTime,Time.deltaTime);
    //    }
    //    else
    //    {
    //        //print(nav.desiredVelocity.magnitude);

    //        float angle = Vector3.Angle(transform.forward,nav.desiredVelocity);
    //        float angle2rad=0;
    //        angle2rad = Mathf.Deg2Rad * angle;//角度转弧度
    //        //因为给转向设置了缓冲时间，所以敌人在转至正确方向后不会立刻停止转向。
    //        //而是会转过一点距离，然后再向反方向转动校正方向，这会让敌人在转弯后沿曲线移动
    //        //为了防止这种情况发生，加一个判断条件，检测角度是否小于“deadZone”
    //        //deadZone = deadZone * Mathf.Deg2Rad;
    //        //if (Mathf.Abs(angle2rad) < deadZone)
    //        //{
    //        //    //不使用动画控制器控制敌人朝向，把“angle”设为0，并让敌人朝向期望速度方向
    //        //    transform.LookAt(transform.position + nav.desiredVelocity);
    //        //    angle2rad = 0f;
    //        //}
    //        //根据夹角确定转向
    //        if (angle2rad > 90*Mathf.Deg2Rad)
    //        {
    //            anim.SetFloat("Speed", 0, speedDampTime, Time.deltaTime);
    //        }
    //        else //由投影确定速度大小
    //        {
    //            Vector3 projection = Vector3.Project(nav.desiredVelocity,transform.forward);
    //            anim.SetFloat("Speed",projection.magnitude,speedDampTime,Time.deltaTime);
    //        }
    //        //angle2rad = Mathf.Deg2Rad * angle;//角度转弧度

    //        Vector3 crossRes = Vector3.Cross(transform.forward,nav.desiredVelocity);
    //        //叉乘所得向量指向屏幕内则说明desire在当前左侧，需要左转
    //        if (crossRes.y < 0)
    //        {
    //            angle2rad = -angle2rad;
    //        }

    //        anim.SetFloat("AngularSpeed", angle2rad, angularDampTime, Time.deltaTime);
    //    }
    //    anim.SetBool("playerInSight", sight.isPlayerInSight);
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;
        //判断玩家是否被敌人发现
        if (sight.isPlayerInSight)
        {
            //如果玩家被发现，那么要让敌人停止移动，把速度设为0
            speed = 0f;
            //计算敌人应该面对的方向，和敌人当前面对的方向之间的角度
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            //玩家没有被发现，速度基于“Nav Mesh Agent”的期望速度
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            //因为给转向设置了缓冲时间，所以敌人在转至正确方向后不会立刻停止转向。
            //而是会转过一点距离，然后再向反方向转动校正方向，这会让敌人在转弯后沿曲线移动
            //为了防止这种情况发生，加一个判断条件，检测角度是否小于“deadZone”
            //if (Mathf.Abs(angle) < deadZone)
            //{
            //    //不使用动画控制器控制敌人朝向，把“angle”设为0，并让敌人朝向期望速度方向
            //    transform.LookAt(nav.desiredVelocity);
            //    angle = 0f;
            //}
        }
        anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        anim.SetFloat("AngularSpeed", angle, angularDampTime, Time.deltaTime);
        anim.SetBool("playerInSight", sight.isPlayerInSight);

    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        //“toVector”参数等于“Nav Mesh Agent”中的期望速度向量
        //如果期望速度为0，那么方向为0，并让函数返回0
        if (toVector == Vector3.zero)
        {
            return 0;
        }
        //角度的绝对值
        float angle = Vector3.Angle(fromVector, toVector);
        //判断这个角度是在当前方向左边还是右边
        //计算着两个向量的向量积，并得到法向量。
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        //如果两个向量方向相同，结果会大于0
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        //把角度转换为弧度
        angle *= Mathf.Deg2Rad;
        return angle;
    }

}

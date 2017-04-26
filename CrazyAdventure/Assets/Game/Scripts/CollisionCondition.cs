using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CollisionCondition : MonoBehaviour {

    public bool NeedDie = false;
    public bool NeedHead = false;
    public bool NeedWin = false;
    public bool NeedSave = false;
    public bool NoFall = false;//为true时下落时不能产生碰撞

    public UnityEvent OnTriggerHandler;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (NeedSave)
            HeroController.Instance.StartPos = transform.localPosition;
        if (NoFall && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y <= 0)
            return;
        if (NeedDie)
        {
            HeroController.Instance.IsDead = true;
        }
        else if (NeedWin)
        {
            HeroController.Instance.IsWin = true;
        }
        OnTriggerHandler.Invoke();
        
    }

    public UnityEvent OnCollisionHandler;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //当需要头部碰撞才触发的collider被其他部位触发时会无视该触发/碰撞事件
        if (NeedHead && collision.collider.name != "Head" )
            return;  
        
        OnCollisionHandler.Invoke();    

        if (NeedDie)
        {
            HeroController.Instance.IsDead = true;
        }
        else if (NeedWin)
        {
            HeroController.Instance.IsWin = true;                
        }
        
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ResetObject : MonoBehaviour {

    public UnityEvent OnResetHandler;

    public void Reset()
    {
        if (gameObject.name == "DynamicBricks")
            transform.localPosition = BrickFall.DefaultPos;
        else if (gameObject.name == "Bomb")
        {
            transform.localPosition = FlyBomb.DefaultPos;
            Vector3 rotation = transform.localEulerAngles;
            rotation.z = 0; // 在这里修改坐标轴的值
            transform.localEulerAngles = rotation;

        }
            
        OnResetHandler.Invoke();
    }
}

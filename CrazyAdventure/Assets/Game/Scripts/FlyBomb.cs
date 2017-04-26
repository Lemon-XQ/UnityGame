using UnityEngine;
using System.Collections;

public class FlyBomb : MonoBehaviour {

    private float TranslateSpeed=0.1f;
    private float beginTime;
    public static Vector3 DefaultPos;

    void OnEnable()
    {
        DefaultPos = transform.localPosition;
        beginTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        
        Vector3 rotation = transform.localEulerAngles;

        if (Time.time - beginTime < 0.5f)
            rotation.z = 0;
        else if (Time.time - beginTime < 2.0f)
            rotation.z = -90; // 在这里修改坐标轴的值
        else if (Time.time - beginTime < 2.8f)
            rotation.z = 180; // 在这里修改坐标轴的值
        else if (Time.time - beginTime < 5.0f)
            rotation.z = 90; // 在这里修改坐标轴的值
        else
            gameObject.GetComponent<FlyBomb>().enabled = false;

        transform.Translate(Vector3.up * TranslateSpeed);
        transform.localEulerAngles = rotation;
    }

}

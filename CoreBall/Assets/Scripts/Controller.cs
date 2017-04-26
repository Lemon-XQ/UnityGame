using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public bool switcher=true;
    public float rotate_angle = 30;
    public bool clockwise = true;//默认顺时针旋转
	

	void Update () {
	    if(switcher){
            if (clockwise)
            {
                transform.Rotate(-Vector3.forward,rotate_angle*Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward, rotate_angle * Time.deltaTime);
            }
        }
	}

    public void OnStart()
    {
        switcher = true;
    }

    public void OnStop()
    {
        switcher = false;
    }

}

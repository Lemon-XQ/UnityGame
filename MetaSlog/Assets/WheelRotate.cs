using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour {

    public float speed = 180;
    private bool isRotate = true;

	// Use this for initialization
	void Start () {
        Invoke("StopRotate",1.6f);
	}
	
	// Update is called once per frame
	void Update () {
        if(isRotate)
           transform.Rotate(-Vector3.forward,speed*Time.deltaTime);
	}

    void StopRotate()
    {
        isRotate = false;
    }
}

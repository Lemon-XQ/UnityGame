using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 90;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}

    public void ChangeSpeed(float speedNew)
    {
        this.speed = speedNew;
    }
}

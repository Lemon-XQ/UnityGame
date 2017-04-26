using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            float x = Mathf.Lerp(Camera.main.transform.position.x, player.position.x, 1.0f);
           
            x = Mathf.Clamp(x, 7.4f, 33.12f);//控制摄像机移动范围

            Camera.main.transform.localPosition = new Vector3(x, Camera.main.transform.position.y,
                Camera.main.transform.position.z);


        }
        
	}
}

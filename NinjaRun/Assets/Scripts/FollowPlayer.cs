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
      //  float dis = transform.position.x - player.position.x;
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        transform.position = pos;
    }
}

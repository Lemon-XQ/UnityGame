using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private Vector3 offset;
    private Transform player;
    private Vector3 camPos;


	void Start () {
        camPos = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = this.transform.position - player.position;
	}
	

	void Update () {
        transform.position = player.position + offset;
	}
}

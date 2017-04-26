using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    private Vector3 offset;


	// Use this for initialization
	void Start () {
        offset = transform.position - (player1.transform.position + player2.transform.position) / 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (player1 == null || player2 == null) return;
        transform.position = offset + (player1.transform.position + player2.transform.position) / 2;
        float distance = Vector3.Distance(player1.transform.position,player2.transform.position);
        float size = distance * 0.72f;
        Camera.main.orthographicSize = size;
	}
}

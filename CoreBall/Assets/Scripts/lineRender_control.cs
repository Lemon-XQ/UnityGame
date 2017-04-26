using UnityEngine;
using System.Collections;

public class lineRender_control : MonoBehaviour {

    private LineRenderer line;

	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	void Update () {
        line.SetPosition(0,Vector3.zero);
        line.SetPosition(1,transform.position);

	}
}

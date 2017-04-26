using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {

    public float speed = 10;

    void Start() {
        Destroy(this.gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
}

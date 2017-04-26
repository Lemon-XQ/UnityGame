using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour {

    public float rotate_angle;

	// Update is called once per frame
	void Update () {
        transform.Rotate(-Vector3.forward,rotate_angle*Time.deltaTime);
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            AudioManager.instance.playAudio("Sounds/Collectible");
            Destroy(this.gameObject);
        }
    }


}

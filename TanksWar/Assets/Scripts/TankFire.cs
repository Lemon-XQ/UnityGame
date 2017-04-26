using UnityEngine;
using System.Collections;

public class TankFire : MonoBehaviour {

    public GameObject missile;
    public float missileSpeed = 10;
    public AudioClip FireAudio;
    public AudioSource audiosource;
    public GameObject arrow;

    private int number;
    private Transform firePosition;

	// Use this for initialization
	void Start () {
        number = gameObject.GetComponent<TankMovement>().number;
        firePosition = transform.Find("firePos");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("FirePlayer" + number))
        {
            arrow.SetActive(true);
            GameObject go = GameObject.Instantiate(missile, firePosition.position, firePosition.rotation) as GameObject;
            audiosource.clip = FireAudio;
            audiosource.Play();
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * missileSpeed;
        }
        else if(Input.GetButtonUp("FirePlayer" + number))
            arrow.SetActive(false);
	}
}

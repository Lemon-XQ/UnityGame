using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource audiosource;
    public float angularSpeed = 10;
    public float walkSpeed = 3;
    public int number;//坦克编号
    public AudioClip IdleAudio;
    public AudioClip DrivingAudio;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("HorizontalPlayer"+number);
        rigidBody.angularVelocity = transform.up * angularSpeed * h;

        float v = Input.GetAxis("VerticalPlayer"+number);
        rigidBody.velocity = transform.forward * walkSpeed * v;

        if(Mathf.Abs(h)>0.1f || Mathf.Abs(v)>0.1f){
            audiosource.clip = DrivingAudio;
            if(audiosource.isPlaying==false)
               audiosource.Play();
        }
        else
        {       
            audiosource.clip = IdleAudio;
            if (audiosource.isPlaying == false)
                audiosource.Play();
        }
            

	}
}

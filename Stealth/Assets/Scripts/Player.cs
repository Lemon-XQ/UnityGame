using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float moveSpeed = 1;
    public float rotateSpeed = 1;
    public bool hasKey = false;

    private Animator anim;

	
	void Awake () {
        anim = GetComponent<Animator>();
	}
	

	void Update () {

        //处于死亡状态时改变标志位防止反复播放死亡动画
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
        {
            //print("dying");

            anim.SetBool("IsDead", true);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsSneak",true);
        }
        else
        {
            anim.SetBool("IsSneak",false);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(v) > 0.1f || Mathf.Abs(h) > 0.1f)
        {
           float newSpeed = Mathf.Lerp(anim.GetFloat("Speed"),5.6f,moveSpeed*Time.deltaTime);
           anim.SetFloat("Speed",newSpeed);

           Vector3 targetDir = new Vector3(h,0,v);

           Quaternion newRotation = Quaternion.LookRotation(targetDir, Vector3.up);
           transform.rotation = Quaternion.Lerp(transform.rotation,newRotation,rotateSpeed*Time.deltaTime);
        }
        else
            anim.SetFloat("Speed", 0);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("LocoMotion") )
        {
            PlayFootAudio();
        }
        else
        {
            StopFootAudio();
        }

	}

    private void PlayFootAudio()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void StopFootAudio()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

}

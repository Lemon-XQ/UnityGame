using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public bool requireKey = false;
    public AudioSource musicDeny;
    public AudioSource musicOpen;

    private Animator anim;
    private GameObject player;


    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (anim.IsInTransition(0))//处于开关状态时播放音效
        {
            if(!musicOpen.isPlaying)
                musicOpen.Play();
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (requireKey)
        {
            if (other.tag == "Player")
            {
                if (player.GetComponent<Player>().hasKey)
                {
                    anim.SetBool("Close",false);
                }
                else
                {
                    if(!musicDeny.isPlaying)
                        musicDeny.Play();
                }
            }         
        }
        else
        {
            if (other.tag == "Player" || other.tag=="Enemy")
            {
                anim.SetBool("Close",false);
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
         anim.SetBool("Close", true);
    }

}

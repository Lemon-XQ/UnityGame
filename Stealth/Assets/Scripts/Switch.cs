using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public GameObject laser;
    public Material unlock;
    public GameObject screen;

    

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Z))//按下z键激活开关
            {
                laser.SetActive(false);
                screen.GetComponent<Renderer>().material = unlock;
                if(!GetComponent<AudioSource>().isPlaying)
                     GetComponent<AudioSource>().Play();
            }
        }
    }

}

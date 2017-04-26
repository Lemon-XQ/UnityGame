using UnityEngine;
using System.Collections;

public class CCTVCam : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController._instance.alarmOn = true;
            GameController._instance.lastPlayerPosition = other.transform.position;
        }
    }
}

using UnityEngine;
using System.Collections;

public class KeyCard : MonoBehaviour {

    public AudioClip pickUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().hasKey = true;           
            AudioSource.PlayClipAtPoint(pickUp,transform.position,1);
            Destroy(this.gameObject);
        }
    }
}

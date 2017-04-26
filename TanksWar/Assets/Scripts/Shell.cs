using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

    public float time;
    public GameObject ShellExplosionPrefab;
    public AudioClip ShellExplosionAudio;

    public AudioSource audiosource;

     void OnCollisionEnter(Collision collision)
    {
        audiosource.clip = ShellExplosionAudio;
        audiosource.Play();
        //AudioSource.PlayClipAtPoint(ShellExplosionAudio,transform.position,1.0f);
        Destroy(this.gameObject);
        GameObject go = GameObject.Instantiate(ShellExplosionPrefab,transform.position,transform.rotation) as GameObject;
        Destroy(go,time);
        if (collision.collider.tag == "Tank")
        {
            collision.collider.SendMessage("TakeDamage");
        }
    }
}

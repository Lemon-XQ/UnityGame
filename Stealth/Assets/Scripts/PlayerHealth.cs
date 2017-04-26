using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float hp = 100;
    public AudioSource audioSource_dead;
    public AudioClip deadAudio;

    private Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("NeedDead", false);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //print("dead");
            anim.SetBool("NeedDead", true);
            if(!audioSource_dead.isPlaying)
                audioSource_dead.Play();
           StartCoroutine(ReloadScene());//启动协程            
        }

    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}

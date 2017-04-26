using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {

    public float hp = 100;
    public GameObject TankExplosionPrefab;
    public AudioClip TankExplosionAudio;
    public Slider slider;
    public GameObject fill;

    public AudioSource audiosource;

    void Start()
    {
        slider.value = hp / 100;
    }

    void TakeDamage()
    {
        if (hp <= 0) return;
        hp -= Random.Range(10, 20);
        slider.value = hp / 100;
        if (hp <= 0)
        {
           // AudioSource.PlayClipAtPoint(TankExplosionAudio, transform.position,1);
            audiosource.clip = TankExplosionAudio;
            audiosource.Play();
            GameObject go = GameObject.Instantiate(TankExplosionPrefab,transform.position+Vector3.up,transform.rotation) as GameObject;
            Destroy(this.gameObject);
            Destroy(go, 1.05f);
        }
        else if(hp <= 40)
            fill.GetComponent<Image>().color = Color.red;
        else if(hp <= 70)
            fill.GetComponent<Image>().color = new Color(1,1,0,1);
        else
            fill.GetComponent<Image>().color = Color.green;

    }
}

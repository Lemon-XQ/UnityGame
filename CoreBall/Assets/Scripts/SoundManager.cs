using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private AudioSource audio;
    public static SoundManager _instance;

	void Start () {
        _instance = this;
        audio = GetComponent<AudioSource>();
	}
    
    public void PlayAudio(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        audio.PlayOneShot(clip);
        
    }
}

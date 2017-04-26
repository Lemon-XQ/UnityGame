using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        instance = this;
        audio = GetComponent<AudioSource>();
	}

    public void playAudio(string path)
    {
        Debug.Log("Play");
        AudioClip clip = Resources.Load<AudioClip>(path);
        audio.PlayOneShot(clip);
    } 

}

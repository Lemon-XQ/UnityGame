using UnityEngine;
using System.Collections;


public class SoundManager : MonoBehaviour {

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;
    public string ResourceDir = "Sounds";

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();       
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    public bool Mute
    {
        get { return audioSource.mute; }
        set
        {
            audioSource.mute = value;
        }
    }

    #region BGM
    //设置背景音量 0-1
    public float BGVolume
    {
        get { return audioSource.volume; }
        set
        {
            audioSource.volume = value;
        }
    }

    public void PlayBGM(string name)
    {
       
        string path = ResourceDir + "/" + name;
        AudioClip ac = Resources.Load<AudioClip>(path);
        audioSource.clip = ac;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.clip = null;
        audioSource.Stop();
    }
    #endregion

    //Audio
    public void PlayAudio(string name)
    {
        string path = ResourceDir + "/" + name;
        AudioClip ac = Resources.Load<AudioClip>(path);;
        audioSource.PlayOneShot(ac);
    }
}

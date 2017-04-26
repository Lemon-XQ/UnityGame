using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController _instance;
    public bool alarmOn = false;
    public AudioSource musicNormal;
    public AudioSource musicPanic;
    public float MusicFadeSpeed = 1;
    public Vector3 lastPlayerPosition = Vector3.zero;

    private GameObject[] alarmers;
   

    void Awake()
    {
        _instance = this;
        alarmers = GameObject.FindGameObjectsWithTag("Alarm");
    }

    void Update()
    {
        AlarmLight._instance.alarm = alarmOn;
        if (alarmOn)
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume,0,Time.deltaTime*MusicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0.5f, Time.deltaTime * MusicFadeSpeed);
            PlayAlarm();
        }
        else
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0.5f, Time.deltaTime * MusicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0, Time.deltaTime * MusicFadeSpeed);
            StopAlarm();
        }
    }

    private void PlayAlarm()
    {
        foreach (GameObject alarm in alarmers)
        {
            if (!alarm.GetComponent<AudioSource>().isPlaying)
                alarm.GetComponent<AudioSource>().Play();
        }
    }

    private void StopAlarm()
    {
        foreach (GameObject alarm in alarmers)
        {
             alarm.GetComponent<AudioSource>().Stop();
        }
    }

}

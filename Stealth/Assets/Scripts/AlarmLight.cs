using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

    public static AlarmLight _instance;

    public bool alarm = false;
    private Light light;
    public float lowIntensity = 0;
    public float highIntensity = 1;
    private float targetIntensity;
    public float animSpeed = 1;
	
	void Awake () {
        _instance = this;
        light = GetComponent<Light>();
	}
	
	void Update () {
        if (alarm)
        {
            light.intensity = Mathf.Lerp(light.intensity,targetIntensity,Time.deltaTime * animSpeed);
            if (Mathf.Abs(light.intensity - targetIntensity) < 0.1f)
            {
                if (targetIntensity == highIntensity)
                {
                    targetIntensity = lowIntensity;
                }
                else
                    targetIntensity = highIntensity;
                
            }
        }
	}
}

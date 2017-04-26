using UnityEngine;
using System.Collections;

public class Focus : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

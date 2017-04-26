using UnityEngine;
using System.Collections;

public class Age : MonoBehaviour {

    private UIInput input;

	// Use this for initialization
	void Start () {
        input = this.GetComponent<UIInput>();
	}
	
	// Update is called once per frame
	public void OnAgeValueChanged() {
        string value=input.value;
        int valueInt = int.Parse(value);
        if (valueInt < 18)
        {
            input.value = "18";
        }
        if (valueInt > 120)
        {
            input.value = "120";
        }
	}
}

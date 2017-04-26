using UnityEngine;
using System.Collections;

public class TestHUDText : MonoBehaviour {

    public HUDText text;
    public UISlider slider;
    private int count;
    public UILabel label;
    private int value;
    private int bloodValue=100;

	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            value = -10;
            text.Add(value,Color.red,1f);
            UpdateLabelAndSlider(value);         
        }
        else if (Input.GetMouseButtonDown(1))
        {
            value = +10;
            text.Add(value,Color.green,1f);
            UpdateLabelAndSlider(value);
        }
	}

    public void UpdateLabelAndSlider(int value)
    {
        if (bloodValue>=0 && bloodValue<=100)
        {
            bloodValue += value;
            if (bloodValue <= 0)
                bloodValue = 0;
            else if (bloodValue >= 100)
                bloodValue = 100;
        }
        label.text = bloodValue+"/100";
        slider.value = bloodValue / 100.0f;
    }
}

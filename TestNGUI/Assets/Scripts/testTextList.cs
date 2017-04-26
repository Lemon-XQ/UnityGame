using UnityEngine;
using System.Collections;

public class testTextList : MonoBehaviour {

    private UITextList textlist;
    public UIInput input;

	// Use this for initialization
	void Start () {
        textlist = this.GetComponent<UITextList>();
	}

    public void OnFinishInput()
    {
        textlist.Add(input.value);
        input.value = "";
    }

	// Update is called once per frame
	void Update () {
        
	}
}

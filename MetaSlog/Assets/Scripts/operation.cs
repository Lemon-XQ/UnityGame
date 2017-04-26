using UnityEngine;
using System.Collections;

public class operation : MonoBehaviour {

    public GameObject inactiveObject;
	// Use this for initialization
	public void setOperationActive () {
        gameObject.SetActive(true);
        inactiveObject.SetActive(false);
        
	}

}

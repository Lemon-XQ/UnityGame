using UnityEngine;
using System.Collections;

public class select : MonoBehaviour {

    public GameObject[] inactiveObject;
	// Use this for initialization
    public void setCharacterActive()
    {
        gameObject.SetActive(true);
        foreach (GameObject o in inactiveObject)
        {
            o.SetActive(false);
        }
        
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageShow : MonoBehaviour {


	void Start () {
        this.GetComponent<GUIText>().text = "  WASD to Move\n  Z to Switch\n  Shift to Sneak\n";
	}
	
}

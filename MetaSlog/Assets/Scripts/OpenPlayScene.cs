using UnityEngine;
using System.Collections;

public class OpenPlayScene : MonoBehaviour {

    public int sceneIndex;

	// Use this for initialization
    public void openPlayScene() 
    {
        Application.LoadLevel(sceneIndex);
    }
}

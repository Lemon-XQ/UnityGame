using UnityEngine;
using System.Collections;

public class GameManage : MonoBehaviour {

    private int sceneIndex;

    public void OnStartGame(int sceneIndex)
    {
        Application.LoadLevel(sceneIndex);
    }
}

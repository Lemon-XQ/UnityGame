using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Up : MonoBehaviour {

    public int upSpeed = 1;
    private bool up_switch = true;
    private LineRenderer line;

	void Start () {
        line = GetComponent<LineRenderer>();
	}

	void Update () {
        if (up_switch)
        {
            transform.Translate(0,upSpeed*Time.deltaTime,0);
            if (transform.position.y >= -2.5f)
            {
                SoundManager._instance.PlayAudio("shoot");

                transform.position = new Vector3(0,-2.5f,0);
                transform.parent = GameObject.Find("CenterSphere").transform;
                up_switch = false;
            }
        }
        else
        {
             line.SetPosition(0, Vector3.zero);
             line.SetPosition(1, transform.position);
        }           
	}

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameObject.Find("CenterSphere").GetComponent<Controller>().OnStop();
            SoundManager._instance.PlayAudio("dieOrWin");
            Invoke("OnOver", 1f);
        }
    }


    void OnOver()
    {
        //重新加载场景
        SceneManager.LoadScene(0);
    }

}

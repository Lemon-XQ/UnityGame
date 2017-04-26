using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Lift : MonoBehaviour {

    public float openSpeed = 1;
    public Transform innerDoor_left;
    public Transform innerDoor_right;
    public Transform outerDoor_left;
    public Transform outerDoor_right;

    private Vector3 tempLeft = Vector3.zero;
    private Vector3 tempRight = Vector3.zero;
    private Vector3 startPos_left = Vector3.zero;
    private Vector3 startPos_right = Vector3.zero;
    private bool startLift = false;
    private AudioSource liftAudioSource;

    void Awake()
    {
        startPos_left = innerDoor_left.position;
        startPos_right = innerDoor_right.position;
        tempLeft = innerDoor_left.position;
        tempRight = innerDoor_right.position;
        liftAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (startLift)
        {
            //内部门复位
            innerDoor_left.position = startPos_left ;
            innerDoor_right.position = startPos_right ;
            //上升
            Vector3 pos=new Vector3(transform.position.x,0,transform.position.z);
            pos.y=Mathf.Lerp(transform.position.y,transform.position.y+5,Time.deltaTime*0.5f);
            transform.position = pos;
            //播放音效
            if (!liftAudioSource.isPlaying)
                liftAudioSource.Play();
        }
        else
        {
            tempLeft.x = Mathf.Lerp(tempLeft.x,outerDoor_left.position.x,openSpeed*Time.deltaTime);
            tempRight.x = Mathf.Lerp(tempRight.x, outerDoor_right.position.x, openSpeed * Time.deltaTime);
            innerDoor_left.position = tempLeft;
            innerDoor_right.position = tempRight;
        }
    }

    public void OnTriggerStay(Collider other){
        if (other.tag == "Player")
        {
            StartCoroutine("ReloadScene");
        }
    }

    IEnumerator ReloadScene()
    {
        //等1秒后启动电梯
        yield return new WaitForSeconds(1f);
        startLift = true;
        //等5秒后重新加载场景
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }


}

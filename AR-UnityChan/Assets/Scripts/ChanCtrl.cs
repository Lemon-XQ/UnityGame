using UnityEngine;
using System.Collections;
using System;

public class ChanCtrl : MonoBehaviour {

    public float waitTime = 3f;//切换至下一个动画的时间
    public bool isRandom = true;

    private Animator _animator;
    private AnimatorStateInfo _currentState;
    private AnimatorStateInfo _preState;
    private AnimationClip[] _FaceClips;
    private string[] _FaceMotionName;
    private AudioClip[] _ChanVoice;
    private AudioSource audio;
    private AudioClip[] _HourClips;


	void Start () {
        audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _currentState = _animator.GetCurrentAnimatorStateInfo(0);
        _preState = _currentState;

        _FaceClips = Resources.LoadAll<AnimationClip>("FaceMotion");
        _ChanVoice = Resources.LoadAll<AudioClip>("ChanVoice");
        _HourClips = Resources.LoadAll<AudioClip>("HourClips");
        _FaceMotionName = new string[_FaceClips.Length];

        for (int i = 0; i < _FaceClips.Length; i++)
        {
            _FaceMotionName[i] = _FaceClips[i].name;
        }
        StartCoroutine("RandomChangeMotion");
	}
	

	void Update () {

        RaycastHit hitinfo;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitinfo,Mathf.Infinity))
        {
            if(hitinfo.collider.tag=="Face")
            {
                ChangeFace();
            }
        }

        if (_animator.GetBool("Next"))
        {
            _currentState = _animator.GetCurrentAnimatorStateInfo(0);
            //比较当前状态是否与前一个状态相同
            if (_preState.fullPathHash != _currentState.fullPathHash)
            {
                //说明已经进入一个新的状态,不让true一直触发
                _animator.SetBool("Next", false);
                _preState = _currentState;
            }
        }

       //StartCoroutine("RandomChangeMotion");
	}

    IEnumerator RandomChangeMotion()
    {
        while (true)
        {
            if (isRandom)
            {
                _animator.SetBool("Next", true);
            }
            yield return null;
           // yield return new WaitForSeconds(waitTime);
        }
    }

    private void ChangeFace()
    {
        _animator.SetLayerWeight(1, 1);//脸部层权重设为1
        int index = UnityEngine.Random.Range(0,_FaceMotionName.Length);
        _animator.CrossFade(_FaceMotionName[index],0);//立刻切换到下一个脸部动画

        //播放音效
        //if (audio.isPlaying)
        //{
        //    audio.Stop();//停止当前声音，切换到下一段音效
        //}
        if (!audio.isPlaying)
        {
            audio.clip = _ChanVoice[index];
            audio.Play();
        }
        
    }

    public void OnAskTimeButtonClick()
    {
        int hour=DateTime.Now.Hour;
        if (audio.isPlaying)
        {
            audio.Stop();
        }
        audio.clip=_HourClips[hour];
        audio.Play();
    }

}

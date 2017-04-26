using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour {

    private Animator animator;

	void Start () {
        animator = this.GetComponent<Animator>();
	}

    public void IdleState()
    {
        animator.SetBool("Die", false);
        animator.SetFloat("Run", 0.5f);
    }

    public void RunState(bool isRunL)
    {
        float value = isRunL ? 0 : 1;
        animator.SetFloat("Run", value);

    }

    public void JumpState(bool isJump,bool left)
    {
        if (isJump)
        {
            if (left)
                animator.SetBool("JumpL", true);
            else
                animator.SetBool("JumpR", true);
            SoundManager.Instance.PlayAudio("Audio_jump");
        }
        else
        {
            animator.SetBool("JumpL", false);
            animator.SetBool("JumpR", false);
        }
    }

    public void DieState()
    {
        animator.SetBool("Die", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void PlayWalkAnimation()
    {
        myAnimator.SetBool("walking", true);
    }

    private void StopWalkAnimation()
    {
        myAnimator.SetBool("walking", false);
    }

    private void PlayIdleAnimation()
    {
        myAnimator.SetBool("Idle", true);
    }

    private void StopIdleAnimation()
    {
        myAnimator.SetBool("Idle", false);
    }

    private void PlayRunningAnimation()
    {
        myAnimator.SetBool("running", true);
    }

    public void StopRunningAnimation()
    {
        myAnimator.SetBool("running", false);
    }

    public bool IsRunning()
    {
        return myAnimator.GetBool("running");
    }

    public void StartIdle()
    {
        PlayIdleAnimation();
        StopWalkAnimation();
        StopRunningAnimation();
    }

    public void StartWalk()
    {
        PlayWalkAnimation();
        StopIdleAnimation();
        StopRunningAnimation();
    }

    public void StartRunning()
    {
        PlayRunningAnimation();
        StopIdleAnimation();
        StopWalkAnimation();
    }
}

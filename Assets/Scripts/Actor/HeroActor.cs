using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActor : Actor
{
    [SerializeField]
   protected Animator animator;
    [SerializeField]
    protected AnimationClip idle, run, die;
    public void PlayIdle()
    {
        animator.Play(idle.name);
    }
    public void PlayRun()
    {
        animator.Play(run.name);
    }
    public void PlayDie()
    {
        animator.Play(die.name);
    }
    private void Awake()
    {
        OnAwake();
    }
}

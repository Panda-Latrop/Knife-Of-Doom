using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationComponent : ActorComponent
{
    [SerializeField]
    protected Animation animator;
    [SerializeField]
    protected AnimationClip shakeClip, smallShakeClip;

    protected override void OnAwake()
    {
        animator.AddClip(shakeClip,shakeClip.name);
        animator.AddClip(smallShakeClip, smallShakeClip.name);
    }
    public void PlayShake()
    {
        animator.Play(shakeClip.name);
    }
    public void PlaySmallShake()
    {
        animator.Play(smallShakeClip.name);
    }
    private void Awake()
    {
        OnAwake();
    }
}

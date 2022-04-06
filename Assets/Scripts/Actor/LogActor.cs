using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogActor : GameStuffActor
{
    [SerializeField]
    protected Transform rotator;
    [SerializeField]
    protected new LogAnimationComponent animation;
    protected AnimationCurve rotationCurve;
    protected float time;
    protected List<GameStuffActor> attachedStuffs = new List<GameStuffActor>();
    [SerializeField]
    protected bool useHitEffect;
    [SerializeField]
    protected DynamicActor hitEffect;

    public bool AnimationIsPlaying => animation.IsPlaying;

    public override void OnPop()
    {
        base.OnPop();
        enabled = physics.enabled = true;
    }
    public override void OnPush()
    {
        base.OnPush();
        animation.Stop();
        enabled = physics.enabled = false;
        time = 0.0f;
        rotator.rotation = Quaternion.identity;
        for (int i = 0; i < attachedStuffs.Count; i++)
        {
            GameInstance.Instance.PoolManager.Push(attachedStuffs[i]);
        }
        attachedStuffs.Clear();
    }
    public void SetCurve(AnimationCurve curve)
    {
        rotationCurve = curve;
    }
    public override void Hit(GameStuffActor by)
    {
        PlayHit();
        GameInstance.Instance.GameState.OnHitLog(this, by);
        if (useHitEffect)
            GameInstance.Instance.PoolManager.Pop(hitEffect).SetPosition(by.transform.position);
    }
    public void AttachStuff(GameStuffActor stuff,float offset = 0.0f) 
    {
        attachedStuffs.Add(stuff);
        Transform logT = transform, stuffT = stuff.transform;
        stuffT.SetParent(rotator, true);
        stuffT.position = logT.position + (stuffT.position - logT.position).normalized * (collider.bounds.extents.x+ offset);
    }
    public void RemoveStuff(GameStuffActor stuff)
    {
        attachedStuffs.Remove(stuff);
    }
    public override void Break()
    {
        for (int i = 0; i < attachedStuffs.Count; i++)
        {
            GameStuffActor stuff = attachedStuffs[i];
            stuff.transform.SetParent(null, true);
            stuff.AddRandomForce();
        }
        attachedStuffs.Clear();
        enabled = physics.enabled = false;
        base.Break();
    }
    public void PlayHit()
    {
        animation.PlayHit();
    }
    public void PlayAppear()
    {
        animation.PlayAppear();
    }
    protected override void OnFixedUpdate()
    {
        rotator.transform.rotation = Quaternion.AngleAxis(rotationCurve.Evaluate(time), Vector3.forward);
        time += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
}

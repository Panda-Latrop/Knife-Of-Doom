using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicActor : Actor
{
    [SerializeField]
    protected float timeToPush = 2.0f;
    protected float nextPush;
    public override void OnPop()
    {
        base.OnPop();
        nextPush = Time.time + timeToPush;
        enabled = true;
    }
    protected override void OnLateUpdate()
    {
        if (Time.time >= nextPush)
        {
            GameInstance.Instance.PoolManager.Push(this);
        }
    }
    protected void LateUpdate()
    {
        OnLateUpdate();
    }
}

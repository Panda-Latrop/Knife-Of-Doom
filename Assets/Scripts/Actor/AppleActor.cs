using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleActor : GameStuffActor
{
    [SerializeField]
    protected float timeToPush = 3.0f;
    protected bool toPush;
    protected float nextPush;
    public override void Hit(GameStuffActor by)
    {
        if (!toPush)
        {
            GameInstance.Instance.GameState.OnHitApple(this, by);
            Break();
        }
    }
    public override void OnPop()
    {
        base.OnPop();
        toPush = false;
        rigidbody.gravityScale = 0;
        physics.enabled = false;
    }
    public override void AddRandomForce(float speed = 10.0f, float torque = 360.0f)
    {
        rigidbody.gravityScale = 1;
        physics.enabled = true;
        toPush = true;
        nextPush = Time.time + timeToPush;
        base.AddRandomForce(speed, torque);
    }

    protected override void OnLateUpdate()
    {
        if (toPush && Time.time >= nextPush)
            GameInstance.Instance.PoolManager.Push(this);
    }
    private void LateUpdate()
    {
        OnLateUpdate();
    }
}

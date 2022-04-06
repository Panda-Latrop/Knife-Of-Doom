using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeActor : GameStuffActor
{
    protected float speed = 20.0f;
    protected int knifeState;
    protected const int STUCK = 1, OUT = 2, NONE = 0;
    [SerializeField]
    protected float timeToPush = 3.0f;
    protected float nextPush;
    [SerializeField]
    protected bool useHitEffect;
    [SerializeField]
    protected DynamicActor hitEffect;

    public override void OnPush()
    {
        base.OnPush();
        enabled = false;
        rigidbody.gravityScale = 0.0f;
        rigidbody.rotation = 0;
        knifeState = NONE;
        collider.enabled = true;
    }
    public override void OnPop()
    {
        base.OnPop();
        enabled = true;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public override void Hit(GameStuffActor by)
    {
        if (knifeState == STUCK)
        {
            if (useHitEffect)
                GameInstance.Instance.PoolManager.Pop(hitEffect).SetPosition(by.transform.position);
            GameInstance.Instance.GameState.OnHitKnife(this, by);
        }


    }

    protected override void OnUpdate()
    {
        if(knifeState == NONE)
        {
            rigidbody.gravityScale = 0.0f;
            physics.SetVelocity(Vector2.up * speed);
        }

    }
    public void Stuck()
    {
        knifeState = STUCK;
        physics.enabled = enabled = false;
    }
    public override void AddRandomForce(float speed = 10.0f, float torque = 360.0f)
    {
        rigidbody.gravityScale = 1;
        knifeState = OUT;
        enabled = physics.enabled = true;
        nextPush = Time.time + timeToPush;
        collider.enabled = false;
        base.AddRandomForce(speed, torque);

    }
    protected override void OnLateUpdate()
    {
        if ((knifeState == OUT && Time.time >= nextPush) || transform.position.y >= 100.0f)
            GameInstance.Instance.PoolManager.Push(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (knifeState == NONE && (1 << target.layer) == (1 << 6))
        {
            switch (target.tag)
            {
                case "Log":
                    Stuck();
                    break;
                case "Apple":
                    break;
                default:
                    knifeState = OUT;
                    enabled = true;
                    physics.enabled = true;
                    rigidbody.gravityScale = 1.0f;
                    AddRandomForce();
                    nextPush = Time.time + timeToPush;
                    break;
            }
            target.GetComponent<GameStuffActor>().Hit(this);
        }
    }
    private void Update()
    {
        OnUpdate();
    }
    private void LateUpdate()
    {
        OnLateUpdate();
    }
}

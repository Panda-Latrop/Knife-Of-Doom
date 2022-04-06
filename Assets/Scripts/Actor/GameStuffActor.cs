using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStuffActor : Actor
{
    [SerializeField]
    protected new Rigidbody2D rigidbody;
    [SerializeField]
    protected new Collider2D collider;
    [SerializeField]
    protected StuffSkinComponent skin;
    [SerializeField]
    protected KinematicPhysicsComponent physics;
    [SerializeField]
    protected DynamicActor gibs;

    //public KinematicPhysicsComponent Physics => physics;
    public override Bounds Bounds => collider.bounds;
    protected override void OnAwake()
    {
        physics.SetRigidbody(rigidbody);
    }
    public override void OnPop()
    {
        base.OnPop();
        physics.enabled = true;
    }
    public override void OnPush()
    {
        base.OnPush();
        physics.enabled = false;
    }
    public virtual void AddRandomForce(float speed= 10.0f,float torque = 360.0f)
    {
        physics.SetVelocity(Random.insideUnitCircle * speed);
        physics.SetTorque((Random.Range(0, 2) == 0 ? -1 : 1) *torque);
    }
    public void SetSkin(StuffSkinScriptableObject skin)
    {
        this.skin.SetSkin(skin);
    }
    public virtual void Hit(GameStuffActor by)
    {

    }
    public virtual void Break()
    {
        PoolManager pool = GameInstance.Instance.PoolManager;
        pool.Pop(gibs).SetPosition(transform.position);
        pool.Push(this);

    }
    private void Awake()
    {
        OnAwake();
    }
}

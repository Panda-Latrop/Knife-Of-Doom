using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootState
{
    initiated,
    process,
    ended,
    unready,
    none,
}
public class KnifeThrowerComponent : ActorComponent
{
    protected StuffSkinScriptableObject skin;
    protected KnifeActor knife;
    [SerializeField]
    protected Transform shootPoint;
    protected float speed;
    protected float timeToShoot, nextShoot;
    protected ShootState shootState = ShootState.ended;

    public ShootState ShootState => shootState;
    public Transform ShootPoint => shootPoint;
    public void SetKnife(KnifeActor knife)
    {
        this.knife = knife;
    }
    public void SetSkin(StuffSkinScriptableObject skin)
    {
        this.skin = skin;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetFireRate(float fireRate)
    {
        timeToShoot = 1.0f / fireRate;
    }
    protected virtual bool CanShoot()
    {
        if (Time.time >= nextShoot)
        {
            nextShoot = Time.time + timeToShoot;
            return true;
        }
        return false;
    }
    public ShootState Shoot(Vector3 position, Vector3 direction)
    {
        if (CanShoot())
        {
            enabled = true;
            KnifeActor knife = GameInstance.Instance.PoolManager.Pop(this.knife) as KnifeActor;
            knife.SetSkin(skin);
            knife.SetPosition(position);
            knife.SetRotation(Quaternion.identity);
            knife.SetSpeed(speed);
            shootState = ShootState.initiated;
            return shootState;
        }
        shootState = ShootState.unready;
        return shootState;
    }

    private void Awake()
    {
        OnAwake();
    }
}
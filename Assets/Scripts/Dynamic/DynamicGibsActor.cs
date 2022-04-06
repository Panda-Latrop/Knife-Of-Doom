using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicGibsActor : DynamicImpactActor
{
    [SerializeField]
    protected float force, torque;
    protected List<Vector2> defaultPos = new List<Vector2>();
    protected override void OnAwake()
    {
        for (int i = 0; i < transform.childCount; i++)
            defaultPos.Add(transform.GetChild(i).localPosition);
    }
    public override void OnPop()
    {
        base.OnPop();
        ResetChildrenPositionAndRotation();
    }
    protected virtual void ResetChildrenPositionAndRotation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = defaultPos[i];
            child.rotation = Quaternion.identity;
        }
    }
    protected virtual void ApplyForce(Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.AddForce(Random.insideUnitCircle * force, ForceMode.Impulse);
        rigidbody.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
    }
    protected virtual void ApplyForce(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0.0f;
        rigidbody.AddForce(Random.insideUnitCircle * force, ForceMode2D.Impulse);
        rigidbody.AddTorque(Random.Range(-1.0f,1.0f) * torque, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        OnAwake();
    }
}

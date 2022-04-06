using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicPhysicsComponent : ActorComponent
{
    protected new Rigidbody2D rigidbody;
    protected Vector2 velocity;
    protected float rotation,torque;
    protected bool hasRotation;

    private void OnDisable()
    {
        rotation = 0.0f;
        torque = 0.0f;
        hasRotation = false;
        velocity = Vector2.zero;
    }
    public void SetRigidbody(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
        this.rigidbody.isKinematic = true;
    }
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
    public void SetRotation(float rotation)
    {
        this.rotation = rotation;
        hasRotation = true;
    }
    public void SetTorque(float torque)
    {
        this.torque = torque;
    }
    public void SetGravityScale(float scale)
    {
        rigidbody.gravityScale = scale;
    }
    protected virtual float RotationCalculation()
    {
        if (hasRotation)
            return rotation;
        else
            return rigidbody.rotation + torque * Time.fixedDeltaTime;
    }
    protected virtual Vector3 PositionCalculation()
    {
        velocity += Physics2D.gravity * rigidbody.gravityScale * Time.fixedDeltaTime;
        return rigidbody.position + velocity * Time.fixedDeltaTime;
    }
    protected override void OnFixedUpdate()
    {
        rigidbody.MoveRotation(RotationCalculation());
        rigidbody.MovePosition(PositionCalculation());
        hasRotation = false;
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
}

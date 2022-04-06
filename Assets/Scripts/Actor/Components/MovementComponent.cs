using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : ActorComponent
{
    protected new Rigidbody2D rigidbody;
    [SerializeField]
    protected float speed;
    protected Vector2 velocity;
    protected float torque;
    protected bool hasForce;

    public void SetRigidbody(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
    }
    public void SetMove(Vector2 direction)
    {
        velocity = direction * speed;
    }
    public void SetForce(Vector2 velocity, float torque)
    {
        this.velocity = velocity;
        this.torque = torque;
        hasForce = true;
    }

    protected override void OnFixedUpdate()
    {

        if (hasForce)
        {
            velocity += Physics2D.gravity * Time.fixedDeltaTime;
            rigidbody.MoveRotation(rigidbody.rotation + torque);
        }
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
}

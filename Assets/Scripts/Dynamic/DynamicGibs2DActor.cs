using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGibs2DActor : DynamicGibsActor
{
    [SerializeField]
    protected List<Rigidbody2D> physicsGibs = new List<Rigidbody2D>();

    public override void OnPop()
    {
        base.OnPop();
        for (int i = 0; i < physicsGibs.Count; i++)
        {
            Rigidbody2D gib = physicsGibs[i];
            ApplyForce(gib);
        }
    }
}

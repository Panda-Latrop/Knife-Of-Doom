using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGibs3DActor : DynamicGibsActor
{
    [SerializeField]
    protected List<Rigidbody> physicsGibs = new List<Rigidbody>();

    public override void OnPop()
    {
        base.OnPop();
        for (int i = 0; i < physicsGibs.Count; i++)
        {
            Rigidbody gib = physicsGibs[i];
            ApplyForce(gib);
        }
    }
}

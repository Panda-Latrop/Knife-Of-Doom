using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByCurveComponent : KinematicPhysicsComponent
{
    [SerializeField]
    protected AnimationCurve motionCurve;
    protected float time;
    private void OnDisable()
    {
        time = 0.0f;
    }
    protected override float RotationCalculation()
    {
        float rotation = motionCurve.Evaluate(time);
        time += Time.deltaTime * torque;
        return rotation;
    }

}

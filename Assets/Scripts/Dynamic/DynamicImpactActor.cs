using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicImpactActor : DynamicActor
{
    [SerializeField]
    protected bool useParticle;
    [SerializeField]
    protected ParticleSystem particle;
    [SerializeField]
    protected bool useAudio;
    [SerializeField]
    protected AudioSource source;
    public override void OnPop()
    {
        base.OnPop();
        if (useParticle)
            particle.Play(true);
        if (useAudio)
            source.Play();

    }
}

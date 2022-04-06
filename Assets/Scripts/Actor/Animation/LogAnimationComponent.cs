using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationVector
{
    [SerializeField]
    protected AnimationCurve posX, posY;
    protected float time,endTime;
    public bool IsEnd { get { if (time > endTime) { time = 0; return true; } else return false; } }
    public void Prepare()
    {
        endTime = Mathf.Max(posX.keys[posX.length - 1].time, posY.keys[posY.length - 1].time);
    }
    public void Stop()
    {
        time = 0;
    }
    public Vector2 Evaluate()
    {
        Vector2 vector = new Vector2(posX.Evaluate(time), posY.Evaluate(time));
        time += Time.deltaTime;   
        return vector;
    }

}

public class LogAnimationComponent : ActorComponent
{
    [SerializeField]
    protected Transform root;
    protected Vector3 defaultScale;
    protected bool isPlaying;
    protected int currentAnimation;
    [SerializeField]
    protected AnimationVector hitAnimation, appearAnimation;
    public bool IsPlaying => isPlaying;

    protected override void OnAwake()
    {
        hitAnimation.Prepare();
        appearAnimation.Prepare();
        defaultScale = root.localScale;
        
    }
    public void Stop()
    {
        hitAnimation.Stop();
        appearAnimation.Stop();
        isPlaying = false;
    }
    public void PlayHit()
    {
        isPlaying = true;
        currentAnimation = 0;
        hitAnimation.Stop();
    }
    public void PlayAppear()
    {
        isPlaying = true;
        currentAnimation = 1;
        appearAnimation.Stop();
    }
    protected override void OnLateUpdate()
    {
        if (isPlaying)
        {
            switch (currentAnimation)
            {
                case 0:
                    root.position = root.parent.position + (Vector3)hitAnimation.Evaluate();
                    if (hitAnimation.IsEnd)
                        isPlaying = false;
                    break;
                case 1:
                    Vector3 eva = appearAnimation.Evaluate();
                    eva.z = 1;
                    root.localScale = Vector3.Scale(defaultScale,eva);
                    if (appearAnimation.IsEnd)
                        isPlaying = false;
                    break;
            }
        }
    }
    private void Awake()
    {
        OnAwake();
    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }
}

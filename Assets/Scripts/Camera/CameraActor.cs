using SimpleJSON;
using System;
using System.Collections;
using UnityEngine;

public class CameraActor : Actor
{
    [SerializeField]
    protected new Camera camera;
    protected Vector3 offset;
    [SerializeField]
    protected new CameraAnimationComponent animation;
    public Camera Instance => camera;
    public float NearClipPlane { get => camera.nearClipPlane; }

    public CameraActor Setup()
    {
        offset = transform.position;

        return this;
    }
    public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
    {
        return camera.ScreenPointToRay(pos, eye);
    }
    public virtual void Teleport(Vector3 point)
    {
        transform.position = point;
    }
    public void PlayShake()
    {
        animation.PlayShake();
    }
    public void PlaySmallShake()
    {
        animation.PlaySmallShake();
    }
}

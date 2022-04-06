using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : Actor
{

    protected bool hasController;
    protected ControllerBase controller;

    public Pawn Self => this;
    public bool HasController { get => hasController; }
    public ControllerBase Controller { get => controller; }
    public virtual void OnPossess(ControllerBase controller)
    {
        hasController = true;
        this.controller = controller;
        return;
    }
    public virtual void OnUnpossess()
    {
        if (hasController)
        {
            hasController = false;
            controller = null;
        }
        return;
    }
}

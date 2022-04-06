using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : Actor
{
    protected bool hasPawn;
    protected Pawn controlledPawn;
    public bool HasPawn { get => hasPawn; }
    public Pawn ControlledPawn { get => controlledPawn; }
    public virtual void Possess(Pawn pawn)
    {
        gameObject.SetActive(true);
        enabled = true;
        controlledPawn = pawn;
        pawn.OnPossess(this);
        hasPawn = true;
    }
    public virtual void Unpossess()
    {
        gameObject.SetActive(false);
        enabled = false;
        if (hasPawn)
        {
            controlledPawn.OnUnpossess();
            controlledPawn = null;
            hasPawn = false;
        }
        GameInstance.Instance.PoolManager.Push(this);
    }
}

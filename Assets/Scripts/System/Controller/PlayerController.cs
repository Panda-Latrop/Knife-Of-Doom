using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{
    public bool gameInput = true;
    protected new CameraActor camera;
    protected GameState gameState;

    protected override void OnAwake()
    {
        
    }
    public override void Possess(Pawn pawn)
    {
        base.Possess(pawn);
    }
    public override void Unpossess()
    {
        base.Unpossess();
    }

    public void SetCamera(CameraActor camera)
    {
        this.camera = camera;
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            gameState.ThrowKnife();
        }
    }
    private void Awake()
    {
        OnAwake();
    }
    private void Update()
    {
        OnUpdate();
    }
}

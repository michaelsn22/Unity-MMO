using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{
    private PlayerSprintData sprintData;
    private float startTime;
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        sprintData = movementData.SprintData;
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.movementSpeedModifier = movementData.RunData.speedModifier;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.ReusableData.shouldWalk)
        {
            return;
        }

        if (Time.time < startTime + sprintData.RunToWalkTime)
        {
            return;
        }
        StopRunning();
    }

    #endregion
    
    #region Main Methods
    private void StopRunning()
    {
        if (stateMachine.ReusableData.movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);

            return;
        }
        stateMachine.ChangeState(stateMachine.WalkingState);
    }
    
    #endregion


    #region Input Methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MediumStoppingState);
    }

    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        //toggle walk state
        base.OnWalkToggleStarted(context);
        //change to walking state
        stateMachine.ChangeState(stateMachine.WalkingState);
    }

    #endregion

    
}

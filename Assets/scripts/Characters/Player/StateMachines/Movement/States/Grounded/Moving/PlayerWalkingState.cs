using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovingState
{
    public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Method

    public override void Enter()
    {
        base.Enter();
        
        stateMachine.ReusableData.movementSpeedModifier = movementData.WalkData.speedModifier;

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
    }
    #endregion

    #region Input Methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.LightStoppingState);
    }

    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        //toggle walk state
        base.OnWalkToggleStarted(context);
        //change to running state
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    #endregion

    //#region Reusable Methods
//MIGHT BE AN ISSUE HERE IN THE FUTURE!@!@$#$@^%$%&**&&*^^^^^^^^^55463543!
/*
    protected void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #endregion
    */
}

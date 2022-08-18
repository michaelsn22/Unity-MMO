using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardStoppingState : PlayerStoppingState
{
    public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;
    }
    #endregion

    #region Reusable Methods
    protected override void OnMove()
    {
        if (stateMachine.ReusableData.shouldWalk)
        {
            return;
        }
        stateMachine.ChangeState(stateMachine.RunningState);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        ResetSprintState();
    }
    #endregion


    #region Reusable Methods
    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    protected virtual void ResetSprintState()
    {
        stateMachine.ReusableData.shouldSprint = false;
    }
    #endregion
}

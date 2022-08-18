using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{

    private PlayerFallData fallData;
    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        fallData = airborneData.FallData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.movementSpeedModifier = 0f;

        ResetVerticalVelocity();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        LimitVerticalVelocity();
    }
    #endregion

    #region Reusable Methods
    protected override void ResetSprintState()
    {

    }
    #endregion

    
    #region Main Methods
    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = getPlayerVerticleVelocity();
        if (playerVerticalVelocity.y >= -fallData.FallSpeedLimit)
        {
            return;
        }

        Vector3 limitedVelocity = new Vector3(0f, -fallData.FallSpeedLimit - stateMachine.Player.Rigidbody.velocity.y, 0f);

        stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerAirborneState
{
    private PlayerJumpData jumpData;

    private bool shouldKeepRotating;

    private bool canStartFalling;

    public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        jumpData = airborneData.JumpData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.movementSpeedModifier = 0f;

        stateMachine.ReusableData.MovementDecelerationForce = jumpData.DecelerationForce;

        stateMachine.ReusableData.RotationData = jumpData.RotationData;

        shouldKeepRotating = stateMachine.ReusableData.movementInput != Vector2.zero;



        Jump();
    }

    public override void Exit()
    {
        base.Exit();

        SetBaseRotationData();

        canStartFalling = false;
    }

    public override void Update()
    {
        base.Update();

        if (!canStartFalling && IsMovingUp(0f))
        {
            canStartFalling = true;
        }

        if (!canStartFalling || getPlayerVerticleVelocity().y > 0)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.FallingState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (shouldKeepRotating)
        {
            RotateTowardsTargetRotation();
        }

        if (IsMovingUp())
        {
            //fix the floatyness of the jumping.
            DecelerateVertically();
        }
    }
    #endregion

    #region Reusable Methods
    protected override void ResetSprintState()
    {

    }
    #endregion


    #region Main Methods
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

        Vector3 playerForward = stateMachine.Player.transform.forward;

        if (shouldKeepRotating)
        {
            playerForward = GettargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        jumpForce.x *= playerForward.x;
        jumpForce.z *= playerForward.z;

        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.CollidersUtility.CapsuleColliderData.collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, jumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            //then check if we are on a slope
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            if (IsMovingUp())
            {
                float forceModifier = jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);

                jumpForce.x *= forceModifier;
                jumpForce.z *= forceModifier;
            }
            if (IsMovingDown())
            {
                float forceModifier = jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);

                jumpForce.y *= forceModifier;
            }
        }


        ResetVelocity();

        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion
}

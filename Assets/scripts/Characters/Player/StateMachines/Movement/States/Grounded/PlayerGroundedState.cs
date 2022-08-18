using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    private SlopeData slopeData;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        slopeData = stateMachine.Player.CollidersUtility.SlopeData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        UpdateShouldSprintState();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Float();
    }
    
    #endregion

    #region Main Methods
    private bool IsThereGroundUnderneath()
    {
        BoxCollider groundCheckCollider = stateMachine.Player.CollidersUtility.TriggerColliderData.GroundCheckCollider;

        Vector3 groundColliderCenterInWorldSpace = stateMachine.Player.CollidersUtility.TriggerColliderData.GroundCheckCollider.bounds.center;
        Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, groundCheckCollider.bounds.extents, groundCheckCollider.transform.rotation, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

        return overlappedGroundColliders.Length > 0;
    }
    private void UpdateShouldSprintState()
    {
        if (!stateMachine.ReusableData.shouldSprint)
        {
            return;
        }

        if (stateMachine.ReusableData.movementInput != Vector2.zero)
        {
            return;
        }

        stateMachine.ReusableData.shouldSprint = false;
    }

    private void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace =
            stateMachine.Player.CollidersUtility.CapsuleColliderData.collider.bounds.center;
        Ray downwardsRayFRomCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFRomCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.playerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFRomCapsuleCenter.direction);

            float slopeSpeedModifer = SetSlopeSpeedModiferOnAngle(groundAngle);

            if (slopeSpeedModifer == 0f)
            {
                return;
            }
            float distanceToFloatingPoint =
                stateMachine.Player.CollidersUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;
            if (distanceToFloatingPoint == 0f)
            {
                return;
            }

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - getPlayerVerticleVelocity().y;

            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
            
            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }

    private float SetSlopeSpeedModiferOnAngle(float angle)
    {
        float slopeSpeedModifer = movementData.slopeSpeedAngle.Evaluate(angle);

        stateMachine.ReusableData.movementOnSlopesSpeedModifier = slopeSpeedModifer;

        return slopeSpeedModifer;
    }
    #endregion

    #region reusable methods
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;

        stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;

        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
    }


    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;

        stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;

        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
    }
    
    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.shouldSprint)
        {
            stateMachine.ChangeState(stateMachine.SprintingState);
            return;
        }
        if (stateMachine.ReusableData.shouldWalk)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    protected override void OnContactWithGroundExited(Collider collider)
    {
        base.OnContactWithGroundExited(collider);

        if (IsThereGroundUnderneath())
        {
            return;
        }
        //i modified the below line and added .CpasuleColliderData after collidersutility. This may cause an issue later on down the line. #################
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.CollidersUtility.CapsuleColliderData.collider.bounds.center;

        Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.Player.CollidersUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);
        if (!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, movementData.GroundToFallRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            OnFall();
        }
    }

    protected virtual void OnFall()
    {
        stateMachine.ChangeState(stateMachine.FallingState);
    }
    #endregion

    #region Input Methods

    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            //toggle walk state
            base.OnWalkToggleStarted(context);
            //change to running state
            stateMachine.ChangeState(stateMachine.RunningState);
        }
    
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

       protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.DashingState);
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpingState);
    }

    #endregion
    
}

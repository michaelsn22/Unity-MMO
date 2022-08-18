using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    protected PlayerAirborneData airborneData;

    public float AdjustSpeed;


    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
        movementData = stateMachine.Player.Data.GroundedData;
        airborneData = stateMachine.Player.Data.AirborneData;
        InitializeData();
        
    }

    private void InitializeData()
    {
        SetBaseRotationData();
    }

    public virtual void Enter()
    {
        //Debug.Log("current State " + GetType().Name);
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
    }

    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        stateMachine.ReusableData.shouldWalk = !stateMachine.ReusableData.shouldWalk;
    }

    protected virtual void OnContactWithGround(Collider collider)
    {
        
    }

    protected virtual void OnContactWithGroundExited(Collider collider)
    {

    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
    }

    protected void DecelerateHorizontally()
    {
        Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
    }

    protected void DecelerateVertically()
    {
        Vector3 playerVerticalVelocity = getPlayerVerticleVelocity();

        stateMachine.Player.Rigidbody.AddForce(-playerVerticalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
    }

    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
        Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

        Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);

        return playerHorizontalMovement.magnitude > minimumMagnitude;
    }

    protected bool IsMovingUp(float minimumVelocity = 0.1f)
    {
        return getPlayerVerticleVelocity().y > minimumVelocity;
    }

    protected bool IsMovingDown(float minimumVelocity = 0.1f)
    {
        return getPlayerVerticleVelocity().y < -minimumVelocity;
    }

    public virtual void Update()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void OnAnimationEnterEvent()
    {

    }
    public virtual void OnAnimationExitEvent()
    {

    }
    public virtual void OnAnimationTransitionEvent()
    {

    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGround(collider);
            return;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGroundExited(collider);
            return;
        }
    }


    private void Move()
    {
        AdjustSpeed = stateMachine.Player.AdjustSpeed;
        if (stateMachine.ReusableData.movementInput == Vector2.zero || stateMachine.ReusableData.movementSpeedModifier == 0f)
        {
            return;
        }

        Vector3 movementDirection = GetMovementInputDirection();

        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GettargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMoveMentSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * AdjustSpeed * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }

    protected Vector3 GettargetRotationDirection(float targetRotation)
    {
        return Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
    }
    private float Rotate(Vector3 direction)
    {
        float directionAngle = updateTargetRotation(direction);
        
        RotateTowardsTargetRotation();
        
        return directionAngle;
    }

    protected  float updateTargetRotation(Vector3 direction, bool shouldConsiderCamera = true)
    {
        float directionAngle = GetDirectionAngle(direction);
        if (shouldConsiderCamera)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }
        
        if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
        stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }
//Handle smooth rotation
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

        if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimetoReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

        stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

        stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
    }
// camera Angle maths
    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360;
        }

        return directionAngle;
    }
// rotation of the angle
    private float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void ReadMovementInput()
    {
        stateMachine.ReusableData.movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 PlayerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

        PlayerHorizontalVelocity.y = 0f;

        return PlayerHorizontalVelocity;
    }

    protected Vector3 getPlayerVerticleVelocity()
    {
        return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
    }

    protected float GetMoveMentSpeed()
    {
        return movementData.baseSpeed * stateMachine.ReusableData.movementSpeedModifier * stateMachine.ReusableData.movementOnSlopesSpeedModifier;
    }

    protected void SetBaseRotationData()
    {
        stateMachine.ReusableData.RotationData = movementData.BaseRotationData;

        stateMachine.ReusableData.TimetoReachTargetRotation = stateMachine.ReusableData.RotationData.targetRotoationReachTime;
    }
    
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ReusableData.movementInput.x, 0f, stateMachine.ReusableData.movementInput.y);
    }

    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;
    }

    protected void ResetVerticalVelocity()
    {
        Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
    }
}

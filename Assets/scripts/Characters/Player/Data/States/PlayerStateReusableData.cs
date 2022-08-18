using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusableData 
{
  public Vector2 movementInput { get; set; }
  
  public float movementSpeedModifier { get; set; } = 1f;
  
  public float movementOnSlopesSpeedModifier { get; set; } = 1f;

  public float MovementDecelerationForce { get; set; } = 1f;

  public bool shouldWalk { get; set; }

  public bool shouldSprint { get; set; }

  private Vector3 currentTargetRotation;
  private Vector3 timetoReachTargetRotation;
  private Vector3 dampedTargetRotationCurrentVelocity;
  private Vector3 dampedTargetRotationPassedTime;

  public ref Vector3 CurrentTargetRotation
  {
    get
    {
      return ref currentTargetRotation;
    }
    
  }
  public ref Vector3 TimetoReachTargetRotation
  {
    get
    {
      return ref timetoReachTargetRotation;
    }
    
  }
  public ref Vector3 DampedTargetRotationCurrentVelocity
  {
    get
    {
      return ref dampedTargetRotationCurrentVelocity;
    }
    
  }
  public ref Vector3 DampedTargetRotationPassedTime
  {
    get
    {
      return ref dampedTargetRotationPassedTime;
    }
  }

  public Vector3 CurrentJumpForce {get; set; }

  public PlayerRotationData RotationData {get; set; }

}

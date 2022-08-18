using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CapsuleCollidersUtility
{
    public CapsuleColliderData CapsuleColliderData { get; private set; }
    
    [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
    
    [field: SerializeField] public SlopeData SlopeData { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if (CapsuleColliderData != null)
        {
            return;
        }

        CapsuleColliderData = new CapsuleColliderData();
        
        CapsuleColliderData.Initialize(gameObject);
    }

    public void CalculateCapsuleColliderDimensions()
    {
        SetCapsuleColliderRadius(DefaultColliderData.radius);
        
        SetCapsuleColliderHeight(DefaultColliderData.height * (1f - SlopeData.stepHeightPercentage));

        RecalculateCapsuleColliderCenter();

        float halfColliderHeight = CapsuleColliderData.collider.height / 2f;
        if ( halfColliderHeight < CapsuleColliderData.collider.radius)
        {
            SetCapsuleColliderRadius(halfColliderHeight);
        }
        CapsuleColliderData.UpdateColliderData();
    }

    public void SetCapsuleColliderRadius(float radius)
    {
        CapsuleColliderData.collider.radius = radius;
    }
    
    public void SetCapsuleColliderHeight(float height)
    {
        CapsuleColliderData.collider.height = height;
    }

    public void RecalculateCapsuleColliderCenter()
    {
        //take the height of the capsule of the player and subtract by the "Default height"
        float colliderHeightDifference = DefaultColliderData.height - CapsuleColliderData.collider.height;
        
        //take new ColliderCenter
        Vector3 newColliderCenter = new Vector3(0f, DefaultColliderData.CenterY + (colliderHeightDifference / 2f), 0f);
        
        //set the new Center to the capsule
        CapsuleColliderData.collider.center = newColliderCenter;
        
        //set radius to new height so the collider will become smaller rather than move
    }
}

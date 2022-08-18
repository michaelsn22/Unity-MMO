using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerTriggerColliderData
{
    [field: SerializeField] public BoxCollider GroundCheckCollider {get; private set;}
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerCapsuleColliderUtility : CapsuleCollidersUtility
{
    [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData {get; private set;}
}

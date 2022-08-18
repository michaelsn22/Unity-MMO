using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerAirborneData
{
    [field: SerializeField] public PlayerJumpData JumpData { get; private set; }

    [field: SerializeField] public PlayerFallData FallData { get; private set; }
}

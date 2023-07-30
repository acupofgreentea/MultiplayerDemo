﻿using Fusion;
using UnityEngine;

public struct PlayerNetworkInput : INetworkInput
{
    public Vector3 MovementInput { get; set; }
    public bool HasInput { get; set; }
}
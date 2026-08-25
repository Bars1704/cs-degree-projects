using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingState
{
    public float MaxVelocity { get; }
    public Vector3 CurrentPos { get; }
    public Vector3 Velocity { get; }
    public Vector3 GetSpeed();
}
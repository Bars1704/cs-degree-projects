using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityMovingState
{
    protected const float _gizmosLengthMultiplier = 5;
    protected Transform Transform;
    public float MaxVelocity { protected set; get; }
    public Vector3 CurrentPos => Transform.position;
    public Vector3 Velocity { get; set; }

    public UnityMovingState(Transform transform, Vector3 startVelocity, float maxSpeed)
    {
        Transform = transform;
        Velocity = startVelocity;
        MaxVelocity = maxSpeed;
    }

    public static Vector3 Truncate(Vector3 vector, float dist)
    {
        if (vector.magnitude > dist)
            return vector.normalized * dist;
        else
            return vector;
    }

    public Vector3 CalculatedSpeed => Truncate(GetSpeed(), MaxVelocity);
    protected abstract Vector3 GetSpeed();

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(CurrentPos, (CurrentPos + Velocity * _gizmosLengthMultiplier));
    }
}
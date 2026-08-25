using System;
using UnityEngine;

public class PursuitState : SeekableMovingState
{
    public event Action<IPursuitable> OnReach;
    
    private readonly IPursuitable _target;

    private const float REACH_DISTANCE = 1.5f;
    private Vector3 _seekingPos
    {
        get
        {
            var renderingRange = (CurrentPos - _target.Position).magnitude / MaxVelocity;
            return _target.Position + _target.Velocity* renderingRange;
        }
    }

    protected override Vector3 GetSpeed()
    {
        if ((CurrentPos - _target.Position).sqrMagnitude <= REACH_DISTANCE)
        {
            OnReach?.Invoke(_target);
            return Vector3.zero;
        }
        return Seek(_seekingPos);
    }
    
    public PursuitState(Transform transform, Vector3 startVelocity, float maxSpeed, IPursuitable target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }
}
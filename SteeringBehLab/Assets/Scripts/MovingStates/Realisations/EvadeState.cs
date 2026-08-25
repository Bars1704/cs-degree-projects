using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : SeekableMovingState
{
    private IMovingState _target;

    private Vector3 _evadingPos
    {
        get
        {
            var renderingRange = (CurrentPos - _target.CurrentPos).magnitude / MaxVelocity;
            return _target.CurrentPos + _target.Velocity * renderingRange;
        }
    }

    public EvadeState(Transform transform, Vector3 startVelocity, float maxSpeed, IMovingState target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CurrentPos, _evadingPos);
        Gizmos.DrawWireSphere(_evadingPos, 0.2f);
    }


    protected override Vector3 GetSpeed() => Flee(_evadingPos);
}
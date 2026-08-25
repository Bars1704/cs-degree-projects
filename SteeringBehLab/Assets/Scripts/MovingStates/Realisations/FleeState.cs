using UnityEngine;

public class FleeState : SeekableMovingState
{
    private Transform _target;

    public FleeState(Transform transform, Vector3 startVelocity, float maxSpeed, Transform target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }

    protected override Vector3 GetSpeed() => Flee(_target.position);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CurrentPos, _target.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(CurrentPos, CurrentPos + GetSpeed());
    }
}
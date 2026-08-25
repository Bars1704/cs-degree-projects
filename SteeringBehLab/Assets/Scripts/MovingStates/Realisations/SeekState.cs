using UnityEngine;

public class SeekState : SeekableMovingState
{
    private Transform _target;

    protected override Vector3 GetSpeed() => Seek(_target.position);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CurrentPos, _target.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(CurrentPos, GetSpeed());
    }

    public SeekState(Transform transform, Vector3 startVelocity, float maxSpeed, Transform target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }
}
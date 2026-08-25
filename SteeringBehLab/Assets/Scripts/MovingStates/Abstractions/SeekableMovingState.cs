using UnityEngine;

public abstract class SeekableMovingState : UnityMovingState
{
    protected Vector3 Seek(Vector3 targetPos)
    {
        var desiredVelocity = (targetPos - CurrentPos) * MaxVelocity;
        return desiredVelocity - Velocity;
    }

    protected Vector3 Flee(Vector3 evadingPos)
    {
        var desiredVelocity = (evadingPos - CurrentPos) * MaxVelocity;
        return -desiredVelocity - Velocity;
    }

    protected SeekableMovingState(Transform transform, Vector3 startVelocity, float maxSpeed)
        : base(transform, startVelocity, maxSpeed)
    {
    }
}
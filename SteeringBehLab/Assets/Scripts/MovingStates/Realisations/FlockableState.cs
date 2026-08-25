using UnityEngine;

namespace MovingStates.Realisations
{
    using System.Collections.Generic;
    using Abstractions;

    public class FlockableState : SeekableMovingState
    {
        private List<IFlockable> _flock;

        private Vector3 result;
        private Vector3 socialDistanceForce;
        private Vector3 avgVelocity;
        private Vector3 avgPos;

        protected override Vector3 GetSpeed()
        {
            avgPos = Vector3.zero;
            avgVelocity = Vector3.zero;
            socialDistanceForce = Vector3.zero;
            foreach (var flockmate in _flock)
            {
                avgPos += flockmate.Position;
                avgVelocity += flockmate.Velocity;

                socialDistanceForce += Truncate(Flee(flockmate.Position), 0.6f * MaxVelocity);
            }

            if (_flock.Count != 0)
            {
                avgPos /= _flock.Count;
                avgVelocity /= _flock.Count;
            }

            Truncate(vector: avgVelocity, MaxVelocity * 0.2f);

            result = Truncate(Truncate(socialDistanceForce, MaxVelocity) + Truncate(Seek(avgPos), MaxVelocity) +
                              Truncate(avgVelocity, MaxVelocity), MaxVelocity);
            return result;
        }

        public FlockableState(Transform transform, Vector3 startVelocity, float maxSpeed,
            List<IFlockable> flock) : base(
            transform, startVelocity, maxSpeed)
        {
            _flock = flock;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(CurrentPos, CurrentPos + Truncate(Seek(avgPos), MaxVelocity));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(CurrentPos, CurrentPos + Truncate(socialDistanceForce, MaxVelocity));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(CurrentPos, CurrentPos + Truncate(avgVelocity, MaxVelocity));
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(CurrentPos, CurrentPos + result);
        }
    }
}
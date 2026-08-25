namespace MovingStates.Abstractions
{
    using UnityEngine;

    public interface IFlockable
    {
        public Vector3 Velocity { get; }
        public Vector3 Position { get; }
    }
}
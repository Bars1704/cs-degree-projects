using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour, IPursuitable
{
    [SerializeField, Min(0)] protected float _maxSpeed = 5;
    [SerializeField, Min(0)] protected float _shrapness = 0.5f;

    public Vector3 Velocity { get; protected set; } = Vector3.zero;
    public Vector3 Position => transform.position;
    protected abstract List<UnityMovingState> GetMovingStates();
    private List<UnityMovingState> _currentStates;

    private Vector3 prevVelocity;

    private void Move()
    {
        _currentStates = GetMovingStates();

        var steering = Vector3.zero;
        _currentStates.ForEach(x => steering += x.CalculatedSpeed);

        steering *= _shrapness;
        var maxSpeed = _currentStates.Max(x => x.MaxVelocity);
        Velocity = UnityMovingState.Truncate(steering + Velocity, maxSpeed);

        _currentStates.ForEach(x => x.Velocity = Velocity);

        transform.Translate(Velocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        _currentStates = GetMovingStates();
        Move();
    }

    protected virtual void OnDrawGizmos()
    {
        _currentStates?.ForEach(x => x.OnDrawGizmos());
    }
}
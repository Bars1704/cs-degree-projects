using System.Collections.Generic;
using UnityEngine;

public class Bunny : AnimalBase
{
    [SerializeField] private float _detectionCirlceRadius = 6;

    private InsideBoxState _insideBoxState;
    private WanderState _wanderState;

    private void Start()
    {
        _wanderState = new WanderState(transform, Velocity, _maxSpeed / 5f, 5, 3, 5);
        _insideBoxState =
            new InsideBoxState(transform, Velocity, _maxSpeed / 5f, new Rect(-Vector2.one * 50, Vector2.one * 100));

        _insideBoxState.OnEndAvoid += _wanderState.Random;
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        var c = Physics2D.OverlapCircleAll(transform.position, _detectionCirlceRadius);
        if (c.Length <= 1)
            return new List<UnityMovingState>() { _wanderState, _insideBoxState };
        else
            return new List<UnityMovingState>()
            {
                _insideBoxState,
                new FleeState(transform, Velocity, _maxSpeed, c[1].transform)
            };
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionCirlceRadius);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wolf : AnimalBase
{
    [SerializeField] private float _detectionCirlceRadius = 4;
    [SerializeField] private float _maxHunger = 10;

    private float _hunger;
    private float _hungerAmount = 0.1f;

    private InsideBoxState _insideBoxState;
    private WanderState _wanderState;

    private void Start()
    {
        _wanderState = new WanderState(transform, Velocity, _maxSpeed / 5f, 5, 3, 5);
        _insideBoxState =
            new InsideBoxState(transform, Velocity, _maxSpeed / 5f, new Rect(-Vector2.one * 50, Vector2.one * 100));

        _insideBoxState.OnEndAvoid += _wanderState.Random;
        _hunger = _maxHunger;
    }

    private void Update()
    {
        _hunger -= _hungerAmount * Time.deltaTime;
        if (_hunger <= 0)
            Kill();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionCirlceRadius);
    }

    private void Eat(IPursuitable target)
    {
        (target as IKillable)?.Kill();
        _hunger = _maxHunger;
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        var target = Physics2D.OverlapCircleAll(transform.position, _detectionCirlceRadius)
            .OrderBy(x => (transform.position - x.transform.position).sqrMagnitude)
            .FirstOrDefault(x =>
            {
                var comp = x.GetComponent<IPursuitable>();
                return comp != default && !(comp is Wolf) ;
            });

        var defaultResult = new List<UnityMovingState> { _wanderState, _insideBoxState };

        if (target == default)
            return defaultResult;

        var pursuitState = new PursuitState(transform, Velocity, _maxSpeed, target.GetComponent<IPursuitable>());
        pursuitState.OnReach += Eat;
        return new List<UnityMovingState>
        {
            _insideBoxState,
            pursuitState
        };
    }
}
using System.Collections.Generic;
using System.Linq;
using MovingStates.Abstractions;
using MovingStates.Realisations;
using UnityEngine;

public class Deer : AnimalBase, IFlockable
{
    [SerializeField]
    private float _detectionCirlceRadius = 10;

    [SerializeField]
    private float circleDistance = 5;

    [SerializeField]
    private float circleRadius = 3;

    [SerializeField]
    private float shake = 5;


    private InsideBoxState _insideBoxState;
    private WanderState _wanderState;
    private FlockableState _flockableState;

    private void Start()
    {
        _wanderState = new WanderState(transform, Velocity, _maxSpeed / 5f, circleDistance, circleRadius, shake);
        _insideBoxState =
            new InsideBoxState(transform, Velocity, _maxSpeed / 5f, new Rect(-Vector2.one * 50, Vector2.one * 100));
        _flockableState = new FlockableState(transform, Velocity, _maxSpeed / 5f,
            new List<Deer>().Cast<IFlockable>().ToList());

        _insideBoxState.OnEndAvoid +=
            () => _wanderState = new WanderState(transform, Velocity, _maxSpeed / 5f, 5, 3, 5);
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        var c = Physics2D.OverlapCircleAll(transform.position, _detectionCirlceRadius);
        if (c.Length <= 1)
            return new List<UnityMovingState>() {_wanderState, _insideBoxState};

        var flock = new List<Deer>();

        foreach (var collider2D in c)
        {
            var flockMate = collider2D.GetComponent<Deer>();
            if (flockMate != null)
            {
                flock.Add(flockMate);
                continue;
            }

            var neutral = collider2D.GetComponent<Bunny>();
            // Костыль, чек на то, надо ли избегать цели. Имхо лучше сделать статическую матрицу ссыкования, наверное.
            // Олени по сути только зайцев не боятся. Вот и чек через зайца. Но матрица страха лучше. Но мне кажется,
            // что кто-то придумает поумнее решение.

            if (neutral == null)
            {
                return new List<UnityMovingState>()
                {
                    _insideBoxState,
                    new FleeState(transform, Velocity, _maxSpeed, collider2D.transform),
                    new FlockableState(transform, Velocity, _maxSpeed, flock.Cast<IFlockable>().ToList())
                };
            }
        }

        return new List<UnityMovingState>()
        {
            _insideBoxState,
            new FlockableState(transform, Velocity, _maxSpeed, flock.Cast<IFlockable>().ToList()),
            // new WanderState(transform, velocity, _maxSpeed / 5, circleDistance, circleRadius, shake)
        };
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionCirlceRadius);
    }

    public Vector3 Position => transform.position;
}
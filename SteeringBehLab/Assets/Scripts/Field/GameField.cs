using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public Vector2 Size => Vector3.one * RectSize;

    [SerializeField] private float RectSize;

    [SerializeField] private AnimalsFabric _animalsFabric;

    [SerializeField] private Player _player;

    private void FixedUpdate()
    {
        var killable = new List<IKillable>();

        if (IsOutsideBox(_player.transform))
            killable.Add(_player);

        foreach (var animal in _animalsFabric.Animals)
            if (IsOutsideBox(animal.transform))
                killable.Add(animal);

        killable.ForEach(x => x.Kill());
    }

    private bool IsOutsideBox(Transform target)
    {
        var magnitude = target.position - transform.position;
        magnitude.x = Mathf.Abs(magnitude.x);
        magnitude.y = Mathf.Abs(magnitude.y);

        return (magnitude.x > Size.x / 2 || magnitude.y > Size.y / 2);
    }
}
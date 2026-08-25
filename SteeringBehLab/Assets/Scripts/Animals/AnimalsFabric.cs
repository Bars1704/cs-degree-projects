using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalsFabric : MonoBehaviour
{
    [Serializable]
    private struct AnimalSet
    {
        public AnimalBase AnimalBase;
        public int Count;
        public bool IsFlockAnimal;
        public int FlockSize;
    }

    [SerializeField]
    private GameField _gameField;

    [SerializeField]
    private List<AnimalSet> _spawningSettings;

    public List<AnimalBase> Animals { get; private set; }

    private void Start()
    {
        Animals = new List<AnimalBase>();
        foreach (var animalSet in _spawningSettings)
        {
            if (animalSet.IsFlockAnimal)
            {
                for (int i = 0; i < animalSet.Count; i++)
                {
                    Vector3 flockPosition = Random.insideUnitCircle * Mathf.Min(_gameField.Size.x, _gameField.Size.y) /
                                            2.5f;
                    flockPosition += _gameField.transform.position;

                    for (int j = 0; j < animalSet.FlockSize; j++)
                    {
                        var pos = Random.insideUnitCircle * animalSet.FlockSize / 1.5f;
                        var animal = Instantiate(animalSet.AnimalBase, _gameField.transform);
                        animal.transform.position = flockPosition + new Vector3(pos.x, pos.y, 0);
                        animal.OnDeath += OnAnimalDeath;
                        Animals.Add(animal);
                        i++;
                        if (i >= animalSet.Count)
                        {
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < animalSet.Count; i++)
            {
                Vector3 pos = Random.insideUnitCircle * Mathf.Min(_gameField.Size.x, _gameField.Size.y) / 2.5f;
                pos += _gameField.transform.position;

                var animal = Instantiate(animalSet.AnimalBase, _gameField.transform);
                animal.transform.position = pos;
                animal.OnDeath += OnAnimalDeath;
                Animals.Add(animal);
            }
        }
    }

    private void OnAnimalDeath(AnimalBase animal)
    {
        Animals.Remove(animal);
        animal.OnDeath -= OnAnimalDeath;
    }
}
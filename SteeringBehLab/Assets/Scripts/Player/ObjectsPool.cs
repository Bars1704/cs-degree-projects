using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsPool = new List<GameObject>();
    [SerializeField] private int _initialAmount;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parentContainer;
    
    private void Awake()
    {
        for (var i = 0; i < _initialAmount; i++)
        {
            var obj = Instantiate(_prefab, _parentContainer);
            obj.SetActive(false);
            _objectsPool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        var availableObj = _objectsPool.FirstOrDefault(o => !o.activeSelf);

        if (availableObj != default)
        {
            availableObj.SetActive(true);
            return availableObj;
        }

        var newObj = Instantiate(_prefab);
        _objectsPool.Add(Instantiate(newObj));
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _objectsPool.Add(obj);
    }
}
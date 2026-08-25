using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public readonly List<GameObject> pool;
    readonly GameObject Example;
    public ObjectPool(GameObject example)
    {
        pool = new List<GameObject> { };
        if (example.activeInHierarchy)
        {
            pool.Add(example);
            example.SetActive(false);
        }
        Example = example;
    }
    public void UncheckAll()
    {
        foreach (var go in pool)
        {
            go.SetActive(false);
        }
    }
    public GameObject GetElement()
    {
        var current = pool.Find((x) => !x.activeSelf);
        if (current == null)
        {
            return Create();
        }
        else
        {
            current.SetActive(true);
            return current;
        }
    }
    GameObject Create()
    {
        var newob = GameObject.Instantiate(Example);
        pool.Add(newob);
        return newob;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float Delay = 10;
    public GameObject SpawnMe;
    public bool AutoMode = false;
    void Start()
    {
        if (AutoMode)
        {
            StartCoroutine(SpawnEveryTime());
        }
    }
    IEnumerator SpawnEveryTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            Spawn();
        }
    }
     public void Spawn()
    {
        Instantiate(SpawnMe, gameObject.transform.position, gameObject.transform.rotation);
    }
    public void Spawn(GameObject obj)
    {
        Instantiate(obj, gameObject.transform.position, gameObject.transform.rotation);
    }
}

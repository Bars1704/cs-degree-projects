using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    public GameObject spawnme;
    private void OnDestroy()
    {
        Instantiate(spawnme, gameObject.transform.position, gameObject.transform.rotation);
    }
}

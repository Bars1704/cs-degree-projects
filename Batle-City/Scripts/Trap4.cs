using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap4 : MonoBehaviour
{
    float Pause = 0.15f;
    public GameObject bullet;
    public int BulletCount = 7;
    public int Count = 7;
    Vector3 pos;
    public bool InstantFire = true;
    void Start()
    {
        pos = gameObject.transform.position;
        bullet.GetComponent<BulletScript>().PlayerId = -1;
        bullet.GetComponent<BulletScript>().OnlyDirect = false;
        StartCoroutine(FireAll());
    }
    IEnumerator SpeedBoost(int count)
    {
        var Step = 360 / count;
        var angle = 0;
        for (int i = 0; i < count; i++)
        {
            Instantiate(bullet, pos, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(Pause);
            angle += Step;
        }
        
    }

    IEnumerator FireAll ()
    {
        for (int x = 0; x < Count; x++)
        {
            if (InstantFire)
            {
                var Step = 360 / BulletCount;
                var angle = 0;
                for (int i = 0; i < BulletCount; i++)
                {
                    Instantiate(bullet, pos, Quaternion.Euler(0, 0, angle));
                    angle += Step;
                }
            }
            else
            {
                StartCoroutine(SpeedBoost(BulletCount));
            }
            yield return new WaitForSeconds(Pause);
        }
        Destroy(gameObject);
    }
}

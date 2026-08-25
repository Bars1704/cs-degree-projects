using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blust : MonoBehaviour
{
    public int Damage = 10;
    float StartTime;
    float Pause = 0.1f;

    private void Start()
    {
        StartTime = Time.time;
    }
    private void Update()
    {
        if(Time.time > StartTime + Pause)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DestroyByBullet")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<Health>().Damage(Damage);
        }
    }
}

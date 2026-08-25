using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public int Damage;
    private float NextDamage = 0f;
    private float reload = 0.5f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&Time.time>NextDamage)
        {
            collision.gameObject.GetComponent<Health>().Damage(Damage);
            gameObject.GetComponent<Health>().Damage(3);
            NextDamage += reload;
        }
    }
    
}

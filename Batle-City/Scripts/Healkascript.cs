using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healkascript : MonoBehaviour
{
    public int healvalue = 8;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Heal(healvalue);
            Destroy(gameObject);
        }
    }
}

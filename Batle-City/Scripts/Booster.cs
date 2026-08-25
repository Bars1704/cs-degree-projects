using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float BoostValue;
    public float BoostTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Tank>().BoostSpeed(BoostValue,BoostTime);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Where;

    public void IgnoreObject(float Time, Collider2D obj)
    {
        StartCoroutine(_Ignore(obj,Time));
    }
    IEnumerator _Ignore(Collider2D col1 , float Time)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col1, true);
        yield return new WaitForSeconds(Time);
        if (col1 != null)
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col1, false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = Where.transform.position;
        Where.GetComponent<Teleport>().IgnoreObject(4, collision.collider);
    }
}

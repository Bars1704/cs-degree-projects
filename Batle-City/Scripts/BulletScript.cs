using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float MovingSpeed = 1;
    public Rigidbody2D rb;
    protected float angle;
    public int Damage;
    public List<string> taglist;
    public int PlayerId;
    public bool OnlyDirect = true;
    void Start()
    {
        if (OnlyDirect)
        {
            int x = (int)(transform.rotation.eulerAngles.z / 90);
            switch (x)
            {
                case 0:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 2:
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 3:
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
            }
        }

        angle = transform.rotation.eulerAngles.z;
        MakeInvisible();
    }       
    public void Move4()
    {
         switch (angle)
         {
             case 0f:
                 rb.MovePosition(rb.position + new Vector2(0, 1 * MovingSpeed * Time.deltaTime));
                 break;
             case 180f:
                 rb.MovePosition(rb.position + new Vector2(0, -1 * MovingSpeed * Time.deltaTime));
                 break;
             case 90f:
                 rb.MovePosition(rb.position + new Vector2(-1 * MovingSpeed * Time.deltaTime, 0));
                 break;
             case 270f:
                 rb.MovePosition(rb.position + new Vector2(1 * MovingSpeed * Time.deltaTime, 0));
                 break;
            }
    }
    void Update()
    {
        if (OnlyDirect)
        {
            Move4();
        }
        else
        {
            float newangle = angle+90;
            float x = Mathf.Cos(newangle * Mathf.Deg2Rad);
            float y = Mathf.Sin(newangle * Mathf.Deg2Rad);
            var moveto = new Vector2(x, y) * MovingSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveto);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Root"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Damagable")
        {
            var health = collision.gameObject.GetComponent<Health>();
            health.Damage(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            var id = collision.gameObject.transform.Find("Weapons").gameObject.GetComponent<Weapon>().PlayerId;
            if(id == PlayerId)
            {
                Destroy(gameObject);
            }
            else
            {
                var health = collision.gameObject.GetComponent<Health>();
                health.Damage(Damage);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "DestroyByBullet")
        {
            var x = collision.gameObject.GetComponent<BulletScript>();
            if (x == null || x.PlayerId != PlayerId)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }

        }
        else if (collision.gameObject.tag == "Glass")
        {
            Destroy(collision.gameObject);
        }
    }
    public void MakeInvisible()
    {
        StartCoroutine(Invis());
    }
    IEnumerator Invis()
    {
        float Pause = 0.02f;
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.isTrigger = true;
        yield return new WaitForSeconds(Pause);
        col.isTrigger = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFire : BulletScript
{
    Health myhealth;
    SpriteRenderer spr;
    Color newColor;
    public int chancetodamage = 2;
    void Start()
    {
        myhealth = gameObject.GetComponent<Health>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        angle = transform.rotation.eulerAngles.z;
        rb = gameObject.GetComponent<Rigidbody2D>();
        newColor = spr.color;
        angle += 90;
    }
    void FixedUpdate()
    {
        myhealth.Damage(1);
        float value = 0.015f;
        newColor.r += value;
        newColor.g -= value;
        spr.color = newColor;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);
        var moveto = new Vector2(x, y) * MovingSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveto);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Root") || collision.gameObject.tag == "DestroyByBullet") { }
        else if (collision.gameObject.tag == "Damagable")
        {
            var helth = collision.gameObject.GetComponent<Health>();
            if (Random.Range(0, chancetodamage) == 0)
            {
                helth.Damage(Damage);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            var helth = collision.gameObject.GetComponent<Health>();
            var id = collision.gameObject.transform.Find("Weapons").gameObject.GetComponent<Weapon>().PlayerId;
            if (helth.HealtValue > 0 && id != PlayerId)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health.Damage(Damage);
            }
        }
        else if (collision.gameObject.tag == "Glass")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}

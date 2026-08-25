using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Part
{
    public GameObject bullet;
    public float fireRate = 0.5F;
    public int Damage;
    private float nextFire = 0.0F;
    public KeyCode shoot = KeyCode.Space;
    public int PlayerId;
    public AudioClip sound;
    protected AudioSource aSource;
    public Rigidbody2D rb;

    public void PlaySound()
    {
        if (ArbitrScript.settings.Sound)
            aSource.PlayOneShot(sound);
    }
    protected void InitializeWeapon()
    {
        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        var id = bullet.gameObject.GetComponent<BulletScript>();
        id.PlayerId = PlayerId;
        id.Damage = Damage;
        aSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        InitializeWeapon();
    }
    virtual protected void Shoot() { Debug.Log("Shoot"); }
    protected Vector3 MakeOtstup(Vector3 pos)
    {
        float otstup = 30;
        gameObject.transform.parent.GetComponent<Tank>().Make90();
        switch (transform.rotation.eulerAngles.z)
        {
            case 0: pos.y += otstup; break;
            case 90: pos.x -= otstup; break;
            case 180: pos.y -= otstup; break;
            case 270: pos.x += otstup; break;
        }
        return pos;
    }
    protected void Update()
    {
        if (Input.GetKey(shoot) && Time.time > nextFire)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }
}

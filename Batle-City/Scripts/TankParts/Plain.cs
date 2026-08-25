using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain : Weapon
{
    protected override void Shoot()
    {
        bullet.transform.rotation = transform.rotation;
        var pos = MakeOtstup(transform.position);
        Instantiate(bullet, pos, transform.rotation);
        PlaySound();
    }
}

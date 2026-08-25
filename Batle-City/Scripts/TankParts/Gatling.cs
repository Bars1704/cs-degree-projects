using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : ApproxWeapon
{
    float LastGatlingTime;
    float Period = 0.1f;
    float GatlingRate;
    float GatlingApprox;
    float GatLingApproxStep = 0.1f;
    float GatLingRateStep = 0.7f;
    void Start()
    {
        InitializeWeapon();
        GatlingRate = fireRate;
        Approximate = 15;
    }
    protected override void Shoot()
    {
        if (Time.time - LastGatlingTime > Period + fireRate)
        {
            GatlingApprox = Approximate;
            fireRate = GatlingRate;
        }
        LastGatlingTime = Time.time;
        bullet.GetComponent<BulletScript>().OnlyDirect = false;
        var EylerAngles = transform.rotation.eulerAngles;
        var apr = Random.Range(GatlingApprox * -1, GatlingApprox);
        EylerAngles.z += apr;
        if (fireRate >= 0.15f)
        {
            fireRate *= GatLingRateStep;
        }
        GatlingApprox -= GatLingApproxStep;
        var pos = MakeOtstup(transform.position);
        Instantiate(bullet, pos, Quaternion.Euler(EylerAngles));
        PlaySound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    LineRenderer _lineRend;
    void Start()
    {
        InitializeWeapon();
        _lineRend = GetComponent<LineRenderer>();
    }
    protected override void Shoot()
    {
        var angle = transform.forward;
        switch (transform.rotation.eulerAngles.z)
        {
            case 0: angle.y += 90; break;
            case 90: angle.x -= 90; break;
            case 180: angle.y -= 90; break;
            case 270: angle.x += 90; break;
        }
        Ray2D ray = new Ray2D(transform.position, angle);
        RaycastHit2D hit2D = Physics2D.Raycast(MakeOtstup(ray.origin), ray.direction, 1000f);
        _lineRend.SetPosition(0, MakeOtstup(ray.origin));
        Color Start = _lineRend.startColor;
        Color End = _lineRend.endColor;
        Start.a = 1000;
        End.a = 1000;
        _lineRend.startColor = Start;
        _lineRend.endColor = End;
        if (hit2D.collider != null)
        {
            GameObject target = hit2D.collider.gameObject;
            _lineRend.SetPosition(1, hit2D.point);
            if (target.tag == "Damagable")
            {
                target.GetComponent<Health>().Damage(Damage);
            }
            else if (target.tag == "DestroyByBullet")
            {
                Destroy(target);
            }
            else if (target.tag == "Player" && PlayerId != target.transform.Find("Weapons").gameObject.GetComponent<Weapon>().PlayerId)
            {
                target.GetComponent<Health>().Damage(Damage);
            }
        }
        else
        {
            _lineRend.SetPosition(1, (Vector2)MakeOtstup(ray.origin) + ray.direction * 1000f);
        }
        StartCoroutine(LineColour());
        PlaySound();
    }
    IEnumerator LineColour()
    {
        var step = 0.15f;
        Color Start = _lineRend.startColor;
        Color End = _lineRend.endColor;
        do
        {
            for (int i = 0; i < _lineRend.colorGradient.colorKeys.Length; i++)
            {
                Start.a -= step;
                End.a -= step;
                _lineRend.startColor = Start;
                _lineRend.endColor = End;
            }
            yield return new WaitForSeconds(0.1f);
        } while (_lineRend.startColor.a > 0);
    }
}

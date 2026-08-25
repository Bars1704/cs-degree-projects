using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Selecter : MonoBehaviour
{
    private string FindName;
    int num = 0;
    private GameObject[] SpriteList;
    Image Sprite;
    GameObject Tank;
    GameObject changeMe;
    Text stats;
    public WhatShow ShowType;
    public enum WhatShow
    {
        Weapon, Body, Wheel
    }

    private void Start()
    {
        switch (ShowType)
        {
            case WhatShow.Body:
                FindName = "Body";
                break;
            case WhatShow.Wheel:
                FindName = "Wheels";
                break;
            case WhatShow.Weapon:
                FindName = "Weapons";
                break;
        }
        SpriteList = Resources.LoadAll<GameObject>("TankParts/" + FindName + "/") as GameObject[];
        Tank = GameObject.Find("Tank");
        Sprite = GetComponent<Image>();
        stats = gameObject.transform.parent.GetChild(3).GetComponent<Text>();
        Swipe(0);
        ShowStats();
    }
    string GetWeaponStats()
    {
        StringBuilder str = new StringBuilder();
        var weapon = changeMe.GetComponent<Weapon>();
        str.Append("Name: " + weapon.Name);
        str.Append("\nHealth: " + weapon.healthvalue);
        str.Append("\nWeight: " + weapon.Weight);
        str.Append("\nDamage: " + weapon.Damage);
        str.Append("\nFire Rate: " + weapon.fireRate);
        if (weapon is ApproxWeapon)
        {
            var Apr = weapon as ApproxWeapon;
            str.Append("\nApprox: " + Apr.Approximate);
        }
        if (weapon is ShootGun)
        {
            var Apr = weapon as ShootGun;
            str.Append("\nBulletCount: " + Apr.BulletCount);
        }
        return str.ToString();
    }
    string GetBodyStats()
    {      
        StringBuilder str = new StringBuilder();
        var body = changeMe.GetComponent<Part>();
        str.Append("Name: " + body.Name);
        str.Append("\nHealth: " + body.healthvalue);
        str.Append("\nWeight: " + body.Weight);
        return str.ToString();
    }
    string GetWheelsStats()
    {
        StringBuilder str = new StringBuilder();
        var wheels = changeMe.GetComponent<Wheels>();
        str.Append("Name: " + wheels.Name);
        str.Append("\nHealth: " + wheels.healthvalue);
        str.Append("\nWeight: " + wheels.Weight);
        str.Append("\nSpeed: " + wheels.Speed);
        return str.ToString();
    }
    void ShowStats()
    {
        switch (ShowType)
        {
            case WhatShow.Weapon:
                stats.text = GetWeaponStats();
                break;
            case WhatShow.Body:
                stats.text = GetBodyStats();
                break;
            case WhatShow.Wheel:
                stats.text = GetWheelsStats();
                break;
        }
    }
    void Swipe(int num)
    {
        Sprite.sprite = SpriteList[num].GetComponent<SpriteRenderer>().sprite;
        changeMe = Tank.transform.Find(FindName).gameObject;
        string name = changeMe.name;
        var scale = Tank.transform;
        Destroy(changeMe);
        changeMe = SpriteList[num];
        changeMe.name = name;
        changeMe.transform.localScale = scale.localScale;
        changeMe.transform.rotation = scale.rotation;

        GameObject child = Instantiate(changeMe);
        child.transform.parent = Tank.transform;
        child.transform.localPosition = name == "Weapons" ? new Vector3(0, 0.3F, 0) : new Vector3(0, 0, 0);
        child.name = name;
        Tank.GetComponent<Tank>().Refresh();
        ShowStats();
    }
    public void godown()
    {
        num = num - 1 < 0 ? SpriteList.Length - 1 : num - 1;
        Swipe(num);
    }
    public void goup()
    {
        num = num + 1 == SpriteList.Length ? 0 : num + 1;
        Swipe(num);
    }
}

using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Tank : MonoBehaviour
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public GameObject _wheels;
    public GameObject _weapon;
    public GameObject _body;

    public float speed;
    public Health health;


    public Rigidbody2D rb;
    public int DeathCount = 0;
    public GameObject RespawnPoint;

    void Reassign()
    {
        _wheels = transform.Find("Wheels").gameObject;
        _weapon = transform.Find("Weapons").gameObject;
        _body = transform.Find("Body").gameObject;
    }
    public void makeInvisible(bool i)
    {
        Reassign();
        _wheels.SetActive(i);
        _weapon.SetActive(i);
        _body.SetActive(i);
    }
    public void Refresh()
    {
        _wheels = gameObject.transform.Find("Wheels").gameObject;
        _weapon = gameObject.transform.Find("Weapons").gameObject;
        _body = gameObject.transform.Find("Body").gameObject;
        Wheels wheels = _wheels.GetComponent<Wheels>();
        Weapon weapon = _weapon.GetComponent<Weapon>();
        Part body = _body.GetComponent<Part>();
        health.HealtValue = wheels.healthvalue + weapon.healthvalue + body.healthvalue;
        health.MaxHealth = health.HealtValue;
        speed = (wheels.Speed) / (wheels.Weight + weapon.Weight + body.Weight);
    }
    private void Start()
    {
        health = gameObject.GetComponent<Health>();
        Refresh();
    }
  public void Make90()
    {
        if (transform.rotation.z != 180 && transform.rotation.z != 90 && transform.rotation.z != 270 && transform.rotation.z != 0)
        {
            var angle = transform.rotation.eulerAngles.z;
            var delt1 = Mathf.Abs(angle - 0);
            var delt2 = Mathf.Abs(angle - 90);
            var delt3 = Mathf.Abs(angle - 180);
            var delt4 = Mathf.Abs(angle - 270);
            if (delt1 < delt2 && delt1 < delt3 && delt1 < delt4)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (delt2 < delt1 && delt2 < delt3 && delt2 < delt4)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (delt3 < delt2 && delt3 < delt1 && delt3 < delt4)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (delt4 < delt2 && delt4 < delt3 && delt4 < delt1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
        }
    }
  private void FixedUpdate()
    {
        Make90();
        if (Input.GetKey(down))
        {
            rb.MovePosition(rb.position + new Vector2(0, -1 * speed));
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(up))
        {
            rb.MovePosition(rb.position + new Vector2(0, 1 * speed));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(left))
        {
            rb.MovePosition(rb.position + new Vector2(-1 * speed, 0));
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(right))
        {
            rb.MovePosition(rb.position + new Vector2(1 * speed, 0));
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (Input.anyKey == false)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
    public void BoostSpeed(float BoostValue, float Time)
    {
        StartCoroutine(SpeedBoost(BoostValue, Time));
    }
    IEnumerator SpeedBoost(float BoostValue, float Time)
    {
        speed += BoostValue;
        yield return new WaitForSeconds(Time);
        speed -= BoostValue;
    }
}

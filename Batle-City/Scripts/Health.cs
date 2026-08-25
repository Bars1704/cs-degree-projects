using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealtCHeckingTime = 3;
    public int HealtValue = 50;
    public int MaxHealth = 50;
    public Health(int value)
    {
        HealtValue = value;
        MaxHealth = value;
        InvokeRepeating("CheckHealth",0.1f,3);
    }
    public void ValidateHealth()
    {
        if (HealtValue <= 0)
        {
            Kill();
        }
        else if (HealtValue > MaxHealth)
        {
            HealtValue = MaxHealth;
        }
    }
    void Kill()
    {
        if (gameObject.tag == "Player")
        {
            StartCoroutine(PlayerDeath());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MaxHealth = HealtValue;
    }
    public void Damage(int damage)
    {
        HealtValue -= damage;
        ValidateHealth();
    }
    IEnumerator PlayerDeath()
    {
        var tank = gameObject.GetComponent<Tank>();
        tank.DeathCount++;
        gameObject.transform.position = tank.RespawnPoint.transform.position;
        gameObject.transform.rotation = tank.RespawnPoint.transform.rotation;
        tank.makeInvisible(false);
        yield return new WaitForSeconds(3);
        tank.makeInvisible(true);
        HealtValue = MaxHealth;
    }
    public void Heal(int healing)
    {
        HealtValue += healing;
        ValidateHealth();
    }
}

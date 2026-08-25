using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ObjectsPool _bulletsPool;
    [SerializeField, Min(0)] private int _bulletLifetime;
    [SerializeField, Min(0)] private float _bulletForce;
    [SerializeField, Min(0)] private int _ammo;
    public event Action<int> OnAmmoChanged;
    private int _currentAmmo;

    public void Reset()
    {
        _currentAmmo = _ammo;
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        UpdateUI();
        CheckForOutOfAmmo();
        ProcessInput();
    }

    private void Shoot()
    {
        if (_currentAmmo == 0)
        {
            return;
        }

        var bullet = _bulletsPool.GetObject();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;

        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

        _currentAmmo -= 1;

        StartCoroutine(BulletTimer(bullet));
    }

    private IEnumerator BulletTimer(GameObject obj)
    {
        yield return new WaitForSeconds(_bulletLifetime);

        _bulletsPool.ReturnObject(obj);
    }

    private void ProcessInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void UpdateUI()
    {
        OnAmmoChanged?.Invoke(_currentAmmo);
    }

    private void CheckForOutOfAmmo()
    {
        if (_currentAmmo == 0)
        {
            Reset();
        }
    }
}
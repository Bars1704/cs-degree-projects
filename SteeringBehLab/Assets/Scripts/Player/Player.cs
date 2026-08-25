using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody))]
public class Player : MonoBehaviour, IKillable, IPursuitable
{
    private SpriteRenderer _playerSprite;
    private Collider2D _playerCollider;
    private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;

    public void Kill()
    {
        _playerShooting.Reset();
        _playerMovement.Reset();
        StartCoroutine(InvisibleFrame());
    }

    private void Start()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerCollider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(InvisibleFrame());
    }

    private IEnumerator InvisibleFrame()
    {
        var color = _playerSprite.color;
        _playerSprite.color = Color.grey;
        _playerCollider.enabled = false;
        yield return new WaitForSecondsRealtime(3);
        _playerSprite.color = color;
        _playerCollider.enabled = true;
    }

    public Vector3 Velocity => _rigidbody.velocity;
    public Vector3 Position => gameObject.transform.position;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Camera _mainCamera;

    private Vector2 _moveDirection;
    private Vector2 _mousePosition;

    public void Reset()
    {
        gameObject.transform.position = Vector3.zero;
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void ProcessInputs()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;

        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
    }

    private void Rotate()
    {
        var lookDirection = _mousePosition - _rigidbody.position;

        // 90 degrees offset to match unit circle
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        _rigidbody.rotation = angle;
    }
}
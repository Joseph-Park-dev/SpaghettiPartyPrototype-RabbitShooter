using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LouisMovement : MonoBehaviour
{
    private Vector2 _moveDir;
    private Vector2 _rotateDir;
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    private float _dashTime;
    [SerializeField] private float _startDashTime;
    private Rigidbody2D _rigid2D;

    private PlayerControls _playerControls;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        _dashTime = _startDashTime;
    }

    private void Update()
    {
        LookTowardMouse();
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(dir);
        Dash(dir, _dashSpeed);
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime * _speed, Space.World);
    }

    private void Dash(Vector2 dir, float dashSpeed)
    {
        _dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashTime = _startDashTime;
        }
        if (0 < _dashTime)
        {
            _rigid2D.velocity = dir * dashSpeed;
        }
        else
        {
            _rigid2D.velocity = Vector2.zero;
        }
    }

    private void LookTowardMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
            );
        transform.rotation = Quaternion.LookRotation(
            Vector3.forward,
            mousePos - transform.position
            );
    }
}

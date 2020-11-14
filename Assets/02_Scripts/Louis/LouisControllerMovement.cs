using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LouisControllerMovement : MonoBehaviour
{
    private Vector2 _moveDir;
    private Vector2 _rotateDir;
    private bool _isDashing = false;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    private float _dashTime;
    [SerializeField] private float _startDashTime;
    private Rigidbody2D _rigid2D;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.GamePlay.Move.performed += ctx => _moveDir = ctx.ReadValue<Vector2>();
        _playerControls.GamePlay.Move.canceled += ctx => _moveDir = Vector2.zero;

        _playerControls.GamePlay.Rotate.performed += ctx => _rotateDir = ctx.ReadValue<Vector2>();
        _playerControls.GamePlay.Rotate.canceled += ctx => _rotateDir = Vector2.zero;

        _playerControls.GamePlay.Dash.performed += ctx => _isDashing = true;
        _playerControls.GamePlay.Dash.canceled += ctx => _isDashing = false;
    }

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        _dashTime = _startDashTime;
    }

    private void FixedUpdate()
    {
        Vector2 m = new Vector2(_moveDir.x, _moveDir.y);
        Vector2 r = new Vector2(_rotateDir.x, _rotateDir.y);
        Move(m);
        Dash(m);
        if(r != Vector2.zero)
            Rotate(r);
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime * _speed, Space.World);
        Rotate(dir);
    }

    private void Dash(Vector2 dir)
    {
        _dashTime -= Time.deltaTime;
        if(_isDashing)
        {
            _dashTime = _startDashTime;
            _isDashing = false;
        }
        if (0 < _dashTime)
        {
            _rigid2D.velocity = dir * _dashSpeed;
        }
        else
        {
            _rigid2D.velocity = Vector2.zero;
        }
    }

    private void Rotate(Vector2 dir)
    {
        Vector2 r = dir* Time.deltaTime * _rotSpeed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, r);
    }

    private void OnEnable()
    {
        _playerControls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _playerControls.GamePlay.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCharacterController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerAbility _ability;
    bool invincibility;
    private void Awake()
    {
        _ability = GetComponent<PlayerAbility>();
        _controller = GetComponent<PlayerCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator = GetComponent<PlayerAbility>()._animator;
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * _ability.Speed * Time.fixedDeltaTime;
        //개빨라 뭐야
        Vector2 moveVector = _rigidbody.position + direction;
        _rigidbody.MovePosition(moveVector);
        invincibility = _ability.invincibility;
        //움직이고 있고 무적이 아닐때
        if (!direction.Equals(Vector2.zero) && !invincibility) _animator.SetFloat("RunState", 0.25f);
        //움직이지 않고 무적이 아닐때
        else if (direction.Equals(Vector2.zero) && !invincibility) _animator.SetFloat("RunState", 0.0f);
        //움직이고 있고 무적일때
        else if (!direction.Equals(Vector2.zero) && invincibility) _animator.SetFloat("RunState", 0.75f);
        //움직이지 않고 무적일때
        else if (direction.Equals(Vector2.zero) && invincibility) _animator.SetFloat("RunState", 0.5f);
    }

    public void SetMove(bool set)
    {
        _movementDirection = Vector2.zero;
        if(set) _controller.OnMoveEvent += Move;
        else _controller.OnMoveEvent -= Move;
    }
    public void MoveEventClear()
    {
        _controller.OnMoveEvent -= Move;
    }
}

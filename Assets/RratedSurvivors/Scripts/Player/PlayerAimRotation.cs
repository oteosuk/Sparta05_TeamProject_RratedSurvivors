using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    float flipX;
    private PlayerCharacterController _controller;

    private void Awake()
    {
        flipX = transform.localScale.x;
        _controller = GetComponent<PlayerCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    void OnAim(Vector2 newAimDirection)
    {
        RotationAim(newAimDirection);
    }
    void RotationAim(Vector2 direction)
    {

        float rotZ = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-flipX, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(flipX, transform.localScale.y, transform.localScale.z);
        }
        //Debug.Log(rotZ);
    }
    public void SetAim(bool set)
    {
        if (set) _controller.OnLookEvent += OnAim;
        else _controller.OnLookEvent -= OnAim;
    }
    public void AimEventClear()
    {
        _controller.OnLookEvent -= OnAim;
    }
}

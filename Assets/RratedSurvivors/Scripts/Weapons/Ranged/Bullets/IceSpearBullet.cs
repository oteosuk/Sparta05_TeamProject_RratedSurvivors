using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class IceSpearBullet : Bullet
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public override void Init(int damage, int pierceCount)
    {
        base.Init(damage, pierceCount);
        _animator.SetBool("IsActive", true);
        Invoke("DestroyBullet", 5f);
    }
    
    public override void InitDirection(Vector3 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
    }

    public override void DestroyBullet()
    {
        if(IsInvoking("DestroyBullet")) CancelInvoke("DestroyBullet");

        base.DestroyBullet();
        _animator.SetBool("IsActive", false);
    }


}

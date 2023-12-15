using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HolyRayBullet : Bullet
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public override void Init(int damage, int pierceCount)
    {
        base.Init(damage, pierceCount);
        _animator.SetBool("IsActive", true);
        Invoke("DestroyBullet", 1f);
    }

    public override void DestroyBullet()
    {
        _animator.SetBool("IsActive", false);
        base.DestroyBullet();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBullet : Bullet
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
        Invoke("DestroyBullet", 0.25f);
    }

    public override void DestroyBullet()
    {
        base.DestroyBullet();
        _animator.SetBool("IsActive", false);
    }
}

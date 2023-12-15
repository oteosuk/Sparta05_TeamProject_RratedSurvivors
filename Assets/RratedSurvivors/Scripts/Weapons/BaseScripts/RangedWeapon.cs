using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    protected float _timer = 0f;
    protected float _fireRate;
    protected float FR;

    protected float FireRate { get { return _fireRate * (100 - 20 *_AS) / 100f; } }

    private void FixedUpdate() 
    {
        _timer += Time.fixedDeltaTime;
        if(_timer > FireRate)
        {
            _timer = 0f;
            GenerateBullet();
        }
    }
}

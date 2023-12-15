using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : RangedWeapon
{
    PlayerCharacterController _controller;
    Vector2 _aimDirection = Vector2.up;

    public override void LevelUp()
    {
        _fireRate -= 0.06f;
        _damage += 2;
    }

    public override void StartInit()
    {
        _pierceCount = 0;
        _damage = 7;
        _fireRate = 0.7f;
        _speed = 4;
        _controller.OnLookEvent += GetDirection;
    }

    protected override void AwakeInit()
    {
        _controller = GetComponentInParent<PlayerCharacterController>();
    }

    public void GetDirection(Vector2 dir)
    {
        _aimDirection = dir;
    }

    protected override void GenerateBullet()
    {
        base.GenerateBullet();
        Vector3 dir = _aimDirection;

        // 생성된 bullet의 위치를 Player에게 맞추기
        _bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        _bullet.InitDirection(dir, Speed);
        Managers.Sound.SoundPlay("SFX/Weapon/Fireball", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/Fireball";
        _weaponData.WeaponName = "파이어볼";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.Fireball;
    }
}

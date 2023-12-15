using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : RangedWeapon
{

    PlayerCharacterController _controller;
    Vector2 _aimDirection;

    public override void LevelUp()
    {
        _fireRate -= 0.1f;
        _damage += 2;
    }

    public override void StartInit()
    {
        _pierceCount = -1;
        _damage = 8;
        _fireRate = 1f;
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
        Vector3 dir = _aimDirection;
        
        base.GenerateBullet();

        // 생성된 bullet의 위치를 Player에게 맞추기
        _bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        // 바라보고 있는 방향에서 살짝 앞으로
        _bullet.transform.Translate(_bullet.transform.up * 1.5f, Space.World);
        Managers.Sound.SoundPlay("SFX/Weapon/Slash", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/Slash";
        _weaponData.WeaponName = "참격";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.Slash;
    }
}

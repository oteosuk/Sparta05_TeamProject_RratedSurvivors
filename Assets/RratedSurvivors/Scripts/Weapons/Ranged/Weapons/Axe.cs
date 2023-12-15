using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.IO.LowLevel.Unsafe;

public class Axe : RangedWeapon
{
    public override void StartInit()
    {
        _pierceCount = 5;
        _damage = 12;
        _fireRate = 3f;
        _speed = 10;
    }

    public override void LevelUp()
    {
        _pierceCount += 1;
        _fireRate -= 0.1f;
        _damage += 3;
    }

    protected override void GenerateBullet()
    {
        base.GenerateBullet();
        // 랜덤한 각도로 회전(-45~45) + 회전한 만큼 x좌표로 이동
        float rotation = Random.Range(-45, 45);
        Vector3 dir = Vector3.up + (Vector3.left * rotation / 360);

        _bullet.transform.DORotate(new Vector3(0,0, rotation), 1);
        _bullet.InitDirection(dir , Speed);
        Managers.Sound.SoundPlay("SFX/Weapon/Axe", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/Axe";
        _weaponData.WeaponName = "도끼";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.Axe;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class IceSpear : RangedWeapon
{
    private Scanner _scanner;

    protected override void AwakeInit()
    {
        _scanner = GetComponentInParent<Scanner>();
    }
    public override void StartInit()
    {
        _pierceCount = 0;
        _damage = 6;
        _fireRate = 0.8f;
        _speed = 5;
    }

    public override void LevelUp()
    {
        _fireRate -= 0.05f;
        _pierceCount += 1;
        _damage += 3;
    }



    protected override void GenerateBullet()
    {
        // Scanner로부터 가장 가까운 타겟의 정보 가져오기
        if(!_scanner.NearestTarget) return;

        Vector3 targetPos = _scanner.NearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        base.GenerateBullet();

        _bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        _bullet.transform.GetComponent<Bullet>().InitDirection(dir, Speed);
        Managers.Sound.SoundPlay("SFX/Weapon/IceSpear", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/IceSpear";
        _weaponData.WeaponName = "아이스 스피어";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.IceSpear;
    }
}

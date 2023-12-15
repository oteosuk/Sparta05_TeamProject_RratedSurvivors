using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyRay : RangedWeapon
{
    private Scanner _scanner;

    public override void StartInit()
    {
        _pierceCount = -1;
        _damage = 10;
        _fireRate = 1.9f;
    }

    protected override void AwakeInit()
    {
        _scanner = GetComponentInParent<Scanner>();
    }

    public override void LevelUp()
    {
        _fireRate -= 0.2f;
        _damage += 3;
    }

    protected override void GenerateBullet()
    {
        // Scanner로부터 랜덤한 타겟의 정보 가져오기
        if(!_scanner.RandomTarget) return;
        
        base.GenerateBullet();
        Vector3 targetPos = _scanner.RandomTarget.position;
        _bullet.transform.position = targetPos - new Vector3(0,-1,0);
        Managers.Sound.SoundPlay("SFX/Weapon/HolyRay", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/HolyRay";
        _weaponData.WeaponName = "홀리 레이";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.HolyRay;
    }
}

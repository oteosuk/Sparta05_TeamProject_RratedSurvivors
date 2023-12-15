using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Dagger : Weapon
{
    private float _count;
    private Transform _transform;
    
    public override void StartInit()
    {
        _pierceCount = -1;
        _damage = 10;
        _count = 1;
        _speed = 150;
        WeaponInstantiate();
    }

    public override void LevelUp()
    {
        _damage += 2;
        _count += 1;
        WeaponInstantiate();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back * Speed * Time.fixedDeltaTime);
    }
    
    protected void WeaponInstantiate()
    {
        for(int i=0; i < _count ; i++)
        {
            if (i < transform.childCount)
            {
                _bullet = transform.GetChild(i).GetComponent<Bullet>();
                _bullet.Init(Damage, _pierceCount);
            }
            else
            {
                GenerateBullet();
                _bullet.transform.parent = transform;
            }

            Vector3 rotVec = Vector3.forward * 360 * i / _count;
            _bullet.transform.localPosition = Vector3.zero;
            _bullet.transform.localRotation = Quaternion.identity;
            _bullet.transform.Rotate(rotVec);
            _bullet.transform.Translate(_bullet.transform.up * 1.0f, Space.World); 
        }
        Managers.Sound.SoundPlay("SFX/Weapon/Dagger", SoundType.Effect);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/Dagger";
        _weaponData.WeaponName = "단검";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.Dagger;
    }

}

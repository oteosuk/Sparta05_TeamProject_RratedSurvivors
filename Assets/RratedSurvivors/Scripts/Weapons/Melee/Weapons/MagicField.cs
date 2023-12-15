using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MagicField : Weapon
{
    private float _timer = 0;
    private float _inactiveTime = 3;
    private float _activeTime = 4;
    private bool _isActive;

    public override void StartInit()
    {
        _pierceCount = -1;
        _damage = 6;
        MakeOrInhance();
    }

    public override void LevelUp()
    {
        _inactiveTime -= 0.2f;
        _activeTime += 0.2f;
        _damage += 1;
        MakeOrInhance();
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if(_isActive && _timer > _activeTime)
        {
            _timer = 0;
            _isActive = false;
            _bullet.gameObject.SetActive(false);
        }
        else if(!_isActive && _timer > _inactiveTime)
        {
            _timer = 0;
            _isActive = true;
            _bullet.gameObject.SetActive(true);
            Managers.Sound.SoundPlay("SFX/Weapon/MagicField", SoundType.Effect);
        }
    }

    private void MakeOrInhance()
    {
        // 특성상 GenerateBullet 안씀
        Transform bullet;
        if(_bullet != null)
        {
            bullet = _bullet.transform;
        }
        else
        {
            _bullet = _Pool.Get();
            bullet = _bullet.transform;
            bullet.parent = transform;
        }

        bullet.localPosition = Vector3.zero;
        bullet.localRotation = Quaternion.identity;
        bullet.GetComponent<Bullet>().Init(Damage, _pierceCount);
    }

    protected override void InitWeaponData()
    {
        _weaponData.ResLoc = "Weapon/MagicField";
        _weaponData.WeaponName = "매직필드";
        _weaponData.Level = 0;
        _weaponData.WeaponType = WeaponType.MagicField;
    }
}

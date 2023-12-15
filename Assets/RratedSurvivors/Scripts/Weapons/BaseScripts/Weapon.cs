using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Weapon : MonoBehaviour
{
    protected int _damage;
    protected int _speed;
    protected int _pierceCount;
    protected int _AS = 0;
    protected int _AD = 0;
    protected int _scale = 0;
    protected Bullet _bullet;
    protected WeaponData _weaponData;

    protected int Damage { get { return _damage * (100 + _AD*10 ) / 100; } }
    protected float Scale { get { return (100+_scale*10) /100f; } }
    protected float Speed { get { return _speed * (100 + _AS * 10) / 100f; } }
    public WeaponData WeaponData { get { return _weaponData ;} }
    public int Level { get { return _weaponData.Level; } set { _weaponData.Level = value; } }

    [SerializeField] protected GameObject _bulletPrefab;
    protected IObjectPool<Bullet> _Pool;

    private void Awake() 
    {
        _Pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize:10);
        AwakeInit();
        InitWeaponData();
    }

    public abstract void StartInit();
    protected virtual void AwakeInit() {}
    protected abstract void InitWeaponData();
    public abstract void LevelUp();

    protected virtual void GenerateBullet()
    {
        _bullet = _Pool.Get();
        _bullet.transform.position = transform.position;
        _bullet.transform.rotation = Quaternion.identity;
        _bullet.transform.localScale = new Vector3(Scale, Scale, 1);
        _bullet.Init(Damage, _pierceCount);
    }

    protected Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab).GetComponent<Bullet>();
        bullet.SetManagedPool(_Pool);
        return bullet;
    }

    protected void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    protected void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    protected void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void GetAD(int PlayerAD) {_AD = PlayerAD;}
    public void GetAS(int PlayerAS) 
    {
        if(PlayerAS > 4) { _AS = 4; }
        else{ _AS = PlayerAS; }
    }
    public void GetScale(int PlayerScale) {_scale = PlayerScale;}

}

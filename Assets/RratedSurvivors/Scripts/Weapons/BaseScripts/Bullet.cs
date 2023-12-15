using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    protected int _damage;
    protected int _pierceCount; // -1 is Infinite Piercing
    
    private IObjectPool<Bullet> _ManagedPool;

    public int Damage { get {return _damage ;} }


    public virtual void Init(int damage, int pierceCount)
    {
        _damage = damage;
        _pierceCount = pierceCount;
    }

    public virtual void InitDirection(Vector3 direction, float speed) {}

    public void SetManagedPool(IObjectPool<Bullet> pool)
    {
        _ManagedPool = pool;
    }

    public virtual void DestroyBullet()
    {
        _ManagedPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Enemy" || _pierceCount == -1) return;

        _pierceCount--;

        if(_pierceCount == -1)
        {
            DestroyBullet();
        }
    }
}

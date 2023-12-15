using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AxeBullet : Bullet
{
    private Rigidbody2D _rigidbody;
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void InitDirection(Vector3 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
        Invoke("DestroyBullet", 5f);
    }

    public override void DestroyBullet()
    {
        if(IsInvoking("DestroyBullet")) CancelInvoke("DestroyBullet");
        base.DestroyBullet();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMagicSkill : MonoBehaviour
{
    PlayerAbility playerability;
    private float LifeTime = 8.0f;
    private int damage = 10;
    Transform target;
    Rigidbody2D rb;
    private float shotSpeed = 1f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, LifeTime);
        Invoke("shootToPlayer", 1f);
        playerability = Managers.GameManager.player.GetComponent<PlayerAbility>();
        
    }
    private void OnEnable()
    {
        //LifeTime = 8.0f;
        //Invoke("DestroyProjectile", LifeTime);
    }

    private void shootToPlayer()
    {
        rb.velocity = new Vector2(0, 0);
        rb.velocity = (target.position - transform.position) * shotSpeed;
    }

    private void DestroyProjectile()
    {
        Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerability.CharacterHit(damage);
            Managers.Resource.Destroy(gameObject);
        }
    }
}

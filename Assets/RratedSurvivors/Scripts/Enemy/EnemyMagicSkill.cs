using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicSkill : MonoBehaviour
{
    PlayerAbility playerability;
    private float LifeTime;
    private int damage = 5;

    private void Start()
    {
        playerability = Managers.GameManager.player.GetComponent<PlayerAbility>();
    }
    private void OnEnable()
    {
        LifeTime = 10.0f;
        Invoke("DestroyProjectile", LifeTime);
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

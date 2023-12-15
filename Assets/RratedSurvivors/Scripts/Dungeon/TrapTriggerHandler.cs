using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggerHandler : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.CompareTag("Player"))
        {
            PlayerAbility playerAbility = collision.GetComponent<PlayerAbility>();
            if (playerAbility.invincibility) return;
            int damage = Mathf.FloorToInt(playerAbility.MaxHP * 0.2f);
            playerAbility.CharacterHit(damage);
        }
    }
}

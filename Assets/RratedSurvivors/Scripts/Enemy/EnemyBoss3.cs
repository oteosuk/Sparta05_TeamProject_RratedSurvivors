using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss3 : Enemy
{
    private bool isAttacking = false;
    protected override void Start()
    {
        base.Start();
        enemySpeed = 0.5f;
        enemyAttackCoolTime = 0.5f;
        enemyDefaultHp = 200f;
        enemyHp = 200f;
        enemyPower = 60;
        enemyExp = 800;
    }

    protected override void Update()
    {
        base.Update();
        FollowTarget();
    }

    private void Attack(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player") && !isAttacking) // 공격중아니고 Player면 공격해
        {
            AttackDamage(enemyPower);
            animator.SetTrigger("AttackTrigger"); // 공격 애니메이션 호출 메서드
            isAttacking = true; // 나 공격중이야
            Invoke("ResetAttack", enemyAttackCoolTime); // 쿨타임 시간 뒤에 공격상태 false로 돼
        }
    }
    private void ResetAttack() { isAttacking = false; }

    // Player와 충돌한순간
    private void OnCollisionEnter2D(Collision2D collision) { Attack(collision); }

    // Player와 충돌중일때
    private void OnCollisionStay2D(Collision2D collision) { Attack(collision); }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBoss4 : Enemy
{
    public GameObject SkillPrefab;  // 투사체 프리팹
    private float shotInterval = 1.5f; // 투사체 발사 간격
    private float shotSpeed = 5f;    // 투사체 속도
    private bool isAttacking = false;
    protected override void Start()
    {
        base.Start();
        enemySpeed = 0.8f;
        enemyAttackCoolTime = 0.5f;
        enemyDefaultHp = 500f;
        enemyHp = 500f;
        enemyPower = 80;
        enemyExp = 1000;
        StartCoroutine(ShootPrefabsToPlayer());
    }

    public override void EnablePoolAction()
    {
        //StartCoroutine(ShootPrefabsToPlayer());
    }
    IEnumerator ShootPrefabsToPlayer()
    {
        while (true)
        {
            // 상하좌우
            ShootPrefab(Vector2.up);
            ShootPrefab(Vector2.down);
            ShootPrefab(Vector2.left);
            ShootPrefab(Vector2.right);

            // 대각선
            ShootPrefab(new Vector2(-1, 1).normalized);  // 왼쪽위 대각선
            ShootPrefab(new Vector2(1, 1).normalized);   // 오른쪽위 대각선
            ShootPrefab(new Vector2(-1, -1).normalized); // 왼쪽아래 대각선
            ShootPrefab(new Vector2(1, -1).normalized);  // 오른쪽아래 대각선

            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void ShootPrefab(Vector2 direction)
    {
        
        GameObject blackball = Instantiate(SkillPrefab, transform.position, Quaternion.identity);
        //GameObject blackball = Managers.Resource.Instantiate("BossMagicSkill", null);
        blackball.transform.position = transform.position;
        Rigidbody2D rb = blackball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shotSpeed;
        //Invoke("ShootToPlayer(blackball)", 1f);
    }

    /* private void ShootToPlayer(GameObject blackball)
    {
        Rigidbody2D rb = blackball.GetComponent<Rigidbody2D>();
        rb.velocity = (target.position - blackball.transform.position).normalized * shotSpeed;
    }*/

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

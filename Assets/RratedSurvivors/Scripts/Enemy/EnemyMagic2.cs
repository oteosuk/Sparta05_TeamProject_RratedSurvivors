using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic2 : Enemy
{
    public GameObject SkillPrefab;  // 투사체 프리팹
    private float shotInterval = 1.5f; // 투사체 발사 간격
    private float shotSpeed = 2f;    // 투사체 속도

    private bool isAttacking = false;
    protected override void Start()
    {
        base.Start();
        enemySpeed = 0.8f;
        enemyAttackCoolTime = 1f;
        enemyDefaultHp = 40f;
        enemyHp = 40f;
        enemyPower = 30;
        enemyExp = 30;

        StartCoroutine(ShootPrefabsToPlayer());
    }

    public override void EnablePoolAction()
    {
        StartCoroutine(ShootPrefabsToPlayer());
    }
    IEnumerator ShootPrefabsToPlayer()
    {
        while (true)
        {
            ShootToPlayer();

            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void ShootToPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject redball = Managers.Resource.Instantiate("EnemyMagicSkill", null);
        redball.transform.position = transform.position;
        Rigidbody2D rb = redball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shotSpeed;
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

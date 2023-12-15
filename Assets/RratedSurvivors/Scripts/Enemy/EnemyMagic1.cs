using System.Collections;
using UnityEngine;

public class EnemyMagic1 : Enemy
{
    public GameObject SkillPrefab;  // 투사체 프리팹
    private float shotInterval = 5f; // 투사체 발사 간격
    private float shotSpeed = 2f;    // 투사체 속도

    private bool isAttacking = false;
    protected override void Start()
    {
        base.Start();
        enemySpeed = 0.5f;
        enemyAttackCoolTime = 1f;
        enemyDefaultHp = 15f;
        enemyHp = 15f;
        enemyPower = 20;
        enemyExp = 20;

        StartCoroutine(ShootPrefabs());
    }

    public override void EnablePoolAction()
    {
        StartCoroutine(ShootPrefabs());
    }

    IEnumerator ShootPrefabs()
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
        // 투사체를 발사하는 코드
        //GameObject projectile = Instantiate(SkillPrefab, transform.position, Quaternion.identity);
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

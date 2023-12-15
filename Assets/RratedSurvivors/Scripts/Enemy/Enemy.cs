using System.Linq;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float enemyAttackCoolTime;
    [SerializeField] protected float enemyDefaultHp;
    [SerializeField] protected float enemyHp;
    [SerializeField] protected int enemyPower;
    [SerializeField] protected int enemyExp;
    protected float pushDistance = 1.5f;
    protected float pushSpeed = 15f;
    private bool isAttacked = false;
    private SpriteRenderer[] sprites;
    private Color[] defaultcolors;
    private Vector3 HitDamageTextPos = Vector3.zero;

    PlayerAbility playerability;

    public float EnemyHp // 다른 클래스에서도 이 몬스터 Hp를 안전하게 수정할 수 있게 프로퍼티 사용
    {
        get { return enemyHp; }
        set { if (value >= 0) { enemyHp = value; } }
    }
    protected Transform target;
    protected Animator animator;
    Vector3 originalScale;

    protected virtual void Start()
    {
        playerability = Managers.GameManager.player.GetComponent<PlayerAbility>();
        DefaultSetting();
    }

    private void DefaultSetting()
    {
        animator = transform.GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        originalScale = transform.localScale;
        sprites = GetComponentsInChildren<SpriteRenderer>();
        defaultcolors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++) { defaultcolors[i] = sprites[i].color; } // 기존 색깔들을 전부 저장해놓는 코드(나중에 원래색깔로 되돌리기위해)
        //defaultcolors = sprites.Select(sprite => sprite.color).ToArray(); // 윗 줄 코드를 람다식으로 연습해봤습니다. 
    }


    protected virtual void Update()
    {
        // 이 부분에 Enemy 공통적으로 Update해야 될게 있으면 구현
    }

    protected void FollowTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            
            Vector2 direction = (target.position - transform.position);
            
            FlipCharacter(direction.x, originalScale); // 가는 방향에 따라 이미지 좌우 반전하는 메서드
        }
    }

    private void FlipCharacter(float direction, Vector3 scale)
    {
        if (direction < 0)
        {
            transform.localScale = scale; // 원래 스케일
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z); // x 스케일을 -1를 곱하여 좌우 반전
        }
    }
    // 스킬이나 공격과 충돌한순간
    private void OnTriggerEnter2D(Collider2D other) { Attacked(other); }
    // 스킬이나 공격과 충돌중일떄
    private void OnTriggerStay2D(Collider2D other){ Attacked(other); }

    protected void AttackDamage(int damage)
    {  
        playerability.CharacterHit(damage); // 플레이어가 데미지를 받는 함수
    }

    private void Attacked(Collider2D other)
    {
        if (CheckTag(other) && !isAttacked)
        {
            if (other.name == "MageSpaceSkill") // MageSpaceSkill의 테두리부분만 데미지를 줄수있도록 거리계산후 안쪽에 있으면 데미지를 주지 않게끔
            {
                float scale = other.transform.localScale.x;
                Vector2 origin = other.transform.position;
                if (Vector2.Distance(origin, transform.position) < 8.12 * scale) return;
            }

            ProcessAttack(other.tag);

            // 몬스터가 데미지가 깍이는 부분
            if (other.tag == "PlayerSkill")
            {
                int skilldamage = other.GetComponent<SpaceSkill>().SkillDamage;
                HitDamageText(skilldamage);
                enemyHp -= skilldamage;
            }

            if (other.tag == "PlayerAttack")
            { 
                int bulletDamage = other.GetComponent<Bullet>().Damage;
                HitDamageText(bulletDamage);
                enemyHp -= bulletDamage;
            }

            if (enemyHp <= 0)
            {
                playerability.ExpGet(enemyExp); // 플레이어가 경험치를 얻는 부분

                enemyHp = enemyDefaultHp;

                Managers.Resource.Destroy(gameObject);
                Managers.GameManager.KillNumberOfCurrentStage++;
                Managers.GameManager.NumberOfTotalMonsterKill++;
            }
        }
    }

    private void HitDamageText(int damage)
    {
        GameObject hitUI = Managers.Resource.Instantiate("MonsterHitText", null);
        MonsterDamageText scrip = hitUI.GetComponent<MonsterDamageText>();
        scrip.SetDamage(damage);
        HitDamageTextPos.Set(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        hitUI.transform.position = HitDamageTextPos;
        scrip.TextAnimation();
    }

    private bool CheckTag(Collider2D other)
    {
        return (other.CompareTag("PlayerAttack") || other.CompareTag("PlayerSkill"));
    }

    private void ProcessAttack(string tag)
    {
        ChangeBlack(); // 색깔 검은색으로 변경
        attackedGoBack(); // 뒤로 밀려남
        isAttacked = true; // 공격받은 직후에는 바로 공격 받지않게
        Invoke("ResetColor", 0.08f); // 색깔 원상태로
        Invoke("ResetAttacked", 0.25f); // 다시 공격 받을 수 있는 상태로

    }

    private void attackedGoBack()
    {
        if (target != null)
        {
            // 현재 몬스터의 위치와 플레이어의 위치 차이를 계산
            Vector2 pushDirection = (transform.position - target.position).normalized;

            // 일정 거리만큼 뒤로 밀릴 위치 계산
            Vector2 pushedPosition = (Vector2)transform.position + pushDirection * pushDistance;

            // 몬스터를 새로 계산된 위치로 이동
            transform.position = Vector2.MoveTowards(transform.position, pushedPosition, pushSpeed * Time.deltaTime);
        }
    }
    private void ChangeBlack()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.black;
        }
    }
    private void ResetColor() 
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = defaultcolors[i];
        }
    }
    private void ResetAttacked() => isAttacked = false;

    public virtual void EnablePoolAction()
    {
        //오브젝트풀에서 활성화될때 해야하는 행동 정의
    }
}

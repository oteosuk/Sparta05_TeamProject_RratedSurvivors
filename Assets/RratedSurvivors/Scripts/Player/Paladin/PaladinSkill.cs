using UnityEngine;

public class PaladinSkill : SpaceSkill
{
    private float _skillDuration = 3f; //팔라딘 방패 지속시간

    private void Awake()
    {
        SkillDamage = 10;
        Destroy(gameObject, _skillDuration);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySkill")) // 몬스터 투사체 부수기
        {
            Managers.Resource.Destroy(other.gameObject);
        }
    }
}

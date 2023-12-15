using DG.Tweening;
using UnityEngine;

public class MageSpaceSkill : SpaceSkill
{
    private Transform _spriteRenderTrans;
    private Vector3 _maxScope = new Vector3(2.0f, 2.0f);
    private float _skillDuration = 1.5f; // 메이지스킬 지속시간

    private void Awake()
    {
        SkillDamage = 30;
    }

    public override void UseSkill(Vector3 dir)
    {
        transform.position = dir;
        _spriteRenderTrans = GetComponent<SpriteRenderer>().transform;
        _spriteRenderTrans.DOScale(_maxScope, _skillDuration).OnComplete(() =>
        {
            Managers.Resource.Destroy(gameObject);
        });
        Managers.Sound.SoundPlay("SFX/mageskill", SoundType.Effect);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySkill")) // 몬스터 투사체 부수기
        {
            Managers.Resource.Destroy(other.gameObject);
        }
    }
}

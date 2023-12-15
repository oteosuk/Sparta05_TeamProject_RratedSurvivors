using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;

public enum ESkillName
{
    MageSpaceSkill,
    PaladinSkill,
    Max
}
public enum ESkillPosition
{
    Player,
    NullPosition
}
public class PlayerSkillAction : MonoBehaviour
{
    private PlayerCharacterController _controller;
    [SerializeField] private ESkillName spaceSkill = ESkillName.Max;
    [SerializeField] private ESkillPosition spaceSkillPosition = ESkillPosition.NullPosition;
    internal bool _isCoolDown = false;        //스킬이 쿨타임 상태인지  
    private float _coolDownTimer = 0f;       //스킬 쿨타임 타이머

    private void Awake()
    {
        _controller = GetComponent<PlayerCharacterController>();
    }

    void Start()
    {
        if (_controller != null)
        {
            _controller.OnSpaceSkillEvent += OnSpaceSkill;
        }
    }

    private void OnSpaceSkill()
    {
        if (spaceSkill != ESkillName.Max && !_isCoolDown)
        {
            StartCoroutine(SpaceCoolDown());
            GameObject skill = Managers.Resource.Instantiate(spaceSkill.ToString(), spaceSkillPosition == (ESkillPosition)1 ? null : gameObject.transform);
            if(skill.GetComponent<SpaceSkill>() != null) skill.GetComponent<SpaceSkill>().UseSkill(GetComponent<Transform>().position);
            GameObject.Find("GameUI").GetComponent<GameUI>().SkillCool();
        }

        else
        {
            Debug.Log($"cooldown{_coolDownTimer}");
        }
    }

    private IEnumerator SpaceCoolDown()
    {
        _isCoolDown = true;
        //각 캐릭터별 쿨타임 테스트 완료 후 마지막은 이거 쓰면 될거 같네요
        _coolDownTimer = gameObject.GetComponent<PlayerAbility>().SkillWaiting;
        while (_coolDownTimer >= 0f)
        {
            _coolDownTimer -= Time.deltaTime;
            yield return null;
        }
        _coolDownTimer = 0f;
        _isCoolDown = false;
    }
    public void SetSkill(bool set)
    {
        if (set) _controller.OnSpaceSkillEvent += OnSpaceSkill;
        else _controller.OnSpaceSkillEvent -= OnSpaceSkill;
    }

    public void SkillEventClear()
    {
        _controller.OnSpaceSkillEvent -= OnSpaceSkill;
    }
}

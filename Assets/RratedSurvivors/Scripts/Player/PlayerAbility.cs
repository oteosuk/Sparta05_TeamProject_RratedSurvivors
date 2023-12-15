using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour,ICharacter,IPlayer
{
    private int _ad;
    private int _as;
    private int _scale;

    public int HP {get; private set;}
    public int MaxHP {get; private set;}
    public int AD {get {return _ad;} set {_ad = value; OnADEvent?.Invoke(value);} }
    public int AS {get {return _as;} set {_as = value; OnASEvent?.Invoke(value);} }
    public int Scale {get {return _scale;} set {_scale = value; OnScaleEvent?.Invoke(value);} }
    public float Speed {get; private set; }
    public int DF {get; private set;}
    public int Exp {get; private set;}
    public bool IsDead => HP <= 0;
    public int NextExp {get; private set;}
    public int SkillWaiting {get; private set;}
    public int Level { get; private set;}

    public event Action<int> OnADEvent;
    public event Action<int> OnASEvent;
    public event Action<int> OnScaleEvent;

    PlayerMovement playerMovement;
    PlayerAimRotation playerAimRotation;
    PlayerSkillAction playerSkillAction;
    public Animator _animator;
    //무적 판별 bool
    public bool invincibility = false;
    //무적 지속시간
    float invincibilityTimeSet = 1.0f;

    //체력 마나바 업데이트를 위한 이벤트
    public event Action<int, int> UpdateHPBar;
    public event Action<int, int> UpdateEXPBar;

    //레벨업시 사용할 이벤트
    public event Action<int> LevelUpUI;
    public event Action<int> LevelUp;
    public event Action IsDeadUI;

    // 초기화
    void Awake()
    {
        PlayerSetting();
        playerMovement = GetComponent<PlayerMovement>();
        playerAimRotation = GetComponent<PlayerAimRotation>();
        playerSkillAction = GetComponent<PlayerSkillAction>();
        _animator = GetComponent<Animator>();
    }
    // 캐릭터 생성시 초기화 메서드
    void PlayerSetting()
    {
        JobSettings jobSettings = JsonSetting.Instance.AbilitySettings(gameObject.name);
        if (jobSettings != null)
        {
            HP = jobSettings.HP;
            MaxHP = jobSettings.MaxHP;
            AD = jobSettings.AD;
            Speed = jobSettings.Speed;
            DF = jobSettings.DF;
            Exp = jobSettings.Exp;
            AS = jobSettings.AS;
            NextExp = jobSettings.NextExp;
            SkillWaiting = jobSettings.SkillWaiting;
            Level = jobSettings.Level;
        }
    }

    //스텟 업 메서드
    public void AbilityUp(int select)
    {
        AbilityUpJson(select);
        UpdateHPBar?.Invoke(HP, MaxHP);
    }
    // 레벨업 메서드
    public void LevelUP()
    {
        Level++;
        Exp = 0;
        AddExp();
        LevelUp?.Invoke(Level);
        Managers.Sound.SoundPlay("SFX/levelup", SoundType.Effect);
    }
    // 제이슨으로 스텟 증가량 가져와서 스텟 증가 메서드
    void AbilityUpJson(int select)
    {
        JobAbility jobAbility = JsonSetting.Instance.AbilityUP(gameObject.name);
        if (jobAbility != null)
        {
            
            switch (select)
            {
                case 1:
                    MaxHP += jobAbility.MaxHP;
                    break;
                case 2:
                    AD += jobAbility.AD;
                    break;
                case 3:
                    Speed += jobAbility.Speed;
                    break;
                case 4:
                    AS += jobAbility.AS;
                    break;
            }
            RecoveryHP(jobAbility.MaxHP);
            DF += jobAbility.DF;
        }
    }

    // 레벨업시 경험치 증가 메서드
    void AddExp()
    {
        JobAbility jobAbility = JsonSetting.Instance.AbilityUP(gameObject.name);
        if (jobAbility != null) NextExp += jobAbility.NextExp * Level;
    }
    // 히트시 메서드
    public void CharacterHit(int damage)
    {
        if(!invincibility)
        {
            HP -= damage;
            if(HP < 0) HP = 0;
            UpdateHPBar?.Invoke(HP, MaxHP);
            invincibilityTime();
        }
        if (IsDead) 
        {
            PlayerSet(false);
            _animator.SetTrigger("Die");
            IsDeadUI?.Invoke();
        }
    }
    // 히트시 무적 메서드
    void invincibilityTime()
    {
        //한대 맞고 무적시간 끝났을때
        if (invincibility)
        {
            invincibility = false;
        }
        //한대 맞았을때
        else
        {
            invincibility = true;
            Invoke("invincibilityTime", invincibilityTimeSet);
        }
    }


    //체력 회복 메서드
    public void RecoveryHP(int recovery)
    {
        HP += recovery;
        if(HP > MaxHP)  HP = MaxHP;
        UpdateHPBar?.Invoke(HP, MaxHP);
    }


    // 경험치 획득 메서드
    public void ExpGet(int exp)
    {
        Exp += exp;
        if(Exp >= NextExp)
        {
            LevelUP();
            LevelUpUI?.Invoke(3);
        }
        UpdateEXPBar?.Invoke(Exp, NextExp);
    }
    
    //캐릭터 움직임 멈추는 메서드
    public void PlayerSet(bool set)
    {
        playerMovement.SetMove(set);
        playerAimRotation.SetAim(set);
        playerSkillAction.SetSkill(set);
    }

    //리소스 해제 메서드
    public void PlayerSetClear()
    {
        playerMovement.MoveEventClear();
        playerAimRotation.AimEventClear();
        playerSkillAction.SkillEventClear();
        OnADEvent = null;
        OnASEvent = null;
        OnScaleEvent = null;
        UpdateHPBar = null;
        UpdateEXPBar = null;
        LevelUpUI = null;
        LevelUp = null;
        IsDeadUI = null;
    }
}

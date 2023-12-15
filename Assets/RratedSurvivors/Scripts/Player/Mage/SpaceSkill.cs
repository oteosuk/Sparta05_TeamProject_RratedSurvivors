using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceSkill : MonoBehaviour
{
    public int SkillDamage { get; protected set; } // 스킬 공격력
    public virtual void UseSkill(Vector3 dir) { }  // 스킬 구현
}

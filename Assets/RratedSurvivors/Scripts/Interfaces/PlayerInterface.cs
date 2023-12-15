using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{

}

public interface ICharacter
{
    //체력
    int HP { get;}
    //최대체력
    int MaxHP { get;}
    //공격력
    int AD { get; set; }
    //스피드
    float Speed { get;}
    //방어력 일단은 더미데이터 나중에 틱당 데미지를 입을 때 사용
    int DF {  get;}
    //현재 경험치
    int Exp {  get;}
    //죽었는지 안죽었는지 확인
    bool IsDead { get; }
    //캐릭터가 피해를 입었을 시 호출되는 메서드
    void CharacterHit(int damage);

}
public interface IPlayer
{
    //레벨
    int Level {  get; }
    //공격 속도
    int AS { get; set; }
    //레벨업에 필요한 경험치
    int NextExp { get;}
    //스킬 쿨타임
    int SkillWaiting {  get;}
    //플레이어가 레벨업할 때 능력치 업 선택시 호출되는 메서드 
    void AbilityUp(int select);
}

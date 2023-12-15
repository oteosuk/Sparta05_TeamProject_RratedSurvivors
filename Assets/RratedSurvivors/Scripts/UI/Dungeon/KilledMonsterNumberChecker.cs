using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class KilledMonsterNumberChecker : MonoBehaviour
{
    private ChangeStageController controller;
    private PlayerAbility playerAbility;
    private int numberOfMonsterToKill;

    private void Awake()
    {
        controller = GetComponent<ChangeStageController>();
        
    }

    private void Start()
    {
        playerAbility = Managers.GameManager.player.GetComponent<PlayerAbility>();
    }



    //해당 스테이지 시간안에 목표 몬스터 수를 다 죽였는지 판단하는 함수


}

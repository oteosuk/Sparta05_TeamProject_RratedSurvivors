using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI stageRestTimeText;
    
    private ChangeStageEventCaller caller;

    private float stageManageTime = 10;

    private void Awake()
    {
        caller = GetComponent<ChangeStageEventCaller>();
    }

    private void Start()
    {
        Managers.Sound.SoundPlay("Bgm/skulls_adventure", SoundType.BGM);
        Managers.Sound.SetVolume(SoundType.BGM, 1f);
    }

    void Update()
    {
        UpdateTimeAndStage();
        UpdateUIText();
    }

    private void UpdateTimeAndStage()
    {
        Managers.GameManager.TotalGamePlayTime += Time.deltaTime;
        stageManageTime -= Time.deltaTime;

        if (stageManageTime <= 0)
        {
            UpdateStageLevel();
        }
    }
    private void UpdateUIText()
    {
        stageText.text = "Stage " + Managers.GameManager.CurrentStage.ToString();
        stageRestTimeText.text = stageManageTime.ToString("N2");
    }

    private void UpdateStageLevel()
    {
        stageManageTime = 10;
        caller.CurrentStage++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStageEventCaller : ChangeStageController
{
    private int _currentStage = 1;
    public int CurrentStage
    {
        get { return _currentStage; }
        set
        {
            CallChangeStageEvent(value);
            _currentStage = value;
            Managers.GameManager.CurrentStage++;
        }
    }
}

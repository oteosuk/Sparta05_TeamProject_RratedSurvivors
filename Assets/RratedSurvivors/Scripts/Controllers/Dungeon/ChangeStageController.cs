using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStageController : MonoBehaviour
{
    public event Action<int> ChangeStageEvent;

    public void CallChangeStageEvent(int stage)
    {
        ChangeStageEvent.Invoke(stage);
    }
}

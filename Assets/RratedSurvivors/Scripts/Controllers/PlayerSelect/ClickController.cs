using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public event Action<string> ClickEvent;

    public void CallClickEvent(string name)
    {
            ClickEvent?.Invoke(name);
    }
}

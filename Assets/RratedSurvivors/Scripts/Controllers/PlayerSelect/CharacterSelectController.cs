using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : ClickController
{
    public Button button1;
    public Button button2;

    private void Start()
    {
        AddClickEvent(button1);
        AddClickEvent(button2);
    }

    private void AddClickEvent(Button button)
    {
        button.onClick.AddListener(() =>
        {
            CallClickEvent(button.name);
        });
    }

}

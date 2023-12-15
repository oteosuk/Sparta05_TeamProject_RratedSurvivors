using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfoProvider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    private ClickController controller;
    private JsonSetting jsonSetting;

    public GameObject[] playerObjects; 


    private void Awake()
    {
        controller = GetComponent<ClickController>();
        jsonSetting = GetComponent<JsonSetting>();
    }

    private void Start()
    {
        if (controller != null)
        {
            controller.ClickEvent += ChangeCharacter;
        }
    }

    private void OnEnable()
    {
        ChangeCharacter(playerObjects[0].name);
    }



    public void ChangeCharacter(string characterName = "Mage")
    {
        Managers.GameManager.SelectedPlayerName = characterName;
        

        SetCharacterInfoUIText(characterName);
        SetActiveToSelectedCharacter(characterName);
    }

    private void SetCharacterInfoUIText(string characterName)
    {
        JobSettings jobInfo = jsonSetting.AbilitySettings(characterName);

        hpText.text = "HP : " + jobInfo.MaxHP;
        speedText.text = "Speed : " + jobInfo.Speed;
        atkText.text = "ATK : " + jobInfo.AD;
        defText.text = "DEF : " + jobInfo.DF;
    }

    private void SetActiveToSelectedCharacter(string characterName)
    {
        foreach (GameObject item in playerObjects)
        {
            GameObject gameObject = item.gameObject;
            if (gameObject.name == characterName)
            {
                    gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);
        }
    }
}

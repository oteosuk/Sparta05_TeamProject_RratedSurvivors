using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    private WeaponManager _weaponManager;
    private GameObject _slotIcon;

    private void Awake()
    {
        _slotIcon = Resources.Load<GameObject>("UI/WeaponSlotIcon");

    }

    private void Start()
    {
        Invoke("Init", 0.1f);
    }

    private void MakeSlotIcon(WeaponData weaponData)
    {
        GameObject Icon = Instantiate(_slotIcon);
        Icon.transform.SetParent(transform);
        Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponData.ResLoc);
    }

    private void Init()
    {
        _weaponManager = _gameUI.GetWeaponManager();
        _weaponManager.OnInitWeaponEvent += MakeSlotIcon;
    }

}

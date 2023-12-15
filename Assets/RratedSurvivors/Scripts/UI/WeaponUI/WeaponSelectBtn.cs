using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectBtn : MonoBehaviour
{
    private WeaponManager _weaponManager;

    [SerializeField] TMP_Text _weaponName;
    [SerializeField] TMP_Text _weaponLevel;
    [SerializeField] Image _weaponIcon;
    private GameUI _gameUI;

    WeaponType _weaponType;

    private void OnDisable()
    {
        Destroy(gameObject);
    }
    public void GetWeaponManager(WeaponManager weaponManager)
    {
        _weaponManager = weaponManager;
    }

    public void GetGameUI(GameUI gameUI) { _gameUI = gameUI ;}

    public void SetWeaponSelectBtn(WeaponData weaponData)
    {
        _weaponType = weaponData.WeaponType;
        _weaponName.text = weaponData.WeaponName;
        _weaponLevel.text = weaponData.Level.ToString();
        _weaponIcon.sprite = Resources.Load<Sprite>(weaponData.ResLoc);
    }

    public void WeaponSelectBtnClickEvent()
    {
        _weaponManager.UpdateWeapon(_weaponType);
        _gameUI.ModalSet(-1);
    }
}

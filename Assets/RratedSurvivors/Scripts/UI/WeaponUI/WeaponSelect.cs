using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    private GameObject _weaponSelectBtn;
    private WeaponManager _weaponManager;
    
    private void Awake() 
    {
        _weaponSelectBtn = Resources.Load<GameObject>("UI/WeaponSelectBtn");
        _weaponManager = _gameUI.GetWeaponManager();
    }

    private void OnEnable()
    {
        List<WeaponData> weaponDats = _weaponManager.GetRandomWeapon();

        foreach(WeaponData dat in weaponDats)
        {
            GameObject newBtn = Instantiate(_weaponSelectBtn);
            newBtn.transform.SetParent(transform);
            newBtn.GetComponent<WeaponSelectBtn>().GetWeaponManager(_weaponManager);
            newBtn.GetComponent<WeaponSelectBtn>().GetGameUI(_gameUI);
            newBtn.GetComponent<WeaponSelectBtn>().SetWeaponSelectBtn(dat);

        }
    }
}

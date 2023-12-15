using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
{
    // 시작은 Dagger, 끝은 None 고정 !!
    Dagger,
    MagicField,
    Axe,
    HolyRay,
    IceSpear,
    Slash,
    Fireball,
    None
}
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Dictionary<WeaponType, Weapon> Weapons;
    private Weapon _currentWeapon;
    PlayerAbility _playerAbility;
    public event Action<WeaponData> OnInitWeaponEvent;
    private const int MaxWeaponRange = 5;
    private int _currentWeaponStock = 0;

    public string Name {get; set;}

    private void Awake() 
    {
        Weapons = new Dictionary<WeaponType, Weapon>();
        _playerAbility = GetComponentInParent<PlayerAbility>();
    }

    private void Start()
    {
        InitWeapon();
        _playerAbility.OnADEvent += ReflectAD;
        _playerAbility.OnASEvent += ReflectAS;
        _playerAbility.OnScaleEvent += ReflectScale;
        Invoke("GetFirstWeapon", 0.1f);
    }

    public void ReflectAD(int AD) { foreach(Weapon weapon in Weapons.Values) {weapon.GetAD(AD);} }
    public void ReflectAS(int AS) { foreach(Weapon weapon in Weapons.Values) {weapon.GetAS(AS);} }
    public void ReflectScale(int scale) { foreach(Weapon weapon in Weapons.Values) {weapon.GetScale(scale);} }

    public void GetFirstWeapon()
    {
        if(Name == "Paladin") ActivateWeapon(WeaponType.Slash) ;
        else if ( Name == "Mage") ActivateWeapon(WeaponType.Fireball) ;
    }

    public void InitWeapon()
    {
        WeaponType temp = WeaponType.None;
        foreach(Weapon weapon in GetComponentsInChildren<Weapon>())
        {
            temp = weapon.WeaponData.WeaponType;
            weapon.enabled = false;
            Weapons.Add(temp, weapon);
        }
    }

    public void LevelUpWeapon(WeaponType weaponType)
    {
        _currentWeapon = Weapons[weaponType];
        _currentWeapon.Level++;

        _currentWeapon.LevelUp();
    }

    public void ActivateWeapon(WeaponType weaponType)
    {
        _currentWeapon = Weapons[weaponType];
        _currentWeapon.Level++;

        _currentWeapon.enabled = true;
        _currentWeapon.StartInit();
        OnInitWeaponEvent?.Invoke(_currentWeapon.WeaponData);
        _currentWeaponStock++;
    }

    public void UpdateWeapon(WeaponType weaponType)
    {
        if(Weapons[weaponType].enabled == true) LevelUpWeapon(weaponType);
        else ActivateWeapon(weaponType);
    }

    public List<WeaponData> GetRandomWeapon()
    {
        List<WeaponData> datList = new List<WeaponData>();

        if(_currentWeaponStock == MaxWeaponRange)
        {
            // 리스트 생성해서 5개 넣고 -> 그중에서 랜덤으로 3개 뽑아주기
            foreach(Weapon weapon in Weapons.Values)
            {
                if(weapon.enabled == true)
                {
                    datList.Add(weapon.WeaponData);
                }
            }

            for(int i=0; i<2; i++)
            {
                int rand = Random.Range(0,5-i);
                datList.RemoveAt(rand);
            }
        }
        else
        {
            // 완전 랜덤으로 보내주기
            int count = 0;
            int[] checkArr = {-1,-1,-1};
            while(count != 3)
            {
                int rand = Random.Range((int) WeaponType.Dagger, (int) WeaponType.None);

                if(checkArr[0] == rand || checkArr[1] == rand) continue;

                checkArr[count++] = rand;
                datList.Add(Weapons[(WeaponType)rand].WeaponData);
            }
        }

        return datList;
    }

}

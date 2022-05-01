using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponData : MonoBehaviour
{
    public Transform _weaponPosRight;
    public Transform _weaponPosLeft;
    public GameObject _currentWeaponRight;
    public GameObject _currentWeaponLeft;
    [HideInInspector]
    public UserData _userData;

    private void Awake()
    {
        _userData = SaveSystem.Instance.UserData;
    }
    public void EquipWeapon(Item weapon)
    {
        if (weapon._main == Item.Main.Main)
        {
            Destroy(_currentWeaponRight);
            _currentWeaponRight = Instantiate(weapon._itemPrefab, _weaponPosRight);
            _currentWeaponRight.transform.localPosition = weapon._equipPosition_Right;
            _currentWeaponRight.transform.localRotation = Quaternion.Euler(weapon._equipRotation_Right);
            _userData._currentCharacter._currentWeaponRight = weapon;
        }
        else if (weapon._main == Item.Main.Sub)
        {
            Destroy(_currentWeaponLeft);
            _currentWeaponLeft = Instantiate(weapon._itemPrefab, _weaponPosLeft);
            _currentWeaponLeft.transform.localPosition = weapon._equipPosition_Left;
            _currentWeaponLeft.transform.localRotation = Quaternion.Euler(weapon._equipRotation_Left);
            _userData._currentCharacter._currentWeaponLeft = weapon;
        }
        else if (weapon._main == Item.Main.Both)
        {
            Destroy(_currentWeaponRight);
            Destroy(_currentWeaponLeft);
            _currentWeaponLeft = Instantiate(weapon._itemPrefab, _weaponPosLeft);
            _currentWeaponLeft.transform.localPosition = weapon._equipPosition_Left;
            _currentWeaponLeft.transform.localRotation = Quaternion.Euler(weapon._equipRotation_Left);
            _currentWeaponRight = Instantiate(weapon._itemPrefab, _weaponPosRight);
            _currentWeaponRight.transform.localPosition = weapon._equipPosition_Right;
            _currentWeaponRight.transform.localRotation = Quaternion.Euler(weapon._equipRotation_Right);
            _userData._currentCharacter._currentWeaponRight = weapon;
            _userData._currentCharacter._currentWeaponLeft = weapon;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class WeaponGrid : MonoBehaviour
{
    private const string _StartSceneUIManagerTag = "StartSceneUIManager";

    [SerializeField]
    Image _background;
    [SerializeField]
    RawImage _weaponRawImage;
    [SerializeField]
    Button _button;

    WeaponDetailView _weaponDetailView;
    WeaponListView _weaponListView;
    CharacterSceneUIManager _characterSceneUIManager;
    [SerializeField]
    int _gridNumber;

    public Image Background { get => _background; }
    public RawImage WeaponRawImage { get => _weaponRawImage; }


    public void Initialize(int gridNum)
    {
        _characterSceneUIManager = GameObject.FindGameObjectWithTag(_StartSceneUIManagerTag).GetComponent<StartSceneUIManager>()._characterSceneUIManager;
        _weaponDetailView = _characterSceneUIManager._weaponDetailView;
        _weaponListView = _characterSceneUIManager._weaponListView;
        _button.onClick.AddListener(OnClickEquipWeapon);
        _gridNumber = gridNum;
    }

    public void OnPointerEnter()
    {
        _weaponDetailView.gameObject.SetActive(true);
        _weaponDetailView._rawImage.texture = _weaponRawImage.texture;
    }

    public void OnPoiterExit()
    {
        _weaponDetailView.gameObject.SetActive(false);
    }

    private void OnClickEquipWeapon()
    {
        int weaponNumber = _gridNumber + _weaponListView._pushCount;
        CharacterData characterData = _characterSceneUIManager._currentCharacterData;
        CharacterWeaponData characterWeaponData = _characterSceneUIManager._currentCharacterObj.GetComponent<CharacterWeaponData>();
        Item weapon = characterData._weaponList[weaponNumber];
        characterWeaponData.EquipWeapon(weapon);
    }


}

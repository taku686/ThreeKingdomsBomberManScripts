using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponListView : MonoBehaviour
{
    List<WeaponGrid> _weaponGridsList = new List<WeaponGrid>();
    [SerializeField]
    List<RenderTexture> _renderTextureList = new List<RenderTexture>();
    [SerializeField]
    private Button _leftArrow;
    [SerializeField]
    private Button _rightArrow;
    [SerializeField]
    private Transform _weaponListTransform;
    [SerializeField]
    WeaponGrid _gridPrefab;

    [HideInInspector]
    public List<GameObject> _currentWeaponList = new List<GameObject>();
    [HideInInspector]
    public int _pushCount = 0;

    public void Initialize()
    {
        _leftArrow.onClick.AddListener(OnClickLeftArrow);
        _rightArrow.onClick.AddListener(OnClickRightArrow);
    }
    public void ChangeWeaponGrid(CharacterData characterData)
    {
        RemoveWeaponList(_currentWeaponList);
        RemoveGridList(_weaponGridsList);
        List<Item> weaponList = characterData._weaponList;
        if (weaponList.Count < 3)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                GenerateGrid(i, characterData);
            }
        }
        else if (weaponList.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                GenerateGrid(i, characterData);
            }
        }
        for (int i = 0; i < characterData._weaponList.Count; i++)
        {
            GameObject weapon = Instantiate(
                  weaponList[i]._itemPrefab,
                  new Vector3(
                      1000 + (100 * i) + weaponList[i]._viewPosition.x,
                  1000 + weaponList[i]._viewPosition.y,
                  1000 + weaponList[i]._viewPosition.z),
                  Quaternion.Euler(weaponList[i]._viewRotation));
            _currentWeaponList.Add(weapon);
        }
    }

    private void RemoveWeaponList(List<GameObject> weaponList)
    {
        foreach (var weapon in weaponList)
        {
            DestroyImmediate(weapon);
        }
        weaponList.RemoveRange(0, weaponList.Count);
    }

    private void RemoveGridList(List<WeaponGrid> gridList)
    {
        foreach (var grid in gridList)
        {
            DestroyImmediate(grid.gameObject);
        }
        gridList.RemoveRange(0, gridList.Count);
    }

    private void OnClickLeftArrow()
    {
        if (_pushCount <= 0)
        {
            return;
        }
        _pushCount--;
        for (int i = 0; i < _currentWeaponList.Count; i++)
        {
            _currentWeaponList[i].transform.position = new Vector3(_currentWeaponList[i].transform.position.x + 100,
                                                                    _currentWeaponList[i].transform.position.y,
                                                                    _currentWeaponList[i].transform.position.z);
        }
    }
    private void OnClickRightArrow()
    {
        if (_pushCount >= _currentWeaponList.Count - 3)
        {
            return;
        }
        _pushCount++;
        for (int i = 0; i < _currentWeaponList.Count; i++)
        {
            _currentWeaponList[i].transform.position = new Vector3(_currentWeaponList[i].transform.position.x - 100,
                                                                    _currentWeaponList[i].transform.position.y,
                                                                    _currentWeaponList[i].transform.position.z);
        }
    }

    private void GenerateGrid(int num, CharacterData characterData)
    {
        GameObject weaponGrid = Instantiate(_gridPrefab.gameObject, _weaponListTransform);
        WeaponGrid weaponGridSc = weaponGrid.GetComponent<WeaponGrid>();
        weaponGridSc.Initialize(num);
        RawImage rawImage = weaponGrid.GetComponentInChildren<RawImage>();
        rawImage.texture = _renderTextureList[num];
        _weaponGridsList.Add(weaponGridSc);
        weaponGridSc.Background.color = characterData._charaColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSceneUIManager : MonoBehaviour
{
    public List<CharacterData> _charcterLists = new List<CharacterData>();
    public Button _leftArrow;
    public Button _rightArrow;
    public Button _backButton;
    public Transform _generatePos;
    public StatusView _statusView;
    public Text _nameText;
    public WeaponDetailView _weaponDetailView;
    public WeaponListView _weaponListView;
    [HideInInspector]
    public CharacterData _currentCharacterData;
    [HideInInspector]
    public GameObject _currentCharacterObj;
}

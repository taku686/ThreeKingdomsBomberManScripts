using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public GameObject _charaObj;
    public string _name;
    public int _number;
    public int _speed;
    public int _bombCount;
    public int _attack;
    public int _firePower;
    public int _hp;
    public SkillData _skill1;
    public SkillData _skill2;
    public List<Item> _weaponList;
    public Item _currentWeaponRight;
    public Item _currentWeaponLeft;
    public Item _bomb;
    public Color _charaColor;
}

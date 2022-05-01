using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items/item")]
public class Item : ScriptableObject
{
    public GameObject _itemPrefab;
    public Type _type;
    public Attribute _attribute;
    public Main _main;
    public Vector3 _viewRotation;
    public Vector3 _viewPosition;
    public Vector3 _equipRotation_Left;
    public Vector3 _equipPosition_Left;
    public Vector3 _equipRotation_Right;
    public Vector3 _equipPosition_Right;
    public enum Type
    {
        Shield,
        LargeSword,
        Sword,
        Spear,
        Axe,
        Hammer,
        Knife,
        Rod,
        Whip,
        Nail,
        Bow,
        Club,
        Fan,
        Halberd,
        Cannon,
        Bomb
    }

    public enum Attribute
    {
        Fire,
        Thunder,
        Ice,
        Poison,
        Heal,
        Nothing,
    }

    public enum Main
    {
        Main,
        Sub,
        Both
    }


}
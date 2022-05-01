using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public Sprite _icon;
    public SkillType _skillType;

    public string _explanation;

    public enum SkillType
    {
        Skill1,
        Skill2,
    }
}

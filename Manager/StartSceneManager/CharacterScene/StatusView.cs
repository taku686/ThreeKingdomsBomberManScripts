using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusView : MonoBehaviour
{
    [SerializeField]
    Slider _speedSlider;
    [SerializeField]
    Slider _attackSlider;
    [SerializeField]
    Slider _bombSlider;
    [SerializeField]
    Slider _hpSlider;
    [SerializeField]
    Text _speedText;
    [SerializeField]
    Text _attackText;
    [SerializeField]
    Text _bombText;
    [SerializeField]
    Text _hpText;
    [SerializeField]
    Image _skill1Image;
    [SerializeField]
    Image _skill2Image;
    [SerializeField]
    Text _skill1Explanation;
    [SerializeField]
    Text _skill2Explanation;

    public void ChangeStatusView(CharacterData characterData)
    {
        _speedSlider.value = characterData._speed;
        _speedText.text = $"{characterData._speed}/8";
        _attackSlider.value = characterData._attack;
        _attackText.text = $"{characterData._attack}/8";
        _bombSlider.value = characterData._bombCount;
        _bombText.text = $"{characterData._bombCount}/8";
        _hpSlider.value = characterData._hp;
        _hpText.text = $"{characterData._hp}/8";
    }

    public void ChangeSkillView(CharacterData characterData)
    {
        _skill1Image.sprite = characterData._skill1._icon;
        _skill1Image.color = characterData._charaColor;
        _skill1Explanation.text = characterData._skill1._explanation;
        _skill2Image.sprite = characterData._skill2._icon;
        _skill2Image.color = characterData._charaColor;
        _skill2Explanation.text = characterData._skill2._explanation;
    }

}

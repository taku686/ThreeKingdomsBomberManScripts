using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneUIManager : MonoBehaviour
{
    [SerializeField]
    Image _skill1Button;
    [SerializeField]
    Image _skill2Button;

    public void Initialize(Sprite skill1, Sprite skill2)
    {
        _skill1Button.sprite = skill1;
        _skill2Button.sprite = skill2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSelectView : MonoBehaviour
{
    [SerializeField]
    Button _battleRoyaleButton;
    [SerializeField]
    Button _pointMatchButton;
    [SerializeField]
    Button _crownBattleButton;
    [HideInInspector]
    public SelectFriendMatchView _selectFriendMatchView;
    private void OnEnable()
    {
        _battleRoyaleButton.onClick.AddListener(OnClickBattleRoyale);
        _pointMatchButton.onClick.AddListener(OnClickPointMatch);
        _crownBattleButton.onClick.AddListener(OnClickCrownBattle);
    }

    private void OnDisable()
    {
        _battleRoyaleButton.onClick.RemoveAllListeners();
        _pointMatchButton.onClick.RemoveAllListeners();
        _crownBattleButton.onClick.RemoveAllListeners();
    }

    private void OnClickBattleRoyale()
    {
        GeneralManager._instance._gameSetting._battleMode = GameSetting.BattleMode.BattleRoyale;
        SceneChange();
    }

    private void OnClickPointMatch()
    {
        GeneralManager._instance._gameSetting._battleMode = GameSetting.BattleMode.PointMatch;
        SceneChange();
    }

    private void OnClickCrownBattle()
    {
        GeneralManager._instance._gameSetting._battleMode = GameSetting.BattleMode.CrownBattle;
        SceneChange();
    }

    private void SceneChange()
    {
        this.gameObject.SetActive(false);
        _selectFriendMatchView.gameObject.SetActive(true);
    }

}

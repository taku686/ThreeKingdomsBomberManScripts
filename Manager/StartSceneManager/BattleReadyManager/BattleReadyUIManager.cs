using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReadyUIManager : MonoBehaviour
{
    public BattleSelectView _battleSelectView;
    public Button _cancelButton;
    public SelectFriendMatchView _selectFriendMatchView;
    public RuleSettingView _ruleSettingView;
    public RobbyRoomView _robbyRoomView;
    public SelectCreateRoomView _selectCreateRoomView;
    public EnterFriendRoomView _enterFriendRoomView;
    public StageSelectView _stageSelectView;

    private void Start()
    {
        _battleSelectView._selectFriendMatchView = _selectFriendMatchView;
        _selectFriendMatchView._selectCreateRoomView = _selectCreateRoomView;
        _selectFriendMatchView._robbyRoomView = _robbyRoomView;
        _ruleSettingView._robbyRoomView = _robbyRoomView;
        _selectCreateRoomView._enterFriendRoomView = _enterFriendRoomView;
        _selectCreateRoomView._stageSelectView = _stageSelectView;
        _enterFriendRoomView._robbyRoomView = _robbyRoomView;
        _stageSelectView._ruleSettingView = _ruleSettingView;
    }
}

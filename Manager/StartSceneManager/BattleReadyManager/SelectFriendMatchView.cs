using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFriendMatchView : MonoBehaviour
{
    [SerializeField]
    Button _randomMatchButton;
    [SerializeField]
    Button _friendMatchButton;
    [HideInInspector]
    public RobbyRoomView _robbyRoomView;
    [HideInInspector]
    public SelectCreateRoomView _selectCreateRoomView;


    private void OnEnable()
    {
        _randomMatchButton.onClick.AddListener(OnClickRandomMatch);
        _friendMatchButton.onClick.AddListener(OnClickFriendMatch);
    }

    private void OnDisable()
    {
        _randomMatchButton.onClick.RemoveAllListeners();
        _friendMatchButton.onClick.RemoveAllListeners();
    }

    private void OnClickRandomMatch()
    {
        _robbyRoomView.gameObject.SetActive(true);
        GeneralManager._instance._gameSetting._isFriendMatch = false;
        ChangeScene();
    }

    private void OnClickFriendMatch()
    {
        ChangeScene();
        GeneralManager._instance._gameSetting._isFriendMatch = true;
        _selectCreateRoomView.gameObject.SetActive(true);
    }

    private void ChangeScene()
    {
        this.gameObject.SetActive(false);
    }
}

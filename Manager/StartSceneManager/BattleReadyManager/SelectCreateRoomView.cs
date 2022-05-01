using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCreateRoomView : MonoBehaviour
{
    [SerializeField]
    Button _createRoomButton;
    [SerializeField]
    Button _enterRoomButton;
    [HideInInspector]
    public EnterFriendRoomView _enterFriendRoomView;
    [HideInInspector]
    public StageSelectView _stageSelectView;
    [HideInInspector]
    public bool _isCreate;
    private void OnEnable()
    {
        _createRoomButton.onClick.AddListener(OnClickCreateRoom);
        _enterRoomButton.onClick.AddListener(OnClickEnterRoom);
    }

    private void OnDisable()
    {
        _createRoomButton.onClick.RemoveAllListeners();
        _enterRoomButton.onClick.RemoveAllListeners();
    }

    private void OnClickCreateRoom()
    {
        _stageSelectView.gameObject.SetActive(true);
        _isCreate = true;
        this.gameObject.SetActive(false);
    }

    private void OnClickEnterRoom()
    {
        _enterFriendRoomView.gameObject.SetActive(true);
        _isCreate = false;
        this.gameObject.SetActive(false);
    }

}

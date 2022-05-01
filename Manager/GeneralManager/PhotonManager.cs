using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private const int _BattleScene = 1;
    private const int _TimeLimit = 3;

    [SerializeField]
    BattleReadyUIManager _battleReadyUIManager;



    private float _timer = 0;
    [HideInInspector]
    public bool _isCount;
    private SelectCreateRoomView _selectCreateRoomView;
    private EnterFriendRoomView _enterFriendRoomView;
    private RuleSettingView _ruleSettingView;
    private RobbyRoomView _robbyRoomView;
    private int _battleStageMaxNum;
    private void Start()
    {
        _selectCreateRoomView = _battleReadyUIManager._selectCreateRoomView;
        _enterFriendRoomView = _battleReadyUIManager._enterFriendRoomView;
        _ruleSettingView = _battleReadyUIManager._ruleSettingView;
        _robbyRoomView = _battleReadyUIManager._robbyRoomView;
        _battleStageMaxNum = _battleReadyUIManager._stageSelectView._stageImage.Count - 2;
    }

    private void Update()
    {
        if (_isCount)
        {
            // Debug.Log(_timer);
            _timer += Time.unscaledDeltaTime;
            if (!GeneralManager._instance._gameSetting._isFriendMatch)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers || _timer > _TimeLimit)
                {
                    _isCount = false;
                    _timer = 0;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    GeneralManager._instance._isGameStart = true;
                    SceneManager.LoadScene(_BattleScene);
                }
            }
            else
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    _isCount = false;
                    _timer = 0;
                    GeneralManager._instance._isGameStart = true;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    SceneManager.LoadScene(_BattleScene);
                }
            }
            _robbyRoomView.ChangeImage();
        }
    }

    public void ConnectPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (!GeneralManager._instance._gameSetting._isFriendMatch)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        else if (GeneralManager._instance._gameSetting._isFriendMatch && _selectCreateRoomView._isCreate)
        {
            PhotonNetwork.JoinOrCreateRoom(SetRoomName(), SetRoomProperties(), null);
        }
        /*
        else if (GeneralManager._instance._gameSetting._isFriendMatch && !_selectCreateRoomView._isCreate)
        {
            //    PhotonNetwork.JoinOrCreateRoom(SetRoomName(), null, null);
        }
        */
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (GeneralManager._instance._gameSetting._isFriendMatch && !_selectCreateRoomView._isCreate && !_isCount)
        {
            foreach (var room in roomList.Where((rl) => rl.Name == SetRoomName()))
            {
                PhotonNetwork.JoinRoom(SetRoomName());
            }
        }
    }

    public override void OnJoinedRoom()
    {
        _isCount = true;
        SetStageNum();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _timer = 0;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _timer = 0;
    }

    private RoomOptions SetRoomProperties()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)GeneralManager._instance._gameSetting._paritcipantsPlayer;
        return roomOptions;
    }

    private string SetRoomName()
    {
        if (_selectCreateRoomView._isCreate)
        {
            Debug.Log(_ruleSettingView._inputField.textComponent.text);
            return _ruleSettingView._inputField.textComponent.text;
        }
        else
        {
            return _enterFriendRoomView._inputField.textComponent.text;
        }
    }

    private void SetStageNum()
    {
        if (PhotonNetwork.IsMasterClient && GeneralManager._instance._gameSetting._isFriendMatch)
        {
            PhotonNetwork.CurrentRoom.SetStageNum(GeneralManager._instance._gameSetting._battleStage);
        }
        else if (!PhotonNetwork.IsMasterClient && GeneralManager._instance._gameSetting._isFriendMatch)
        {
            GeneralManager._instance._gameSetting._battleStage = PhotonNetwork.CurrentRoom.GetStageNum();
        }
        else if (PhotonNetwork.IsMasterClient && !GeneralManager._instance._gameSetting._isFriendMatch)
        {
            GeneralManager._instance._gameSetting._battleStage = Random.Range(0, _battleStageMaxNum);
            PhotonNetwork.CurrentRoom.SetStageNum(GeneralManager._instance._gameSetting._battleStage);
        }
        else if (!PhotonNetwork.IsMasterClient && !GeneralManager._instance._gameSetting._isFriendMatch)
        {
            GeneralManager._instance._gameSetting._battleStage = PhotonNetwork.CurrentRoom.GetStageNum();
        }
    }
}

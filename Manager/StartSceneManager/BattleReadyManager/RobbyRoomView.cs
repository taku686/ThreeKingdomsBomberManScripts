using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class RobbyRoomView : MonoBehaviourPunCallbacks
{
    bool _isReady;
    [SerializeField]
    GameObject _errorMessage;
    [SerializeField]
    Button _errorMessageButton;
    [SerializeField]
    PhotonManager _photonManager;
    [HideInInspector]
    public BattleSelectView _battleSelectView;
    [SerializeField]
    List<Image> _imageList = new List<Image>();
    [SerializeField]
    List<Text> _textList = new List<Text>();

    private int _playerCount = 1;
    private float _timer = 0;
    public override void OnEnable()
    {
        _errorMessageButton.onClick.AddListener(OnClickErrorMessage);
        _imageList[0].color = Color.white;
        _isReady = true;
    }

    public override void OnDisable()
    {
        _errorMessageButton.onClick.RemoveAllListeners();
        _playerCount = 1;
        foreach (var image in _imageList)
        {
            image.color = Color.black;
        }
    }

    private void Update()
    {
        if (_isReady)
        {
            Debug.Log("ネットワーク接続");
            _isReady = false;
            _photonManager.ConnectPhotonServer();
        }
        TextChangeColor();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
        _errorMessage.SetActive(true);
        this.gameObject.SetActive(false);
        _battleSelectView.gameObject.SetActive(true);
        PhotonNetwork.Disconnect();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
        _errorMessage.SetActive(true);
        this.gameObject.SetActive(false);
        _battleSelectView.gameObject.SetActive(true);
        PhotonNetwork.Disconnect();
    }

    private void OnClickErrorMessage()
    {
        _errorMessage.SetActive(false);
    }

    public void ChangeImage()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > _playerCount)
        {
            _playerCount++;
            _imageList[_playerCount - 1].color = Color.white;
        }
    }

    private void TextChangeColor()
    {

        _timer += Time.unscaledDeltaTime;
        for (int i = 0; i < _textList.Count; i++)
        {
            _textList[i].color = Color.HSVToRGB(_timer * 0.3f % 1, 1, 1);
        }
    }
}

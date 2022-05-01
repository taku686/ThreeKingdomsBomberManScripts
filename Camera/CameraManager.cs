using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class CameraManager : MonoBehaviourPunCallbacks
{
    private const string _playerTag = "Player";
    [SerializeField]
    CinemachineTargetGroup _targetGroup;
    private List<PlayerBase> _playerList;

    [SerializeField]
    CinemachineVirtualCamera _virtualCamera;
    [HideInInspector]
    public PlayerBase _player;



    // Start is called before the first frame update
    public override void OnEnable()
    {
        _virtualCamera.Follow = _player.transform;
        // Initialize();
    }

    private void Initialize()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag(_playerTag))
        {
            _targetGroup.AddMember(player.transform, 1, 1);
        }
    }









}

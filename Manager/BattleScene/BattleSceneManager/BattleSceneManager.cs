using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
public partial class BattleSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    CameraManager _cameraManager;
    [SerializeField]
    BombManager _bombManager;
    [SerializeField]
    BattleSceneUIManager _battleSceneUIManager;


    public List<Transform> _generatePos = new List<Transform>();
    public PlayerManager _playerManager;

    private UserData _userData;
    private StateMachine<BattleSceneManager> _stateMachine;
    private List<PhotonView> _currentPlayerList = new List<PhotonView>();
    private enum Event : int
    {
        BeforeBattle,
        InBattle,
        AfterBattle,
        InResult
    }

    private void Awake()
    {
        _userData = SaveSystem.Instance.UserData;
        _stateMachine = new StateMachine<BattleSceneManager>(this);
        _stateMachine.AddTransition<BeforeBattle, InBattle>((int)Event.InBattle);
        _stateMachine.AddTransition<InBattle, AfterBattle>((int)Event.AfterBattle);
        _stateMachine.AddTransition<AfterBattle, InResult>((int)Event.InResult);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GeneralManager._instance._isGameStart)
        {
            Initialized();
        }
        _stateMachine.Update();
    }

    private void Initialized()
    {
        GeneralManager._instance._isGameStart = false;
        _stateMachine.Start<BeforeBattle>();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StartSceneManager : MonoBehaviour
{
    private enum Event : int
    {
        TitleScene,
        CharacterScene,
        ConfigeScene,
        BattleReadyScene
    }

    private StateMachine<StartSceneManager> _stateMachine;
    [SerializeField]
    private StartSceneUIManager _startSceneUIManager;
    [SerializeField]
    private GameObject _startSceneObj;
    [SerializeField]
    private GameObject _characterSceneObj;
    [SerializeField]
    private GameObject _battleReadySceneObj;
    [SerializeField]
    private Transform _playerPos;
    [SerializeField]
    private PhotonManager _photonManager;

    private UserData _userData;

    private void Start()
    {
        _stateMachine = new StateMachine<StartSceneManager>(this);
        _stateMachine.AddTransition<TitleSceneManager, CharacterSceneManager>((int)Event.CharacterScene);
        _stateMachine.AddTransition<TitleSceneManager, BattleReadyManager>((int)Event.BattleReadyScene);
        _stateMachine.AddAnyTransition<TitleSceneManager>((int)Event.TitleScene);
        _stateMachine.Start<TitleSceneManager>();
    }

}

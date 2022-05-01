using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleSceneManager>.State;
using Photon.Pun;
using System.Linq;

public partial class BattleSceneManager
{
    public class BeforeBattle : State
    {
        private PlayerManager _playerManager;
        private const int _playerCount = 1;
        protected override void OnEnter(State prevState)
        {
            Initialize();
        }

        private void Initialize()
        {
            PhotonNetwork.CurrentRoom.SetPlayerCount(_playerCount);
            _playerManager = Owner._playerManager;
            _playerManager.CreatePlayer();
            Owner._cameraManager._player = _playerManager._player;
            Owner._bombManager._localPlayerPhotonView = _playerManager._player.gameObject.GetComponent<PhotonView>();
            Owner._cameraManager.enabled = true;
            Owner._battleSceneUIManager.Initialize(Owner._userData._currentCharacter._skill1._icon, Owner._userData._currentCharacter._skill2._icon);
        }

        protected override void OnUpdate()
        {
            if (Owner._currentPlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                Owner._stateMachine.Dispatch((int)Event.InBattle);
            }
        }
    }
    private int _currentPlayerCount = 0;
    private const string _playerCountKey = "pc";
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        foreach (var prop in propertiesThatChanged.Where((pc) => pc.Key as string == _playerCountKey))
        {
            _currentPlayerCount += PhotonNetwork.CurrentRoom.GetPlayerCount();
        }
    }
}

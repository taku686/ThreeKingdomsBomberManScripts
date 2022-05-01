using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleSceneManager>.State;
using Photon.Pun;
using System.Linq;

public partial class BattleSceneManager
{
    public class InBattle : State
    {
        private const string _playerTag = "Player";
        protected override void OnEnter(State prevState)
        {
            Initialize();
        }

        private void Initialize()
        {
            ChangePlayerListOrder();
            SetStartPosPlayer();
            CreateBombPool();
        }

        private void SetStartPosPlayer()
        {
            int playerNumber = 0;
            Owner._currentPlayerList.RemoveRange(0, Owner._currentPlayerList.Count);
            foreach (var player in GameObject.FindGameObjectsWithTag(_playerTag).Select((pl) => pl.GetComponent<PhotonView>()))
            {
                Owner._currentPlayerList.Add(player);
                player.transform.position = Owner._generatePos[playerNumber].position;
                playerNumber++;
            }
        }

        private void CreateBombPool()
        {
            Owner._bombManager._currentPlayerList = Owner._currentPlayerList;
            Owner._bombManager.Initialize();
        }

        private void ChangePlayerListOrder()
        {
            foreach (var player in Owner._currentPlayerList.OrderBy(pl => pl.ViewID))
            {
                Owner._currentPlayerList.Remove(player);
                Owner._currentPlayerList.Add(player);
            }
        }
    }
}

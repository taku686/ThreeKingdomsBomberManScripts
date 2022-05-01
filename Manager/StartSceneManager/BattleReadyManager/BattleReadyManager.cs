using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<StartSceneManager>.State;
using Photon.Pun;
using UnityEngine.SceneManagement;

public partial class StartSceneManager
{
    public class BattleReadyManager : State
    {
        private BattleReadyUIManager _battleReadyUIManager;

        protected override void OnEnter(State prevState)
        {
            Owner._battleReadySceneObj.SetActive(true);
            Owner._photonManager.gameObject.SetActive(true);
            _battleReadyUIManager = Owner._startSceneUIManager._battleReadyUIManager;
            _battleReadyUIManager._battleSelectView.gameObject.SetActive(true);
            _battleReadyUIManager._cancelButton.onClick.AddListener(OnClickCancelButton);
        }

        protected override void OnExit(State nextState)
        {
            Owner._battleReadySceneObj.SetActive(false);
            Owner._photonManager.gameObject.SetActive(false);
            _battleReadyUIManager._battleSelectView.gameObject.SetActive(false);
            _battleReadyUIManager._selectFriendMatchView.gameObject.SetActive(false);
            _battleReadyUIManager._ruleSettingView.gameObject.SetActive(false);
            _battleReadyUIManager._robbyRoomView.gameObject.SetActive(false);
            _battleReadyUIManager._cancelButton.onClick.RemoveAllListeners();
        }

        private void OnClickCancelButton()
        {
            Owner._photonManager._isCount = false;
            PhotonNetwork.Disconnect();
            Owner._stateMachine.Dispatch((int)Event.TitleScene);
        }
    }
}

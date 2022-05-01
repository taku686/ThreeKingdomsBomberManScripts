using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<StartSceneManager>.State;

public partial class StartSceneManager
{
    public class TitleSceneManager : State
    {
        private TitleSceneUIManager _titleSceneUIManager;
        private GameObject _character;
        private CharacterWeaponData _characterWeaponData;

        protected override void OnEnter(State prevState)
        {
            Initialize();
            CharacterGenerate();
            _titleSceneUIManager = Owner._startSceneUIManager._titleSceneUIManager;
            _titleSceneUIManager._battleButton.onClick.AddListener(OnClickBattleButton);
            _titleSceneUIManager._equipButton.onClick.AddListener(OnClickEquipButton);
            _titleSceneUIManager._configButton.onClick.AddListener(OnClickConfigButton);
        }

        protected override void OnExit(State nextState)
        {
            DestroyImmediate(_character);
            _titleSceneUIManager._battleButton.onClick.RemoveAllListeners();
            _titleSceneUIManager._equipButton.onClick.RemoveAllListeners();
            _titleSceneUIManager._configButton.onClick.RemoveAllListeners();
            Owner._startSceneObj.SetActive(false);
        }

        private void OnClickEquipButton()
        {
            Owner._stateMachine.Dispatch((int)Event.CharacterScene);
        }

        private void OnClickBattleButton()
        {
            Owner._stateMachine.Dispatch((int)Event.BattleReadyScene);
        }

        private void OnClickConfigButton()
        {
            Owner._stateMachine.Dispatch((int)Event.ConfigeScene);
        }

        private void Initialize()
        {
            Owner._startSceneObj.SetActive(true);
            Owner._userData = SaveSystem.Instance.UserData;
        }

        private void CharacterGenerate()
        {
            _character = Instantiate(Owner._userData._currentCharacter._charaObj, Owner._playerPos);
            _characterWeaponData = _character.GetComponent<CharacterWeaponData>();
            if (Owner._userData._currentCharacter._currentWeaponRight != null)
            {
                _characterWeaponData.EquipWeapon(Owner._userData._currentCharacter._currentWeaponRight);
            }
            if (Owner._userData._currentCharacter._currentWeaponLeft != null)
            {
                _characterWeaponData.EquipWeapon(Owner._userData._currentCharacter._currentWeaponLeft);
            }
        }
    }
}


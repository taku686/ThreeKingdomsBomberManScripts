using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using State = StateMachine<StartSceneManager>.State;

public partial class StartSceneManager
{
    public class CharacterSceneManager : State
    {

        private CharacterSceneUIManager _characterSceneUIManager;
        private List<GameObject> _currentCharacterList = new List<GameObject>();
        private int _currentCharacterNum;


        protected override void OnEnter(State prevState)
        {
            Owner._characterSceneObj.SetActive(true);
            _characterSceneUIManager = Owner._startSceneUIManager._characterSceneUIManager;
            _characterSceneUIManager._rightArrow.onClick.AddListener(OnClickRightArrow);
            _characterSceneUIManager._leftArrow.onClick.AddListener(OnClickLeftArrow);
            _characterSceneUIManager._backButton.onClick.AddListener(OnClickBackButton);
            _characterSceneUIManager._weaponListView.Initialize();
            for (int i = 0; i < _characterSceneUIManager._charcterLists.Count; i++)
            {
                GameObject character = Instantiate(_characterSceneUIManager._charcterLists[i]._charaObj, _characterSceneUIManager._generatePos);
                _currentCharacterList.Add(character);
                character.AddComponent<CharacterAnimation>();
                character.SetActive(false);
            }
            foreach (var chara in _characterSceneUIManager._charcterLists.Where((list) => Owner._userData._currentCharacter._number == list._number))
            {
                ChangeCharacter(_characterSceneUIManager._charcterLists.IndexOf(chara));
                ChangeWeapon(_characterSceneUIManager._charcterLists.IndexOf(chara));
                _currentCharacterNum = _characterSceneUIManager._charcterLists.IndexOf(chara);
            }
        }

        protected override void OnExit(State nextState)
        {
            _characterSceneUIManager._rightArrow.onClick.RemoveAllListeners();
            _characterSceneUIManager._leftArrow.onClick.RemoveAllListeners();
            _characterSceneUIManager._backButton.onClick.RemoveAllListeners();
            Owner._characterSceneObj.SetActive(false);
        }



        private void OnClickRightArrow()
        {
            _currentCharacterList[_currentCharacterNum].SetActive(false);
            _currentCharacterNum++;
            if (_currentCharacterNum >= _characterSceneUIManager._charcterLists.Count)
            {
                _currentCharacterNum = 0;
            }
            ChangeCharacter(_currentCharacterNum);
            ChangeWeapon(_currentCharacterNum);
        }


        private void OnClickLeftArrow()
        {
            _currentCharacterList[_currentCharacterNum].SetActive(false);
            _currentCharacterNum--;
            if (_currentCharacterNum < 0)
            {
                _currentCharacterNum = _characterSceneUIManager._charcterLists.Count - 1;
            }
            ChangeCharacter(_currentCharacterNum);
            ChangeWeapon(_currentCharacterNum);
        }

        private void OnClickBackButton()
        {
            Owner._userData._currentCharacter = _characterSceneUIManager._currentCharacterData;
            SaveSystem.Instance.Save();
            foreach (var chara in _currentCharacterList)
            {
                DestroyImmediate(chara);
            }
            _currentCharacterList.RemoveRange(0, _currentCharacterList.Count);
            Owner._stateMachine.Dispatch((int)Event.TitleScene);
        }

        private void ChangeCharacter(int num)
        {
            //  Debug.Log(num);
            _currentCharacterList[num].SetActive(true);
            _characterSceneUIManager._currentCharacterData = _characterSceneUIManager._charcterLists[num];
            _characterSceneUIManager._currentCharacterObj = _currentCharacterList[num];
            Owner._userData._currentCharacter = _characterSceneUIManager._charcterLists[num];
            _characterSceneUIManager._weaponListView._pushCount = 0;
            _characterSceneUIManager._nameText.text = _characterSceneUIManager._charcterLists[num]._name;
            _characterSceneUIManager._statusView.ChangeSkillView(_characterSceneUIManager._charcterLists[num]);
            _characterSceneUIManager._statusView.ChangeStatusView(_characterSceneUIManager._charcterLists[num]);
            _characterSceneUIManager._weaponListView.ChangeWeaponGrid(_characterSceneUIManager._charcterLists[num]);
        }

        private void ChangeWeapon(int num)
        {
            CharacterWeaponData characterWeaponData = _currentCharacterList[num].GetComponent<CharacterWeaponData>();
            if (Owner._userData._currentCharacter._currentWeaponRight != null)
            {
                characterWeaponData.EquipWeapon(Owner._userData._currentCharacter._currentWeaponRight);
            }
            if (Owner._userData._currentCharacter._currentWeaponLeft != null)
            {
                characterWeaponData.EquipWeapon(Owner._userData._currentCharacter._currentWeaponLeft);
            }
        }
    }
}


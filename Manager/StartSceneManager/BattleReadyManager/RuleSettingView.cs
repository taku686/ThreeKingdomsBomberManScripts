using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleSettingView : MonoBehaviour
{
    [SerializeField]
    Text _playerNumText;
    [SerializeField]
    Button _playerNumLeftButton;
    [SerializeField]
    Button _playerNumRightButton;
    [SerializeField]
    GameObject _caution;
    [SerializeField]
    Button _cautionButton;
    [SerializeField]
    Button _okButton;
    [HideInInspector]
    public RobbyRoomView _robbyRoomView;
    public InputField _inputField;
    private int _minPasswordLimit = 3;

    private void OnEnable()
    {
        _playerNumLeftButton.onClick.AddListener(OnClickLeftArrow);
        _playerNumRightButton.onClick.AddListener(OnClickRightArrow);
        _okButton.onClick.AddListener(OnClickOK);
        _cautionButton.onClick.AddListener(OnClickCautionClose);
    }

    private void OnDisable()
    {
        _caution.SetActive(false);
        _playerNumLeftButton.onClick.RemoveAllListeners();
        _playerNumRightButton.onClick.RemoveAllListeners();
        _okButton.onClick.RemoveAllListeners();
        _cautionButton.onClick.RemoveAllListeners();
    }

    private void OnClickLeftArrow()
    {
        GeneralManager._instance._gameSetting._paritcipantsPlayer--;
        if (GeneralManager._instance._gameSetting._paritcipantsPlayer < 2)
        {
            GeneralManager._instance._gameSetting._paritcipantsPlayer = 4;
        }
        _playerNumText.text = GeneralManager._instance._gameSetting._paritcipantsPlayer.ToString() + " Player";
    }

    private void OnClickRightArrow()
    {
        GeneralManager._instance._gameSetting._paritcipantsPlayer++;
        if (GeneralManager._instance._gameSetting._paritcipantsPlayer > 4)
        {
            GeneralManager._instance._gameSetting._paritcipantsPlayer = 2;
        }
        _playerNumText.text = GeneralManager._instance._gameSetting._paritcipantsPlayer.ToString() + " Player";
    }

    private void OnClickOK()
    {
        if (_minPasswordLimit >= _inputField.textComponent.text.Length)
        {
            _caution.SetActive(true);
            return;
        }
        this.gameObject.SetActive(false);
        _robbyRoomView.gameObject.SetActive(true);
    }

    private void OnClickCautionClose()
    {
        _caution.SetActive(false);
    }
}

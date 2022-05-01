using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterFriendRoomView : MonoBehaviour
{

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
        _cautionButton.onClick.AddListener(OnClickCautionClose);
        _okButton.onClick.AddListener(OnClickOK);
    }

    private void OnDisable()
    {
        _cautionButton.onClick.RemoveAllListeners();
        _okButton.onClick.RemoveAllListeners();
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

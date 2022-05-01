using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectView : MonoBehaviour
{
    [HideInInspector]
    public RuleSettingView _ruleSettingView;
    [SerializeField]
    private Button _leftButton;
    [SerializeField]
    private Button _rightButton;
    [SerializeField]
    private Button _stageButton;
    [SerializeField]
    private List<Image> _imageList = new List<Image>();

    public List<Sprite> _stageImage = new List<Sprite>();

    private int _modifiedValue = 2;
    private int _stageNum = 1;
    private void OnEnable()
    {
        _leftButton.onClick.AddListener(OnClickLeftButton);
        _rightButton.onClick.AddListener(OnClickRightButton);
        _stageButton.onClick.AddListener(OnClickStageButton);
        for (int i = 0; i < _imageList.Count; i++)
        {
            _imageList[i].sprite = _stageImage[i];
        }
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveAllListeners();
        _rightButton.onClick.RemoveAllListeners();
        _stageButton.onClick.RemoveAllListeners();
    }

    private void OnClickRightButton()
    {
        _stageNum++;
        // Debug.Log(_stageNum);
        if (_stageNum > _stageImage.Count - _modifiedValue)
        {
            Debug.Log(_stageNum);
            _stageNum = _stageImage.Count - _modifiedValue;
            return;
        }
        _imageList[0].sprite = _stageImage[_stageNum - 1];
        _imageList[1].sprite = _stageImage[_stageNum];
        _imageList[2].sprite = _stageImage[_stageNum + 1];
    }

    private void OnClickLeftButton()
    {
        _stageNum--;
        Debug.Log(_stageNum);
        if (_stageNum - 1 <= 0)
        {
            _stageNum = 1;
            return;
        }
        _imageList[0].sprite = _stageImage[_stageNum - 1];
        _imageList[1].sprite = _stageImage[_stageNum];
        _imageList[2].sprite = _stageImage[_stageNum + 1];
    }

    private void OnClickStageButton()
    {
        Debug.Log(_stageNum);
        GeneralManager._instance._gameSetting._battleStage = _stageNum - 1;
        this.gameObject.SetActive(false);
        _ruleSettingView.gameObject.SetActive(true);
    }
}

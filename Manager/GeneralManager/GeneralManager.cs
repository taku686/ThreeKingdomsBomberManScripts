using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GeneralManager : MonoBehaviour
{
    private const int TARGET_FRAME = 60;

    public static GeneralManager _instance;
    public GameSetting _gameSetting;
    public bool _isGameStart;
    [SerializeField]
    private CharacterData _default;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        _gameSetting = GetComponentInChildren<GameSetting>();
        if (SaveSystem.Instance.UserData._currentCharacter == null)
        {
            SaveSystem.Instance.UserData._currentCharacter = _default;
        }
        Application.targetFrameRate = TARGET_FRAME;
        SaveSystem.Instance.Save();
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}

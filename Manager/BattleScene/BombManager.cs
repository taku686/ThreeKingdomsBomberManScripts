using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
public class BombManager : MonoBehaviourPunCallbacks
{

    public List<Vector3> _currentBombPosList = new List<Vector3>();

    [SerializeField]
    int _bombCreateCount;
    [SerializeField]
    Transform _bombPoolPos;
    [SerializeField]
    List<GameObject> _bombPrefabList = new List<GameObject>();

    [HideInInspector]
    public int _fieldBombCount = 0;
    [HideInInspector]
    public PhotonView _localPlayerPhotonView;

    public List<BombView> _bombViewList = new List<BombView>();
    [HideInInspector]
    public Vector3 _deletePos;
    [HideInInspector]
    public List<PhotonView> _currentPlayerList = new List<PhotonView>();

    private UserData _userData;
    private Vector3 _generatePos;
    //   private List<Transform> _bombPoolList = new List<Transform>();
    private PhotonView _photonView;
    public void Initialize()
    {
        _userData = SaveSystem.Instance.UserData;
        _photonView = GetComponent<PhotonView>();
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            foreach (var player in _currentPlayerList)
            {
                if (player.IsMine)
                {
                    photonView.RPC(nameof(CreateBombPoolPos), RpcTarget.All, player.ViewID.ToString());
                    photonView.RPC(nameof(CreatePool),
                                    RpcTarget.All,
                                    _bombCreateCount,
                                    player.ViewID,
                                      _userData._currentCharacter._bomb._itemPrefab.name);
                }
            }
        }
        else
        {
            CreateBombPoolPos(_localPlayerPhotonView.ViewID.ToString());
            CreatePool(
            _bombCreateCount,
            _localPlayerPhotonView.ViewID,
            _userData._currentCharacter._bomb._itemPrefab.name
            );
        }
        */
        //   CreateBombPoolPos(_localPlayerPhotonView.ViewID.ToString());
        CreatePool(
        _userData._currentCharacter._bomb._itemPrefab,
        _bombCreateCount,
        _localPlayerPhotonView
        );
    }

    public BombView GetBomb(int playerID, PhotonView owner, Vector3 generatePos)
    {
        _generatePos = generatePos;
        //BombAdd();
        foreach (var bombView in _bombViewList.Where(bv => bv.gameObject.activeSelf == false))
        {
            bombView.gameObject.SetActive(true);
            bombView.ResetBomb();
            return bombView;
        }
        return null;
        /*
                BombView newBombView = CreateNewBomb(_userData._currentCharacter._currentWeaponRight._itemPrefab);
                newBombView.Initialize(playerID, owner);
                _bombViewList.Add(newBombView);
                return newBombView;
        */
    }
    //[PunRPC]
    public void CreatePool(GameObject bombObj, int bombCreateCount, PhotonView localPlayerPhotonView)
    {
        Transform bombPoolPos = CreateBombPoolPos(_localPlayerPhotonView.ViewID.ToString());
        for (int i = 0; i < bombCreateCount; i++)
        {
            BombView bombView = CreateNewBomb(bombObj, localPlayerPhotonView);
            bombView.transform.SetParent(bombPoolPos);
            bombView.gameObject.SetActive(false);
            _bombViewList.Add(bombView);
        }
    }

    private BombView CreateNewBomb(GameObject bombObject, PhotonView owner)
    {
        GameObject bomb = PhotonNetwork.Instantiate(SetBombName(bombObject.name), _bombPoolPos.position, owner.transform.rotation);
        BombView newObj = bomb.GetComponent<BombView>();
        newObj.Initialize(owner.ViewID, owner.gameObject);
        /*
        foreach (var bombPos in _bombPoolList.Where((bl) => (bl.name == viewID.ToString())))
        {
            bomb.transform.SetParent(bombPos);
        }
        */
        return newObj;
    }

    [PunRPC]
    private Transform CreateBombPoolPos(string playerName)
    {
        Transform bombPoolPos = new GameObject().transform;
        //  _bombPoolList.Add(bombPoolPos);
        bombPoolPos.name = playerName;
        bombPoolPos.SetParent(_bombPoolPos);
        return bombPoolPos;
    }

    public void BombAdd()
    {
        _fieldBombCount++;
        _currentBombPosList.Add(_generatePos);
        Debug.Log(_fieldBombCount);
    }

    public void BombDelete()
    {
        _fieldBombCount--;
        _currentBombPosList.Remove(_deletePos);
        Debug.Log(_fieldBombCount);
    }

    private string SetBombName(string bombName)
    {
        return "Bomb/" + bombName;
    }
}

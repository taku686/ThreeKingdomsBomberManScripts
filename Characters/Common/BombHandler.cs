using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombHandler : MonoBehaviour
{
    private const string _bombManagerTag = "BombManager";
    private BombManager _bombManager;
    private Vector3 _generatePos;
    private PhotonView _photonView;
    // Start is called before the first frame update
    void OnEnable()
    {
        _photonView = GetComponent<PhotonView>();
        _bombManager = GameObject.FindGameObjectWithTag(_bombManagerTag).GetComponent<BombManager>();
    }

    public void SpawnBomb(int playerID, int bombLimitCount)
    {
        if (_photonView.IsMine && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _photonView.RPC(nameof(RpcSpawnBomb), RpcTarget.All, playerID, bombLimitCount, (int)GenerateBombPos().x, (int)GenerateBombPos().z);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            RpcSpawnBomb(playerID, bombLimitCount, (int)GenerateBombPos().x, (int)GenerateBombPos().z);
        }
    }

    [PunRPC]
    private void RpcSpawnBomb(int playerID, int bombLimitCount, int generatePosX, int generatePosZ)
    {
        _generatePos = new Vector3(generatePosX, 0, generatePosZ);
        if (bombLimitCount <= _bombManager._fieldBombCount)
        {
            return;
        }
        foreach (var pos in _bombManager._currentBombPosList)
        {
            if (pos == _generatePos)
                return;
        }
        BombView bombView = _bombManager.GetBomb(playerID, _photonView, _generatePos);
        bombView._bombModel.transform.rotation = this.transform.rotation;
        bombView.transform.position = _generatePos;
    }

    private Vector3 GenerateBombPos()
    {
        Vector3 pos = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        return pos;
    }
}

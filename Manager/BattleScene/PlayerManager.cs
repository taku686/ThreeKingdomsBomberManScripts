using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public List<PlayerBase> _characterList = new List<PlayerBase>();
    [HideInInspector]
    public PlayerBase _player;
    private UserData _userData;
    public void CreatePlayer()
    {
        _userData = SaveSystem.Instance.UserData;
        GameObject player = null;
        foreach (var character in _characterList.Where(cl => cl._characterNumber == _userData._currentCharacter._number))
        {
            player = PhotonNetwork.Instantiate(
             character.gameObject.name,
              Vector3.zero,
              Quaternion.Euler(0, 180, 0));
            ChangeWeapon(player);
        }
        PlayerBase playerBaseSc = player.GetComponent<PlayerBase>();
        PhotonView photonView = player.GetComponent<PhotonView>();
        player.GetComponent<Rigidbody>().isKinematic = false;
        _player = playerBaseSc;
        playerBaseSc.enabled = true;
    }

    private void ChangeWeapon(GameObject player)
    {
        CharacterWeaponData characterWeaponData = player.GetComponent<CharacterWeaponData>();
        if (_userData._currentCharacter._currentWeaponRight != null)
        {
            characterWeaponData.EquipWeapon(_userData._currentCharacter._currentWeaponRight);
        }
        if (_userData._currentCharacter._currentWeaponLeft != null)
        {
            characterWeaponData.EquipWeapon(_userData._currentCharacter._currentWeaponLeft);
        }
    }

}

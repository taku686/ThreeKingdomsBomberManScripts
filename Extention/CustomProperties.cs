using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class CustomProperties
{
    private const string _passwordKey = "ps";
    private const string _stageNumKey = "sn";
    private const string _playerCountKey = "pc";
    private static readonly Hashtable _hashtable = new Hashtable();

    public static void SetPassword(this Room room, string password)
    {
        _hashtable[_passwordKey] = password;
        room.SetCustomProperties(_hashtable);
        _hashtable.Clear();
    }

    public static void SetStageNum(this Room room, int stageNum)
    {
        _hashtable[_stageNumKey] = stageNum;
        room.SetCustomProperties(_hashtable);
        _hashtable.Clear();
    }

    public static void SetPlayerCount(this Room room, int playerCount)
    {
        _hashtable[_playerCountKey] = playerCount;
        room.SetCustomProperties(_hashtable);
        _hashtable.Clear();
    }

    public static string GetPassword(this Room room)
    {
        return (room.CustomProperties[_passwordKey] is string password) ? password : string.Empty;
    }

    public static int GetStageNum(this Room room)
    {
        return (room.CustomProperties[_stageNumKey] is int stageNum) ? stageNum : 0;
    }

    public static int GetPlayerCount(this Room room)
    {
        return (room.CustomProperties[_playerCountKey] is int playerCount) ? playerCount : 0;
    }
}

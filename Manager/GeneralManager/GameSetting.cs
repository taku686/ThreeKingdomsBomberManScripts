using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public bool _isOnLine;
    public BattleMode _battleMode;
    public bool _isFriendMatch;
    public int _battleStage;
    public int _paritcipantsPlayer;
    public int _time;
    public int _life;
    public List<GameObject> _stageList = new List<GameObject>();

    public enum BattleMode
    {
        BattleRoyale,
        PointMatch,
        CrownBattle,
        JewelBattle,
    }

}

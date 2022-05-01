using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerBase : MonoBehaviour
{
    protected const string SKILL1 = "Attack";
    protected const string SKILL2 = "Passive";
    protected const string InputManagerTag = "InputHandleManager";
    [SerializeField]
    protected int _moveSpeed;
    [SerializeField]
    protected int _bombCount;
    public int _characterNumber;

    protected float _threshold = 0.3f;
    protected UserData _userData;
    protected InputHandleManager _inputHandleManager;
    protected BombHandler _bombHandler;
    protected Animator _animator;
    protected Rigidbody _rb;
    protected PhotonView _photonView;
    protected bool _isDead;
    protected bool _isMove;


    protected enum Event : int
    {
        Idle,
        Skill_1,
        Skill_2,
        Dead,
    }

    protected virtual void PlayerHandle<TOwner>(StateMachine<TOwner> stateMachine) where TOwner : PlayerBase
    {
        if (Input.GetKey(KeyCode.A) && !_isMove)
        {
            stateMachine.Dispatch((int)Event.Skill_1);
        }

        if (Input.GetKey(KeyCode.S) && !_isMove)
        {
            stateMachine.Dispatch((int)Event.Skill_2);
        }
    }
}


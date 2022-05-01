using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public partial class Sousou_Common : PlayerBase
{
    private StateMachine<Sousou_Common> _stateMachine;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _bombHandler = GetComponent<BombHandler>();
        _inputHandleManager = GameObject.FindGameObjectWithTag(InputManagerTag).GetComponent<InputHandleManager>();
        _inputHandleManager.Skill1Button.onClick.AddListener(OnClickSkill1);
        _inputHandleManager.Skill2Button.onClick.AddListener(OnClickSkill2);
        _userData = SaveSystem.Instance.UserData;
        _moveSpeed = _userData._currentCharacter._speed;
        _bombCount = _userData._currentCharacter._bombCount;
        _stateMachine = new StateMachine<Sousou_Common>(this);
        _stateMachine.AddTransition<Sousou_Idle, Sousou_Skill_One>((int)Event.Skill_1);
        _stateMachine.AddTransition<Sousou_Idle, Sousou_Skill_Two>((int)Event.Skill_2);
        _stateMachine.AddAnyTransition<Sousou_Idle>((int)Event.Idle);
        _stateMachine.AddAnyTransition<Sousou_Dead>((int)Event.Dead);
        _animator = GetComponent<Animator>();
        _stateMachine.Start<Sousou_Idle>();
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        PlayerHandle(_stateMachine);
        _stateMachine.Update();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void Dead()
    {
        if (_isDead)
        {
            _stateMachine.Dispatch((int)Event.Dead);
        }
    }

    protected virtual void OnClickSkill1()
    {
        _stateMachine.Dispatch((int)Event.Skill_1);
    }

    protected virtual void OnClickSkill2()
    {
        _stateMachine.Dispatch((int)Event.Skill_2);
    }

    protected override void PlayerHandle<TOwner>(StateMachine<TOwner> stateMachine)
    {
        base.PlayerHandle(stateMachine);
    }
}

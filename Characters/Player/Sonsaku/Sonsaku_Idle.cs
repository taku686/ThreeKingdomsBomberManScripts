using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Sonsaku_Common>.State;

public partial class Sonsaku_Common
{
    public class Sonsaku_Idle : State
    {
        private readonly string _joystick = "Joystick";

        private readonly int _hashSpeedPara = Animator.StringToHash("speedv");

        private Animator _animator;
        private float _speed = 0.9f;
        private float _speedDampTime = 0.1f;
        private Transform _transform;
        private Rigidbody _rb;
        private float _moveSpeed;
        private Vector3 _dir;


        // Start is called before the first frame update
        protected override void OnEnter(State prevState)
        {
            _animator = Owner._animator;
            _transform = Owner.transform;
            _rb = Owner._rb;
            _moveSpeed = Owner._moveSpeed;
            Owner._inputHandleManager.BombButton.onClick.AddListener(() => Owner._bombHandler.SpawnBomb(Owner._photonView.ViewID, Owner._bombCount));
        }

        protected override void OnExit(State nextState)
        {
            Owner._inputHandleManager.BombButton.onClick.RemoveAllListeners();
            Stop();
        }

        // Update is called once per frame
        protected override void OnUpdate()
        {
            EDir d = KeyToDir();

            if (d == EDir.Pause)
            {
                Stop();
            }
            else
            {
                Move(d);
            }
        }

        private EDir KeyToDir()
        {
#if UNITY_EDITOR
            float hori = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
            float hori = UltimateJoystick.GetHorizontalAxis(_joystick);
            float vert = UltimateJoystick.GetVerticalAxis(_joystick);
#endif
            if (Mathf.Abs(hori) >= Mathf.Abs(vert))
            {
                vert = 0;
            }
            else
            {
                hori = 0;
            }

            if (hori == 0 && vert == 0)
            {
                _dir = Vector3.zero;
                return EDir.Pause;
            }
            if (hori <= -Owner._threshold)
            {
                _dir = Vector3.left;
                return EDir.Left;
            }
            if (vert >= Owner._threshold)
            {
                _dir = Vector3.forward;
                return EDir.Up;
            }
            if (hori >= Owner._threshold)
            {
                _dir = Vector3.right;
                return EDir.Right;
            }
            if (vert <= -Owner._threshold)
            {
                _dir = Vector3.back;
                return EDir.Down;
            }
            return EDir.Pause;
        }

        private Quaternion DirToRotation(EDir d)
        {
            Quaternion r = Quaternion.Euler(0, 0, 0);
            switch (d)
            {
                case EDir.Left:
                    r = Quaternion.Euler(0, 270, 0); break;
                case EDir.Up:
                    r = Quaternion.Euler(0, 0, 0); break;
                case EDir.Right:
                    r = Quaternion.Euler(0, 90, 0); break;
                case EDir.Down:
                    r = Quaternion.Euler(0, 180, 0); break;
            }
            return r;
        }

        private void Move(EDir dir)
        {
            _transform.rotation = DirToRotation(dir);
            _rb.velocity = _dir * _moveSpeed;
            _animator.SetFloat(_hashSpeedPara, _speed, _speedDampTime, Time.deltaTime);
        }

        private void Stop()
        {
            _rb.velocity = Vector3.zero;
            _animator.SetFloat(_hashSpeedPara, 0.0f, _speedDampTime, Time.deltaTime);
        }
    }
}

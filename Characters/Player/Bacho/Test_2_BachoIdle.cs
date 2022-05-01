using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BachoCommon>.State;

public partial class BachoCommon
{
    public class Test_2_BachoIdle : State
    {
        // const string _joystic = "Joystick";
        //Vector3 _target;
        // float _rayDistance = 1;
        // float _threshold = 0.5f;
        protected override void OnEnter(State prevState)
        {
            //   _target = Owner.transform.position;
        }
        /*
        protected override void OnUpdate()
        {
            if (Owner.transform.position == _target)
            {
             //   SetTargetPosition(Owner.transform, Owner._initRotation, Owner._animator);
            }
            Move(_target, Owner._moveSpeed);

        }

        private void SetTargetPosition(Transform transform, Quaternion initRotation, Animator animator)
        {


            var hori = UltimateJoystick.GetHorizontalAxis(_joystic);
            var vert = UltimateJoystick.GetVerticalAxis(_joystic);
            if (Mathf.Abs(hori) >= Mathf.Abs(vert))
            {
                vert = 0;
            }
            else
            {
                hori = 0;
            }

            if (hori > _threshold)
            {
                transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -90, 0));

                if (!Physics.Raycast(transform.position, Vector3.right, _rayDistance, Owner._obstacles))
                {
                    _target = transform.position + Vector3.right;
                }

            }
            if (hori < -_threshold)
            {
                transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, 90, 0));

                if (!Physics.Raycast(transform.position, Vector3.left, _rayDistance, Owner._obstacles))
                {
                    _target = transform.position + Vector3.left;
                }

            }
            if (vert > _threshold)
            {
                transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -180, 0));

                if (!Physics.Raycast(transform.position, Vector3.forward, _rayDistance, Owner._obstacles))
                {
                    _target = transform.position + Vector3.forward;
                }

            }
            if (vert < -_threshold)
            {
                transform.rotation = Quaternion.Euler(initRotation.eulerAngles);

                if (!Physics.Raycast(transform.position, Vector3.back, _rayDistance, Owner._obstacles))
                {
                    _target = transform.position + Vector3.back;
                }

            }


            var speed = Mathf.Max(Mathf.Abs(hori), Mathf.Abs(vert));
            animator.SetFloat("speedv", speed);
        }

        //なめらかなグリッドベースの動きの実装に使える
        private void Move(Vector3 target, float moveSpeed)
        {
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, target, moveSpeed * Time.deltaTime);
        }


        protected override void OnExit(State nextState)
        {
            Owner.transform.position = _target;
        }
        */
    }
}


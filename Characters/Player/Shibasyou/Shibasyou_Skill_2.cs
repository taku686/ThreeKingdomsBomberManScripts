using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Shibasyou_Common>.State;

public partial class Shibasyou_Common
{
    public class Shibasyou_Skill_2 : State
    {
        private const float ANIMATION_WAIT_TIME = 0.867f;
        protected override void OnEnter(State prevState)
        {
            Owner.StartCoroutine(Skill2());
        }

        IEnumerator Skill2()
        {
            Owner._animator.SetBool(SKILL2, true);
            yield return new WaitForSeconds(ANIMATION_WAIT_TIME);
            StateMachine.Dispatch((int)Event.Idle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<RyuubiCommon>.State;

public partial class RyuubiCommon
{
    public class RyuubiSkill1 : State
    {
        private const float ANIMATION_WAIT_TIME = 2.0f;
        protected override void OnEnter(State prevState)
        {
            Owner.StartCoroutine(Skill1());
        }

        IEnumerator Skill1()
        {
            Owner._animator.SetBool(SKILL1, true);
            yield return new WaitForSeconds(ANIMATION_WAIT_TIME);
            StateMachine.Dispatch((int)Event.Idle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Syokatsu_Common>.State;

public partial class Syokatsu_Common
{
    public class Syokatsu_Skill_Two : State
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

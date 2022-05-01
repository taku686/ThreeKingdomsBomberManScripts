using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Tyouun_Common>.State;

public partial class Tyouun_Common
{
    public class Tyouun_Skill_Two : State
    {
        private const float ANIMATION_WAIT_TIME = 1.7f;
        private const float ANIMATION_START_MODIFIED_VALUE = 0.1f;
        protected override void OnEnter(State prevState)
        {
            Owner.StartCoroutine(Skill2());
        }

        IEnumerator Skill2()
        {
            //Owner._animator.SetBool(SKILL2, true);
            Owner._animator.Play(SKILL2, 0, ANIMATION_START_MODIFIED_VALUE);
            yield return new WaitForSeconds(ANIMATION_WAIT_TIME);
            StateMachine.Dispatch((int)Event.Idle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Sousou_Common>.State;

public partial class Sousou_Common
{
    public class Sousou_Skill_One : State
    {
        private const float ANIMATION_WAIT_TIME = 1.1f;
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

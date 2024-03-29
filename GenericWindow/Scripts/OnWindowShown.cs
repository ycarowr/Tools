﻿using UnityEngine;

namespace YWR.Tools
{
    public class OnWindowShown : StateMachineBehaviour
    {
        private Window Window { get; set; }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!Window)
            {
                Window = animator.GetComponentInParent<Window>();
            }

            Window?.OnShown?.Invoke();
        }
    }
}
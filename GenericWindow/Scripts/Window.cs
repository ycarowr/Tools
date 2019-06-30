using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Window : MonoBehaviour
    {
        //animations
        private readonly int ShowId = Animator.StringToHash("Show");
        private readonly int HideId = Animator.StringToHash("Hide");
        
        //events
        public Action OnShown { get; set; } = () => { };
        public Action OnHidden { get; set; } = () => { };
        
        //control
        [SerializeField] private Animator animator;
        public bool IsShowing { get; private set; }

        //--------------------------------------------------------------------------------------------------------------
        
        [Button]
        public void Show()
        {
            if (IsShowing)
                return;
            
            IsShowing = true;
            animator?.Play(ShowId);
            OnShow();
        }

        [Button]
        public void Hide()
        {
            if (!IsShowing)
                return;
            
            IsShowing = false;
            animator?.Play(HideId);
            OnHide();
        }
        
        //--------------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Executed immediately when the window opens.
        /// </summary>
        protected virtual void OnShow()
        {
            //Override to do something.
        }
        
        /// <summary>
        ///     Executed immediately when the window closes.
        /// </summary>
        protected virtual void OnHide()
        {
            //Override to do something.
        }
    }
}

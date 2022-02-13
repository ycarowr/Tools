using System;
using UnityEngine;

namespace YWR.Tools
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private readonly int HideId = Animator.StringToHash("Hide");
        private readonly int ShowId = Animator.StringToHash("Show");
        public Action OnShown { get; set; } = () => { };
        public Action OnHidden { get; set; } = () => { };
        public bool IsShowing { get; private set; }

        //--------------------------------------------------------------------------------------------------------------

        [Button]
        public void Show()
        {
            if (IsShowing)
            {
                return;
            }

            IsShowing = true;
            animator?.Play(ShowId);
            OnShow();
        }

        [Button]
        public void Hide()
        {
            if (!IsShowing)
            {
                return;
            }

            IsShowing = false;
            animator?.Play(HideId);
            OnHide();
        }

        //--------------------------------------------------------------------------------------------------------------

        /// <summary> Executed immediately when the window opens. </summary>
        protected virtual void OnShow()
        {
            //Override to do something.
        }

        /// <summary> Executed immediately when the window closes. </summary>
        protected virtual void OnHide()
        {
            //Override to do something.
        }
    }
}
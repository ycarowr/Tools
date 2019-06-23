using UnityEngine;

namespace Tools
{
    public partial class DialogSystem
    {
        private class DialogAnimation : DialogSubComponent
        {
            public DialogAnimation(IDialogSystem system) : base(system)
            {
                ShowHash = Animator.StringToHash("Show");
                HideHash = Animator.StringToHash("Hide");
                Animator = DialogSystem.Monobehavior.GetComponentInChildren<Animator>();
            }

            private int ShowHash { get; }
            private int HideHash { get; }
            private Animator Animator { get; }

            public void Show()
            {
                if (!DialogSystem.IsOpened)
                    Animator.Play(ShowHash);
            }

            public void Hide()
            {
                if (DialogSystem.IsOpened)
                    Animator.Play(HideHash);
            }
        }
    }
}
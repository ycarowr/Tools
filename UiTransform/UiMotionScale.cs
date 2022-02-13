using UnityEngine;

namespace YWR.Tools
{
    public class UiMotionScale : UiMotionBase
    {
        //--------------------------------------------------------------------------------------------------------------

        public UiMotionScale(IUiMotionHandler handler) : base(handler)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        protected override bool CheckFinalState()
        {
            Vector3 delta = Target - Handler.transform.localScale;
            return delta.magnitude <= Threshold;
        }

        protected override void OnMotionEnds()
        {
            Handler.transform.localScale = Target;
            IsOperating = false;
        }

        protected override void KeepMotion()
        {
            Vector3 current = Handler.transform.localScale;
            float amount = Time.deltaTime * Speed;
            Handler.transform.localScale = Vector3.Lerp(current, Target, amount);
        }
    }
}
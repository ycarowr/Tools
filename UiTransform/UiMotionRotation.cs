using UnityEngine;

namespace YWR.Tools
{
    public class UiMotionRotation : UiMotionBase
    {
        //--------------------------------------------------------------------------------------------------------------

        public UiMotionRotation(IUiMotionHandler handler) : base(handler)
        {
        }

        protected override float Threshold => 0.05f;

        //--------------------------------------------------------------------------------------------------------------

        protected override void OnMotionEnds()
        {
            Handler.transform.eulerAngles = Target;
            IsOperating = false;
            OnFinishMotion?.Invoke();
        }

        protected override void KeepMotion()
        {
            Quaternion current = Handler.transform.rotation;
            float amount = Speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(Target);
            Quaternion newRotation = Quaternion.RotateTowards(current, rotation, amount);
            Handler.transform.rotation = newRotation;
        }

        protected override bool CheckFinalState()
        {
            Vector3 distance = Target - Handler.transform.eulerAngles;
            bool smallerThanLimit = distance.magnitude <= Threshold;
            bool equals360 = (int) distance.magnitude == 360;
            bool isFinal = smallerThanLimit || equals360;
            return isFinal;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
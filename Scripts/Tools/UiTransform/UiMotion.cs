using UnityEngine;

namespace Tools.UI
{
    public class UiMotion : IUiMotion
    {
        public UiMotion(IUiMotionHandler handler)
        {
            Scale = new UiMotionScale(handler);
            Movement = new UiMotionMovement(handler);
            Rotation = new UiMotionRotation(handler);
        }

        public UiMotionBase Movement { get; }
        public UiMotionBase Rotation { get; }
        public UiMotionBase Scale { get; }

        public void Update()
        {
            Movement?.Update();
            Rotation?.Update();
            Scale?.Update();
        }

        public void RotateTo(Vector3 rotation, float speed)
        {
            Rotation?.Execute(rotation, speed);
        }

        public void MoveTo(Vector3 position, float speed, float delay = 0)
        {
            Movement?.Execute(position, speed, delay);
        }

        public void MoveToWithZ(Vector3 position, float speed, float delay = 0)
        {
            Movement?.Execute(position, speed, delay);
        }

        public void ScaleTo(Vector3 scale, float speed, float delay = 0)
        {
            Scale?.Execute(scale, speed, delay);
        }
    }
}
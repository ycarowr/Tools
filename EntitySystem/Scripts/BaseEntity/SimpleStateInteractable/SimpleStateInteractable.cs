using UnityEngine;

namespace YWR.Tools
{
    /// <summary> Class handles simple entities that somehow interact with the player.</summary>
    public abstract class SimpleStateInteractable : BaseInteractable
    {
        public enum State
        {
            Inactive,
            Off,
            On
        }

        public State Current { get; private set; }

        public bool IsProcessing => Current == State.On;

        public bool IsDisabled => Current == State.Off;

        public bool IsActive => Current != State.Inactive;

        //--------------------------------------------------------------------------------------------------------------

        protected override void OnTriggerEnterPlayer(GameObject obj)
        {
            SwitchOn();
        }

        protected override void OnTriggerExitPlayer(GameObject obj)
        {
            SwitchOff();
        }

        //--------------------------------------------------------------------------------------------------------------

        /// <summary> Turns the entity on. </summary>
        public void SwitchOn()
        {
            if (!IsActive)
            {
                return;
            }

            Current = State.On;
            OnStartProcessing();
        }

        /// <summary> Turns the entity off. </summary>
        public void SwitchOff()
        {
            if (!IsActive)
            {
                return;
            }

            Current = State.Off;
            OnStopProcessing();
        }

        public void SetState(State state)
        {
            Current = state;
        }

        //--------------------------------------------------------------------------------------------------------------

        /// <summary> Fired when it collides with the player. </summary>
        protected virtual void OnStartProcessing()
        {
            //Override to do something.
        }

        /// <summary> Fired when the player exit the collision. /// </summary>
        protected virtual void OnStopProcessing()
        {
            //Override to do something.
        }
    }
}
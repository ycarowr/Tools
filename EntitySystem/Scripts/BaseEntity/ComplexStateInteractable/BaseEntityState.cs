using System;

namespace YWR.Tools
{
    public abstract class BaseEntityState : IState
    {
        protected BaseEntityState(EntityBehavioursFsm fsm)
        {
            if (fsm == null)
            {
                throw new ArgumentException(
                    $"Can't create {typeof(BaseEntityState)} with a null {typeof(EntityBehavioursFsm)}.");
            }

            Fsm = fsm;
        }

        protected EntityBehavioursFsm Fsm { get; }

        protected EntityData Data => Fsm.Data;
        public bool IsInitialized => Fsm != null;

        public virtual void OnInitialize()
        {
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnClear()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }
    }
}
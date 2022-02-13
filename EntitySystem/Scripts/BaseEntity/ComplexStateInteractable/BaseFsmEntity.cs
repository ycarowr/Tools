using UnityEngine;

namespace YWR.Tools
{
    public abstract class BaseFsmEntity : BaseInteractable
    {
        [SerializeField] private EntityData data;
        public EntityData Data => data;

        public EntityBehavioursFsm Fsm { get; private set; }

        protected virtual void Awake()
        {
            Fsm = CreateEntityFsm();
            Fsm.Initialize();
        }

        private void Update()
        {
            Fsm.Update();
        }

        private void FixedUpdate()
        {
            Fsm.FixedUpdate();
        }

        protected abstract EntityBehavioursFsm CreateEntityFsm();
    }
}
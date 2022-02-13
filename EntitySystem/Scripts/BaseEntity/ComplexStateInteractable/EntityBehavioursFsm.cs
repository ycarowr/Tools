using System;

namespace YWR.Tools
{
    public class EntityBehavioursFsm : BaseStateMachine
    {
        public EntityBehavioursFsm(EntityData data)
        {
            if (data == null)
            {
                throw new ArgumentException(
                    $"Can't create {typeof(EntityBehavioursFsm)} with a null {typeof(EntityData)}.");
            }

            Data = data;
        }

        public EntityData Data { get; }

        protected override void OnInitialize()
        {
            Data.Debug.spawnPoint = Data.Transform.position;
        }
    }
}
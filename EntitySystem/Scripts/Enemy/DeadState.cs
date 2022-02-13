using UnityEngine;

namespace YWR.Tools
{
    public class DeadState : BaseEnemyState
    {
        private float m_DeadTime;

        public DeadState(EnemyBehavioursFsm fsm) : base(fsm)
        {
        }

        public override void OnUpdate()
        {
            m_DeadTime += Time.deltaTime;
            if (m_DeadTime > Data.Parameters.DeadTime)
            {
                Fsm.Spawn();
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Data.Transform.localPosition = Vector3.zero;
            m_DeadTime = 0;
        }
    }
}
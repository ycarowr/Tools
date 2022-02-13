using UnityEngine;

namespace YWR.Tools
{
    public class StunState : BaseEnemyState
    {
        private float m_StunCount;

        public StunState(EnemyBehavioursFsm fsm) : base(fsm)
        {
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_StunCount = 0;
            Data.NavMeshAgent.isStopped = true;
            Data.Shake.duration = Data.Parameters.StunTime;
            Data.Shake.Shake();
        }

        public override void OnExitState()
        {
            Data.NavMeshAgent.isStopped = false;
        }

        public override void OnUpdate()
        {
            m_StunCount += Time.deltaTime;
            if (m_StunCount > Data.Parameters.StunTime)
            {
                Fsm.Idle();
            }
        }
    }
}
using UnityEngine;

namespace YWR.Tools
{
    public class SpawnState : BaseEnemyState
    {
        private float m_SpawningTime;

        public SpawnState(EnemyBehavioursFsm fsm) : base(fsm)
        {
        }

        public override void OnUpdate()
        {
            m_SpawningTime += Time.deltaTime;
            if (m_SpawningTime > Data.Parameters.SpawnTime)
            {
                Fsm.Idle();
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Data.Transform.localPosition = Vector3.zero;
            m_SpawningTime = 0;
        }
    }
}